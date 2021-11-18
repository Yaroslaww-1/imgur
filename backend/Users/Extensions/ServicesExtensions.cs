using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Nito.AsyncEx;
using System;
using System.Linq;
using System.Threading.Tasks;
using MediaLakeUsers.BuildingBlocks.ExecutionContext;
using MediaLakeUsers.BuildingBlocks.Security;
using MediaLakeUsers.Entities;
using MediaLakeUsers.Infrastructure.EntityFramework;
using MediaLakeUsers.Infrastructure.EntityFramework.Repositories.Users;
using MediaLakeUsers.Infrastructure.EventBus.Integration;
using MediaLakeUsers.Infrastructure.EventBus.Integration.Kafka;
using MediaLakeUsers.Infrastructure.IdentityServer;
using MediaLakeUsers.Options;
using MediaLakeUsers.Services.Auth;
using MediaLakeUsers.Services.Users;

namespace MediaLakeUsers.Extensions
{
    public static class ServicesExtensions
    {
		public static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.Location));
			services.Configure<KafkaOptions>(configuration.GetSection(KafkaOptions.Location));
		}

		public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
			var databaseOptions = configuration.GetSection(DatabaseOptions.Location).Get<DatabaseOptions>();

			var migrationAssembly = typeof(MediaLakeUsersDbContext).Assembly.GetName().Name;

			services.AddDbContext<MediaLakeUsersDbContext>(options =>
				options
					.UseNpgsql(
						databaseOptions.ConnectionString,
						opt => opt.MigrationsAssembly(migrationAssembly))
					.UseSnakeCaseNamingConvention()
			);
		}

		public static void RegisterRepositories(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IUserRepository, UserRepository>();
		}

		public static void RegisterEventBus(this IServiceCollection services)
        {
			services.AddSingleton<KafkaConnectionFactory>();
			services.AddSingleton<IIntegrationEventBus, KafkaIntegrationEventBus>();
        }

		public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<ISecurityService, SecurityService>();

			services.AddTransient<UserService>();
			services.AddTransient<IAuthenticationService, AuthenticationService>();
		}

		public static IServiceCollection RegisterIdentityServer(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddIdentityServer()
				.AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
				.AddInMemoryApiResources(IdentityServerConfig.GetApis())
				.AddInMemoryClients(IdentityServerConfig.GetClients())
				.AddInMemoryPersistedGrants()
				.AddProfileService<ProfileService>()
				.AddDeveloperSigningCredential();

			var urlsOptions = configuration.GetSection(UrlsOptions.Location).Get<UrlsOptions>();

			services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

			services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
				.AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
				{
					options.Authority = urlsOptions.GatewayApiUrl;
					options.RequireHttpsMetadata = false;


					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateAudience = false
					};

					options.Events = new JwtBearerEvents
					{
						OnMessageReceived = context =>
						{
							var accessToken = context.Request.Query["access_token"];

							// If the request is for our hub...
							var path = context.HttpContext.Request.Path;
							if (!string.IsNullOrEmpty(accessToken) &&
								(path.StartsWithSegments("/hubs")))
							{
								// Read the token out of the query string
								context.Token = accessToken;
							}
							return Task.CompletedTask;
						}
					};
				});

			services.AddSingleton<ICorsPolicyService>((container) =>
			{
				var logger = container.GetRequiredService<Microsoft.Extensions.Logging.ILogger<DefaultCorsPolicyService>>();
				return new DefaultCorsPolicyService(logger)
				{
					//TODO: Enable
					//AllowedOrigins = { urlsOptions.GatewayApiUrl }
					AllowAll = true
				};
			});

			IdentityModelEventSource.ShowPII = true;

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

			return services;
		}

		public static void InitializeDatabase(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				using var context = scope.ServiceProvider.GetRequiredService<MediaLakeUsersDbContext>();
				context.Database.Migrate();
			};
		}

		public static void ApplyDatabaseSeeding(this IApplicationBuilder app)
		{
			ApplyRolesSeeding(app);
			AsyncContext.Run(() => ApplyUsersSeeding(app));
		}

		public static void ApplyRolesSeeding(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				using var context = scope.ServiceProvider.GetRequiredService<MediaLakeUsersDbContext>();

				var existingRoles = context.Roles.ToList();

				if (!existingRoles.Any())
                {
					context.Roles.Add(Role.CreateNew("User"));

					context.Roles.Add(Role.CreateNew("Admin"));

					context.SaveChanges();
				}
			};
		}

		public async static Task ApplyUsersSeeding(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				using var context = scope.ServiceProvider.GetRequiredService<MediaLakeUsersDbContext>();

				var userService = scope.ServiceProvider.GetRequiredService<UserService>();

				var existingUsers = context.Users.ToList();

				var existingRoles = context.Roles.ToList();

				if (!existingUsers.Any())
				{
					await userService.InitializeUser(
							new Guid("22222222-2222-2222-2222-222222222222"),
							"user@gmail.com",
							"User",
							"userPass",
							existingRoles.Where(r => r.Name == "User").ToList());

					await userService.InitializeUser(
							new Guid("11111111-1111-1111-1111-111111111111"),
							"admin@gmail.com",
							"Admin",
							"adminPass",
							existingRoles.Where(r => r.Name == "Admin").ToList());
				}
			};
		}
    }
}

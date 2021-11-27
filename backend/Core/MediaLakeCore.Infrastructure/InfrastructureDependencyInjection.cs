using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.BuildingBlocks.ExecutionContext;
using MediaLakeCore.BuildingBlocks.Infrastructure;
using MediaLakeCore.BuildingBlocks.Infrastructure.Options;
using MediaLakeCore.Domain.PostComments;
using MediaLakeCore.Domain.PostReactions;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Domain.Users;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediaLakeCore.Infrastructure.EntityFramework.Repositories;
using MediaLakeCore.Infrastructure.EntityFramework.Repositories.PostComments;
using MediaLakeCore.Infrastructure.EntityFramework.Repositories.Posts;
using MediaLakeCore.Infrastructure.EntityFramework.Seeding;
using MediaLakeCore.Infrastructure.EventBus.Integration;
using MediaLakeCore.Infrastructure.EventBus.Integration.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediaLakeCore.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions(configuration);
            services.AddDatabaseContext(configuration);

            services.AddRepositories();
            services.AddUserContext();

            services.AddIntegrationEventBus();
        }

        public static void ConfigureInfrastructure(this IApplicationBuilder app, bool migrate = true, bool seed = true)
        {
            if (migrate)
            {
                MigrateDatabase(app);
            }

            if (seed)
            {
                SeedDatabase(app);
            }
        }

        private static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.Location));
            services.Configure<UrlsOptions>(configuration.GetSection(UrlsOptions.Location));
            services.Configure<KafkaOptions>(configuration.GetSection(KafkaOptions.Location));
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostCommentRepository, PostCommentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPostReactionRepository, PostReactionRepository>();
        }

        private static void AddUserContext(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.AddSingleton<IUserContext, UserContext>();
        }

        private static void AddIntegrationEventBus(this IServiceCollection services)
        {
            services.AddSingleton<KafkaConnectionFactory>();
            services.AddSingleton<IIntegrationEventBus, KafkaIntegrationEventBus>();
        }

        private static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseOptions = configuration.GetSection(DatabaseOptions.Location).Get<DatabaseOptions>();

			var migrationAssembly = typeof(MediaLakeCoreDbContext).Assembly.GetName().Name;

			services.AddDbContext<MediaLakeCoreDbContext>(options =>
            {
                options
                    .UseNpgsql(
                        databaseOptions.ConnectionString,
                        opt => opt.MigrationsAssembly(migrationAssembly))
                    .UseSnakeCaseNamingConvention();

                options.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
            });
        }

		public static void MigrateDatabase(IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				using var context = scope.ServiceProvider.GetRequiredService<MediaLakeCoreDbContext>();
				context.Database.Migrate();
			};
		}

        public static void SeedDatabase(IApplicationBuilder app)
        {
            var databaseSeeder = new DatabaseSeeder(app);
            databaseSeeder.SeedDatabase();
        }
    }
}
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.BuildingBlocks.ExecutionContext;
using MediaLakeCore.BuildingBlocks.Infrastructure;
using MediaLakeCore.BuildingBlocks.Infrastructure.Options;
using MediaLakeCore.Domain.CommentReactions;
using MediaLakeCore.Domain.Comments;
using MediaLakeCore.Domain.Communities;
using MediaLakeCore.Domain.CommunityMember;
using MediaLakeCore.Domain.PostReactions;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Domain.Users;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediaLakeCore.Infrastructure.EntityFramework.Repositories;
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
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;

namespace MediaLakeCore.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions(configuration);
            services.AddLogger(configuration);

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
            services.Configure<VaultOptions>(configuration.GetSection(VaultOptions.Location));
            services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.Location));
            services.Configure<UrlsOptions>(configuration.GetSection(UrlsOptions.Location));
            services.Configure<KafkaOptions>(configuration.GetSection(KafkaOptions.Location));

            services.Configure<LoggerOptions>(configuration.GetSection(LoggerOptions.Location));
            services.Configure<ElasticsearchOptions>(configuration.GetSection(ElasticsearchOptions.Location));
        }

        private static void AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
            var elasticsearchOptions = configuration.GetSection(ElasticsearchOptions.Location).Get<ElasticsearchOptions>();
            var loggerOptions = configuration.GetSection(LoggerOptions.Location).Get<LoggerOptions>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("app", loggerOptions.AppName)
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticsearchOptions.ConnectionString))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                    IndexFormat = elasticsearchOptions.IndexFormat
                })
                .CreateLogger();

            Log.Information("Logger configured");
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPostReactionRepository, PostReactionRepository>();
            services.AddTransient<ICommentReactionRepository, CommentReactionRepository>();
            services.AddTransient<ICommunityRepository, CommunityRepository>();
            services.AddTransient<ICommunityMemberRepository, CommunityMemberRepository>();
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
using FluentValidation;
using MediaLakeCore.Application.Configuration.Behaviours;
using MediaLakeCore.Application.PostReactions;
using MediaLakeCore.Application.Users.CreateUser;
using MediaLakeCore.Domain.PostReactions;
using MediaLakeCore.Infrastructure.EventBus.Integration;
using MediaLakeCore.Infrastructure.EventBus.Integration.Kafka;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MediaLakeCore.Applciation
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddPipelineBehaviour();

            services.AddDomainServices();
        }

        public static void ConfigureApplication(this IApplicationBuilder app)
        {
            app.AddIntegrationEventBus();
        }

        private static void AddPipelineBehaviour(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }

        private static void AddIntegrationEventBus(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var eventBus = (KafkaIntegrationEventBus)scope.ServiceProvider.GetRequiredService<IIntegrationEventBus>();
                eventBus.Subscribe<UserCreatedIntegrationEvent>();

                eventBus.StartConsumer(app);
            };
        }
        private static void AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IPostReactionsToggler, PostReactionsToggler>();
        }
    }
}
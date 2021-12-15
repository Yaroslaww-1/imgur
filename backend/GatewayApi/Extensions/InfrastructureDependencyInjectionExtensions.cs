using MediaLakeGatewayApi.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;

namespace MediaLakeGatewayApi.Extensions
{
    public static class InfrastructureDependencyInjectionExtensions
    {
        public static void AddLogger(this IServiceCollection services, IConfiguration configuration)
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

        public static void AddMonitoring(this IServiceCollection services, IConfiguration configuration)
        {
            var monitoringOptions = configuration.GetSection(MonitoringOptions.Location).Get<MonitoringOptions>();

            if (monitoringOptions.UseHealthChecks)
            {
                services.AddHealthChecks()
                    .ForwardToPrometheus();
            }
        }

        public static void UseMonitoring(this IApplicationBuilder app, IConfiguration configuration)
        {
            var monitoringOptions = configuration.GetSection(MonitoringOptions.Location).Get<MonitoringOptions>();

            if (monitoringOptions.UseHttpMetrics) {
                app.UseHttpMetrics();
            }

            if (monitoringOptions.UseMonitoring)
            {
                Console.WriteLine("UseMonitoring");
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapMetrics();
                });
            }
        }
    }
}

using MediaLakeGatewayApi.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
    }
}

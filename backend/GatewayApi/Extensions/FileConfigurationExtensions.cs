using Microsoft.Extensions.DependencyInjection;
using Ocelot.Configuration.File;
using System;
using System.Collections.Generic;

public static class FileConfigurationExtensions
{
    public static IServiceCollection ConfigureDownstreamHostAndPortsPlaceholders(
        this IServiceCollection services,
        Dictionary<string, string> hosts)
    {
        services.PostConfigure<FileConfiguration>(fileConfiguration =>
        {
            foreach (var route in fileConfiguration.Routes)
            {
                ConfigureRote(route, hosts);
            }
        });

        return services;
    }

    private static void ConfigureRote(FileRoute route, Dictionary<string, string> hosts)
    {
        foreach (var hostAndPort in route.DownstreamHostAndPorts)
        {
            var host = hostAndPort.Host;
            if (host.StartsWith("{") && host.EndsWith("}"))
            {
                var placeHolder = host.TrimStart('{').TrimEnd('}');
                if (hosts.TryGetValue(placeHolder, out var uriString))
                {
                    var uri = new Uri(uriString);
                    route.DownstreamScheme = uri.Scheme;
                    hostAndPort.Host = uri.Host;
                    hostAndPort.Port = uri.Port;
                }
            }
        }
    }
}
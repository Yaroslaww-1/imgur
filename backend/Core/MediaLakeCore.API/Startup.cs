using MediaLakeCore.API.Middlewares;
using MediaLakeCore.Applciation;
using MediaLakeCore.BuildingBlocks.Infrastructure.Options;
using MediaLakeCore.Infrastructure;
using MediaLakeCore.Infrastructure.Vault;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Collections.Generic;

namespace MediaLakeCore
{
    public static class VaultExtensions
    {
        public static IConfigurationBuilder AddConfigSectionFromVault<T>(this IConfigurationBuilder builder, IConfiguration configuration, string sectionName)
        {
            var vaultOptions = configuration.GetSection(VaultOptions.Location).Get<VaultOptions>();
            var vault = new VaultConnectionFactory(vaultOptions);

            var vaultData = vault.GetSection<T>(sectionName).GetAwaiter().GetResult();

            IDictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            foreach (var key in vaultData.Keys)
            {
                keyValuePairs[$"{sectionName}:{key}"] = vaultData[key].ToString();
            }

            builder.AddInMemoryCollection(keyValuePairs);

            return builder;
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddEnvironmentVariables()
                .Build();

            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddEnvironmentVariables()
                .AddConfigSectionFromVault<DatabaseOptions>(Configuration, DatabaseOptions.Location)
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);

            services.AddApplication();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ConfigureInfrastructure();

            app.ConfigureApplication();
        }
    }
}

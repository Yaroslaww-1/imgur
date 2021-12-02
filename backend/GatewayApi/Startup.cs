using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer4.AccessTokenValidation;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using MediaLakeGatewayApi.Options;
using MediaLakeGatewayApi.Extensions;
using Serilog;

namespace MediaLakeGatewayApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddJsonFile("ocelot.json")
                .AddEnvironmentVariables()
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationProviderKey = "IdentityApiKey";

            services.AddLogger(Configuration);

            services.AddCors();

            services.AddOcelot(Configuration);

            services.AddAuthentication()
                .AddIdentityServerAuthentication(authenticationProviderKey, options =>
                {
                    options.Authority = Configuration.GetSection(ServicesOptions.Position).Get<ServicesOptions>().Hosts["Users"];
                    options.SupportedTokens = SupportedTokens.Jwt;
                    options.ApiSecret = "secret";
                    options.RequireHttpsMetadata = false;
                });

            var servicesOptions = Configuration
                .GetSection(ServicesOptions.Position)
                .Get<ServicesOptions>();
            services.ConfigureDownstreamHostAndPortsPlaceholders(servicesOptions.Hosts);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

            app.UseOcelot().Wait();
        }
    }
}

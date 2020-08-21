using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ArTool
{
    public class StartupDevelopment : Startup
    {
        public StartupDevelopment(IConfiguration config) : base(config)
        {
        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddIdentityServer(o => o.Authentication.CookieAuthenticationScheme = "none")
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(IdentityProviderSeedData.GetApiResourceses())
                .AddInMemoryClients(IdentityProviderSeedData.GetClients());

            return base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider)
        {
            base.Configure(app, env, loggerFactory, serviceProvider);
            app.UseDeveloperExceptionPage();
            app.UseIdentityServer();
        }
    }
}
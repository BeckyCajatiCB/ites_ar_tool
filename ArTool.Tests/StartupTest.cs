using System;
using System.Net.Http;
using IdentityServer4.AccessTokenValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArTool.Tests
{
    public class StartupTest : StartupDevelopment
    {
        public HttpMessageHandler IdpHandler;

        public StartupTest(IConfiguration config) : base(config)
        {
        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(this);
            return base.ConfigureServices(services);
        }

        public override void BindIdentityServerAuthenticationOptions(IdentityServerAuthenticationOptions options)
        {
            base.BindIdentityServerAuthenticationOptions(options);

            options.JwtBackChannelHandler = IdpHandler;
            options.IntrospectionBackChannelHandler = IdpHandler;
            options.IntrospectionDiscoveryHandler = IdpHandler;
        }
    }
}
using System;
using ArTool.Configuration;
using AutoMapper;
using ExceptionNotification.Core;
using IdentityServer4.AccessTokenValidation;
using LogApiRequest.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Extensions.Logging;
using SnakeCaseValueProviderFactory.Core;
using StructureMap;
using SwaggerFilters.Core;
using Swashbuckle.AspNetCore.Swagger;

namespace ArTool
{
    public abstract class Startup
    {
        protected Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Uncomment below lines to Enable DBContext health check
            services.AddHealthChecks();
            //.AddDbContextCheck<AppDbContext>();

            //services.AddDbContext<AppDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc(config =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                    config.ValueProviderFactories.Add(new SnakeCaseQueryStringValueProviderFactory());
                })
                .AddJsonOptions(json =>
                {
                    json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    json.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy(true, false)
                    };
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                        new UnprocessableEntityObjectResult(context.ModelState);
                });
                

            services
                .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddCookie("none")
                .AddIdentityServerAuthentication(BindIdentityServerAuthenticationOptions);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("docs", new Info {Title = "Template Api", Version = "v1"});
                c.OperationFilter<SnakeCaseFilter>();
                c.AddSecurityDefinition("oauth2",
                    new OAuth2Scheme
                    {
                        Description = "Requests an authorization token from Identity Provider",
                        TokenUrl = Configuration["IdentityProvider:Authority"] + "/connect/token",
                        Flow = "application"
                    });
                c.OperationFilter<OAuthFilter>();
            });

            services.AddAutoMapper(typeof(Startup));

            return GetServiceProvider(services);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider)
        {
            AddExceptionNotifier(app);

            LogManager.Configuration.Variables["basedir"] = "../..";
            loggerFactory.AddNLog();
            app.UseLogApiRequest();

            app.UseHealthChecks("/health");

            app.UseAuthentication();

            app.UseForwardedHeaders(new ForwardedHeadersOptions {ForwardedHeaders = ForwardedHeaders.XForwardedProto});

            app.UseSwagger(c => { c.RouteTemplate = "{documentName}/swagger.json"; });
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/docs/swagger.json", "Template Api"); });

            app.UseMvc();
        }

        public virtual void BindIdentityServerAuthenticationOptions(IdentityServerAuthenticationOptions options)
        {
            Configuration.GetSection("IdentityProvider").Bind(options);
        }

        private void AddExceptionNotifier(IApplicationBuilder app)
        {
            var config = new ExceptionNotifierConfiguration();
            Configuration.Bind("ExceptionNotification", config);
            app.AddExceptionNotification(config);
        }

        protected virtual IServiceProvider GetServiceProvider(IServiceCollection services)
        {
            var container = new Container();
            container.Configure(config =>
            {
                config.AddRegistry(new BaseRegistry(Configuration));
                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }
    }
}
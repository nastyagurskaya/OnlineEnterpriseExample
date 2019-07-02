using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HealthChecks.UI.Client;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using OnlineEnterprice.Data.Settings;
using OnlineEnterprice.Domain.Entities;
using OnlineEnterprise.Data.Interfaces;
using OnlineEnterprise.Data.Services;
using OnlineEnterpriseProducts.Web.HealthCheck;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnlineEnterprise.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ShopDatabaseSettings>(
                Configuration.GetSection(nameof(ShopDatabaseSettings)));

            services.AddSingleton<IShopDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ShopDatabaseSettings>>().Value);

            services.AddTransient<IMongoRepository<Product>, ProductRepository>();

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddCheck<MongoHealthCheck>("MongoHealthCheck", failureStatus: HealthStatus.Degraded,
                    tags: new[] { "example" });
            services.AddHealthChecksUI();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    // auth server base endpoint (will use to search for disco doc)
                    options.Authority = "http://localhost:51493";
                    options.ApiName = "products_api"; // required audience of access tokens
                    options.RequireHttpsMetadata = false; // dev only!
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Protected Api"
                });

                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Flow = "implicit", // just get token via browser (suitable for swagger SPA)
                    AuthorizationUrl = "http://localhost:51493/connect/authorize",
                    Scopes = new Dictionary<string, string> { { "products_api", "Products Api - full access" } }
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>(); // Required to use access token
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHealthChecks("/healthcheck", new HealthCheckOptions(){ 
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI();

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products Api");

                c.OAuthClientId("products_api_swagger");
                c.OAuthAppName("Products Api Swagger");
            });
        }

        public class AuthorizeCheckOperationFilter : IOperationFilter
        {
            public void Apply(Operation operation, OperationFilterContext context)
            {
                var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes().OfType<AuthorizeAttribute>().Any();

                if (hasAuthorize)
                {
                    operation.Responses.Add("401", new Response { Description = "Unauthorized" });
                    operation.Responses.Add("403", new Response { Description = "Forbidden" });

                    operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                    {
                        new Dictionary<string, IEnumerable<string>> {{"oauth2", new[] {"products_api"}}}
                    };
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAPI
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
            services.AddControllers();
            services.AddAuthorization();
            // Extension methods for setting up authentication services in an Microsoft.Extensions.DependencyInjection.IServiceCollection.
            services.AddAuthentication("Bearer") // it is a Bearer token
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:44376"; //Identity Server URL
                    options.RequireHttpsMetadata = false; // make it false since we are not using https
                    options.ApiName = "api1"; // api name which should be registered in IdentityServer
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Adds the Microsoft.AspNetCore.Authentication.AuthenticationMiddleware to the
            // specified Microsoft.AspNetCore.Builder.IApplicationBuilder, which enables authentication capabilities.
            app.UseAuthentication(); // add the Authentication middleware
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            // Adds the Microsoft.AspNetCore.Authorization.AuthorizationMiddleware to the
            // specified Microsoft.AspNetCore.Builder.IApplicationBuilder, which enables authorization capabilities.
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace intro3_mvc_template
{
    /// <summary>
    /// Startup.cs is very similar to the Web API template, with just a few differences I discuss below:
    /// </summary>
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
            /* In place of the AddControllers() extension method,
             this time we have AddControllersWithViews. As you might expect, this adds the 
             MVC Controller services that are common to both Web API and MVC, but also adds the 
             services required for rendering Razor views. */
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                /* As this is an MVC app, the middleware pipeline includes the Exception
                 handler middleware for environments outside of Development, and also adds 
                 the HSTS and HTTPS redirection middleware, the same as for 2.2. */
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for
                // production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            /* Next up is the static file middleware, which is placed before the routing middleware.
             This ensures that routing doesn't need to happen for every static file request, which 
             could be quite frequent in an MVC app. */
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            /* The only other difference from the Web API template is the registration of
             the MVC controllers in the endpoint routing middleware. In this case a conventional 
             route is added for the MVC controllers, instead of the attribute routing approach 
             that is typical for Web APIs. Again, this is similar to the setup in 2.x, 
             but adjusted for the endpoint routing system. */
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

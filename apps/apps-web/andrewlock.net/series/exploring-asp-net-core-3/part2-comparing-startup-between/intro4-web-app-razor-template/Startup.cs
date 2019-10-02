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

namespace intro4_web_app_razor_template
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /* The first change in this file is the replacement of AddControllersWithViews()
         with AddRazorPages(). As you might expect, this adds all of the additional services 
         required for Razor Pages. Interestingly it does not add the services required for using 
         standard MVC controllers with Razor Views. If you want to use both MVC and Razor Pages 
         in your app, you should continue to use the AddMvc() extension method. */
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            /* The only other change to Startup.cs is to replace the MVC endpoint with the
             Razor Pages endpoint. As with the services, if you wish to use both MVC and Razor 
             Pages in your app, then you'll need to map both endpoints, e.g. */
            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllers(); // Map attribute-routed API controllers
                // endpoints.MapDefaultControllerRoute(); // Map conventional MVC controllers using the default route
                endpoints.MapRazorPages();
            });
        }
    }
}

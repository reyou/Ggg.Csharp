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

namespace intro2_web_api_template
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            /* It isn't used in the default template, but the IConfiguration is injected
             in the constructor in this template. In any real application you'll almost certainly 
             need access to this to configure your services, so it makes sense.*/
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /* In ConfigureServices, there's a call to an extension method, AddControllers(),
             which is new in ASP.NET Core 3.0. In 2.x, you would typically call services.AddMvc() 
             for all ASP.NET Core applications. However, this would configure the services for 
             everything MVC used, such as Razor Pages and View rendering. 
             If you're creating a Web API only, then those services were completely superfluous.*/
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /* The middleware pipeline is fleshed out a little compared to the empty template.
             We have the developer exception page when running in the Development environment, 
             but note there's no exception page in other environments. That's because it's expected 
             that the ApiController will transform errors to the standard Problem Details format. */
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /* Next is the HTTPS redirection middleware, which ensures requests are made over a
             secure domain (definitely a best practice). */
            app.UseHttpsRedirection();

            /* Then we have the Routing middleware, early in the pipeline again, so that subsequent
             middleware can use the selected endpoint when deciding how to behave. */
            app.UseRouting();

            /* The Authorization middleware is new in 3.0, and is enabled largely thanks
             to the introduction of endpoint routing. You can still decorate your controller actions 
             with [Authorize] attributes, but now the enforcement of those attributes occurs here. 
             The real advantage is that you can apply authorization policies to non-MVC endpoints, 
             which previously had to be handled in a manual, imperative manner. */
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                /* Finally, the API controllers are mapped by calling endpoints.MapControllers().
                 This only maps controllers that are decorated with routing attributes - 
                 it doesn't configure any conventional routes.*/
                endpoints.MapControllers();
            });
        }
    }
}

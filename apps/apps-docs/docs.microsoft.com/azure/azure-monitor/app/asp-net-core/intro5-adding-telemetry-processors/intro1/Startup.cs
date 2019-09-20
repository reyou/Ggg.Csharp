using System.IO;
using System.Linq;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.AspNetCore.TelemetryInitializers;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace intro1
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
            // https://github.com/MicrosoftDocs/azure-docs/issues/27395
            string instrumentationKey = File.ReadAllText("C:\\ao\\appKeys\\instrumentationKey1.txt");
            ApplicationInsightsServiceOptions applicationInsightsServiceOptions = new ApplicationInsightsServiceOptions
            {
                DeveloperMode = true,
                InstrumentationKey = instrumentationKey,
                EnableDebugLogger = true,
                EnableAdaptiveSampling = true,
                EnableQuickPulseMetricStream = true,
            };

            // https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core#adding-telemetryinitializers
            services.AddSingleton<ITelemetryInitializer, MyCustomTelemetryInitializer>();

            // Does not hit breakpoint for some reason
            services.AddApplicationInsightsTelemetryProcessor<MyFirstCustomTelemetryProcessor>();
            // If you have more processors:
            services.AddApplicationInsightsTelemetryProcessor<MySecondCustomTelemetryProcessor>();

            services.AddApplicationInsightsTelemetry(applicationInsightsServiceOptions);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

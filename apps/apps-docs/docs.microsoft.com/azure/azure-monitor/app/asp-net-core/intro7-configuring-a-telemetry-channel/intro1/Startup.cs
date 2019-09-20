using System.IO;
using System.Linq;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddApplicationInsightsTelemetry(applicationInsightsServiceOptions);
            // Use the following to replace the default channel with InMemoryChannel.
            // This can also be applied to ServerTelemetryChannel.
            services.AddSingleton(typeof(ITelemetryChannel), new InMemoryChannel() { MaxTelemetryBufferCapacity = 19898 });


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

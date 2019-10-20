using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Api
{
    /// <summary>
    /// https://localhost:5001/identity
    /// </summary>
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddAuthorization()
                .AddNewtonsoftJson();

            services.AddSingleton<IUserService, UserService>();

            /*AddAuthentication adds the authentication services to DI
             and configures "Bearer" as the default scheme. */
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    // Gets or sets the Authority to use when making OpenIdConnect calls.
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    // Gets or sets a single valid audience value for any received OpenIdConnect token.
                    // This value is passed into TokenValidationParameters.ValidAudience if that property is empty.
                    options.Audience = "api1";
                    options.Events = GetEventsJwtBearerEvents();
                });
        }

        private JwtBearerEvents GetEventsJwtBearerEvents()
        {
            JwtBearerEvents jwtBearerEvents = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    IUserService userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                   
                    User user = userService.GetById(context.Principal.Identity.Name);
                    if (user == null)
                    {
                        // return unauthorized if user no longer exists
                        context.Fail("Unauthorized");
                    }
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine(context);

                    return Task.CompletedTask;
                },
                OnMessageReceived = context =>
                {
                    AuthenticateResult authenticateResult = context.Result;
                    Console.WriteLine(JsonConvert.SerializeObject(authenticateResult));
                    return Task.CompletedTask;
                }
            };
            return jwtBearerEvents;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// https://stackoverflow.com/questions/55775895/asp-net-core-3-api-ignores-authorize-attribute-with-bearertoken
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            /*UseAuthentication adds the authentication middleware to the pipeline
             so authentication will be performed automatically on every call into the host.*/
            app.UseAuthentication();
            app.UseAuthorization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World! Go to https://localhost:5001/identity");
                });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });


        }
    }
}

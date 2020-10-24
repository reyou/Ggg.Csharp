using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Intro1
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
            services.AddControllersWithViews();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, configureOptions =>
                {
                    configureOptions.LoginPath = "/home/login";
                }).
                AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Events = new JwtBearerEvents()
                    {
                        OnChallenge = OnChallange,
                        OnTokenValidated = OnTokenValidated,
                        OnForbidden = OnForbidden,
                        OnAuthenticationFailed = OnAuthenticationFailed
                    };
                });
        }

        private Task OnTokenValidated(TokenValidatedContext arg)
        {
            return Task.CompletedTask;
        }

        private async Task OnChallange(JwtBearerChallengeContext arg)
        {
            ClaimsPrincipal httpContextUser = arg.HttpContext.User;
            if (!httpContextUser.Identity.IsAuthenticated)
            {
                arg.Response.Redirect("/home/login");
                await arg.Response.CompleteAsync();
            }
        }

        private Task OnForbidden(ForbiddenContext arg)
        {
            return Task.CompletedTask;
        }

        private Task OnAuthenticationFailed(AuthenticationFailedContext arg)
        {
            return Task.CompletedTask;
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

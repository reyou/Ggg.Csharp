using Microsoft.Extensions.DependencyInjection;

namespace intro1
{
    /// <summary>
    /// https://asp.net-hacker.rocks/2016/02/17/dependency-injection-in-aspnetcore.html
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // These are the almost complete ways to register to the IServiceCollection:

            // AddTransient
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient(typeof(ICountryService), typeof(CountryService));
            services.Add(new ServiceDescriptor(typeof(ICountryService), typeof(CountryService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(ICountryService), p => new CountryService(), ServiceLifetime.Transient));

            // AddSingleton
            services.AddSingleton<ICountryService, CountryService>();
            services.AddSingleton(typeof(ICountryService), typeof(CountryService));
            services.Add(new ServiceDescriptor(typeof(ICountryService), typeof(CountryService), ServiceLifetime.Singleton));
            services.Add(new ServiceDescriptor(typeof(ICountryService), p => new CountryService(), ServiceLifetime.Singleton));

            // AddScoped
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped(typeof(ICountryService), typeof(CountryService));
            services.Add(new ServiceDescriptor(typeof(ICountryService), typeof(CountryService), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(ICountryService), p => new CountryService(), ServiceLifetime.Scoped));

            // services.AddInstance<ICountryService>(new CountryService());
            // services.AddInstance(typeof(ICountryService), new CountryService());
            // services.Add(new ServiceDescriptor(typeof(ICountryService), new CountryService()));

            return services;
        }
    }
}
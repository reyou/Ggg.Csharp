using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using intro1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace intro1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastService _service;
        /* Code like this, where you're injecting the IServiceProvider, is generally a bad idea. 
         * Instead of being explicit about it's dependencies, this controller has an implicit 
         * dependency on WeatherForecastController. As well as being harder for developers to reason about, 
         * it also means the DI validator doesn't know about the dependency. 
         * Consequently, this app will start up fine, and throw on first use. */
        public WeatherForecastController(IServiceProvider provider)
        {
            _service = provider.GetRequiredService<WeatherForecastService>();
        }

        /// <summary>
        /// https://localhost:44347/weatherforecast
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _service.GetForecasts();
        }


    }
}
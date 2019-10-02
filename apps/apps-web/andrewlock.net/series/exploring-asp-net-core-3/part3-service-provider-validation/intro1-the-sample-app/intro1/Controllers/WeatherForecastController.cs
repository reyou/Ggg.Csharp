using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using intro1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace intro1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastService _service;
        public WeatherForecastController(WeatherForecastService service)
        {
            _service = service;
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

        /// <summary>
        /// https://localhost:44347/weatherforecast/get2
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get2")]
        public IEnumerable<WeatherForecast> Get([FromServices] WeatherForecastService service) // injected using DI
        {
            /* There's obviously no reason to do that here, but it makes the point.
             Unfortunately, the DI validation won't be able to detect this use of an unregistered service. 
             The app will start just fun, but will throw an Exception when you attempt to call the action.
            The obvious solution to this one is to avoid the [FromServices] attribute where possible, 
            which shouldn't be difficult to achieve, as you can always inject into the 
            constructor if needs be. There's one more way to source services from the 
            DI container - using service location. */
            return service.GetForecasts();
        }
    }
}



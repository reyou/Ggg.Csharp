using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using intro1.Services;
using Microsoft.AspNetCore.Mvc;

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


    }
}
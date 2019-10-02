using System;
using System.Collections.Generic;
using intro1.Data;

namespace intro1.Services
{
    public class WeatherForecastService
    {
        private readonly DataService _dataService;
        public WeatherForecastService(DataService dataService)
        {
            _dataService = dataService;
        }

        public IEnumerable<WeatherForecast> GetForecasts()
        {
            string[] data = _dataService.GetData();

            List<WeatherForecast> weatherForecasts = new List<WeatherForecast>();
            // use data to create forecasts
            foreach (string s in data)
            {
                weatherForecasts.Add(new WeatherForecast()
                {
                    Date = DateTime.UtcNow,
                    Summary = Guid.NewGuid().ToString(),
                    TemperatureC = (new Random()).Next(30, 80)
                });
            }

            return weatherForecasts;
        }
    }
}
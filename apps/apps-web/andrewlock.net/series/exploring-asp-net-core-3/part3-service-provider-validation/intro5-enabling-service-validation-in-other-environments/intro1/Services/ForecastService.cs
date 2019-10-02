using System;
using System.Collections.Generic;
using intro1.Data;

namespace intro1.Services
{
    public class ForecastService<T> where T : new()
    {
        private readonly DataService _dataService;
        public ForecastService(DataService dataService)
        {
            _dataService = dataService;
        }

        public IEnumerable<T> GetForecasts()
        {
            string[] data = _dataService.GetData();

            // use data to create forecasts
            List<WeatherForecast> weatherForecasts = new List<WeatherForecast>();
            foreach (string s in data)
            {
                weatherForecasts.Add(new WeatherForecast()
                {
                    Date = DateTime.UtcNow,
                    Summary = Guid.NewGuid().ToString(),
                    TemperatureC = (new Random()).Next(30, 80)
                });
            }
            return weatherForecasts as IEnumerable<T>;
        }
    }
}
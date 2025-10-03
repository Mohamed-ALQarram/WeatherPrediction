using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPrediction.DAL.DTOs;

namespace WeatherPrediction.DAL.Interfaces
{
    public interface INasaPowerApiClient
    {
        public Task<List<WeatherParameters>> GetDailyDataAsync(float lat, float lon, DateTime start, DateTime end);
    }
}

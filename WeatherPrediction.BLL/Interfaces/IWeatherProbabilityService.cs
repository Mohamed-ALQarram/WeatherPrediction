using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPrediction.BLL.DTOs;

namespace WeatherPrediction.BLL.Interfaces
{
    public interface IWeatherProbabilityService
    {
        public Task<WeatherPredictionResult> GetDailyProbabilities(float lat, float lon, DateTime date, bool HigherAccuracy=true);
    }
}

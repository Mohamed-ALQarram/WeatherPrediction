using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherPrediction.BLL.DTOs;
using WeatherPrediction.BLL.Interfaces;

namespace WeatherPrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherProbabilityService _weatherService;

        public WeatherController(IWeatherProbabilityService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<ActionResult<WeatherPredictionResult>> GetDailyMean(
         [FromQuery] float lat, [FromQuery] float lon, [FromQuery] DateTime date, [FromQuery] bool HigherAccuracy = true)
        {
            var result = await _weatherService.GetDailyProbabilities(lat, lon, date, HigherAccuracy);
            if (result == null)
                return NotFound("Could not fetch weather data from NASA API.");

            return Ok(result);
        }

    }
}

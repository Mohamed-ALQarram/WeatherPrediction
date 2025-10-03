using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherPrediction.BLL.DTOs;
using WeatherPrediction.BLL.Interfaces;
using WeatherPrediction.DTOs;

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
        public async Task<ActionResult<WeatherPredictionResult>> GetDailyMean([FromQuery] WeatherRequestDTO Request)
        {
            var result = await _weatherService.GetDailyProbabilities(Request.lat, Request.lon, Request.date, Request.HigherAccuracy);
            if (result == null)
                return NotFound("Could not fetch weather data from NASA API.");

            return Ok(result);
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherPrediction.BLL.DTOs;
using WeatherPrediction.BLL.Interfaces;
using WeatherPrediction.DTOs;

namespace WeatherPrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleAIController : ControllerBase
    {
        private readonly IGoogleAiService googleAIService;

        public GoogleAIController(IGoogleAiService googleAIService)
        {
            this.googleAIService = googleAIService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Ask([FromBody] AskRequest request)
        {
            if (string.IsNullOrEmpty(request?.Msg))
            {
                return BadRequest("Msg cannot be null or empty.");
            }
            var result = await googleAIService.GeminiRecommendations(request.Msg);
            return result;
        }
    }
}

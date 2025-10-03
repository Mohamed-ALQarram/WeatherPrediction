namespace WeatherPrediction.BLL.Interfaces
{
    public interface IGoogleAiService
    {
        public Task<string> GeminiRecommendations(string prompt);
    }
}

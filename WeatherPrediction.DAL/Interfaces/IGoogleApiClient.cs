namespace WeatherPrediction.DAL.Interfaces
{
    public interface IGoogleApiClient
    {
        public Task<string> AskGemini(string message);
    }
}

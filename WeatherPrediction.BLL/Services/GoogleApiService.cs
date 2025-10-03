using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPrediction.BLL.Interfaces;
using WeatherPrediction.DAL.Interfaces;

namespace WeatherPrediction.BLL.Services
{
    public class GoogleApiService: IGoogleAiService
    {
        private readonly IGoogleApiClient googleApiClient;

        public GoogleApiService(IGoogleApiClient googleApiClient)
        {
            this.googleApiClient = googleApiClient;
        }

        public Task<string> GeminiRecommendations(string prompt)
        {
            return googleApiClient.AskGemini(prompt);
        }
    }
}

using GenerativeAI;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPrediction.DAL.Interfaces;

namespace WeatherPrediction.DAL.Clients
{
    public class GoogleApiClient : IGoogleApiClient
    {
        private readonly IConfiguration config;

        public GoogleApiClient(IConfiguration config)
        {
            this.config = config;
        }
        public async Task<string> AskGemini(string message)
        {


            var generativeModel = new GenerativeModel(apiKey: config["GoogleAI:ApiKey"]!, model: "gemini-2.5-flash");

            var response = await generativeModel.GenerateContentAsync(message);

            return response.Text;

        }
    }
}

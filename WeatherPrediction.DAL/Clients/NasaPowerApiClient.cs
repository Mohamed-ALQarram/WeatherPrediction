using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPrediction.DAL.DTOs;
using WeatherPrediction.DAL.Helper;
using WeatherPrediction.DAL.Interfaces;

namespace WeatherPrediction.DAL.Clients
{
    public class NasaPowerApiClient : INasaPowerApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration config;

        public NasaPowerApiClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            this.config = config;
        }


        public async Task<List<WeatherParameters>> GetDailyDataAsync(float lat, float lon, DateTime start, DateTime end)
        {
            string url = config["NasaPowerAPI:URL"] +
             $"?start={start:yyyyMMdd}&end={end:yyyyMMdd}" +
             $"&latitude={lat}&longitude={lon}" +
             $"&parameters=T2M,PRECTOTCORR,RH2M,WS2M,PRECSNO,SNODP" +
             $"&community=ag&format=JSON";


            using var stream = await _httpClient.GetStreamAsync(url);
            var ParametersList = new List<WeatherParameters>();

            await foreach (var wc in WeatherDataParser.ReadWeatherAsync(stream, start, end))
            {
                ParametersList.Add(wc);
            }
            return ParametersList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPrediction.BLL.DTOs;
using WeatherPrediction.BLL.Helpers;
using WeatherPrediction.BLL.Interfaces;
using WeatherPrediction.DAL.DTOs;
using WeatherPrediction.DAL.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WeatherPrediction.BLL.Services
{
    public class WeatherProbabilityService : IWeatherProbabilityService
    {
        private readonly INasaPowerApiClient nasaPowerApiClient;


        public WeatherProbabilityService(INasaPowerApiClient nasaPowerApiClient)
        {
            this.nasaPowerApiClient = nasaPowerApiClient;
        }
        public async Task<WeatherPredictionResult> GetDailyProbabilities(float lat, float lon, DateTime date, bool HigherAccuracy = true)
        {
            DateTime endDate = new DateTime(DateTime.Now.AddYears(-1).Year, 12, 31);
            DateTime startDate;
            if (HigherAccuracy)
                startDate = new DateTime(1981, 1, 1);
            else
                startDate = endDate.AddYears(-10);

            var weatherRecords = await nasaPowerApiClient.GetDailyDataAsync(lat, lon, startDate, endDate);

            if (weatherRecords == null)
                weatherRecords = new List<WeatherParameters>();
            var Result = CalculateProbabilities(weatherRecords);

            return Result;
        
        }
        private WeatherPredictionResult CalculateProbabilities(List<WeatherParameters> weatherRecords)
        {
            int NumOfHotDays = 0, NumOfColdDays = 0, NumOfWindyDays = 0, NumOfWetDays = 0,
                NumOfHumidDays = 0, NumOfDryDays=0, NumOfSnowDays=0, NumOfSnowPrecipitation=0;
            float TotalTemp = 0, TotalHumidty = 0, TotalPrecipitation = 0, TotalWindSpeed = 0, TotalSnowDepth=0, TotalSnowPrecipitation=0;
            int count = weatherRecords.Count;
            foreach (var weatherRecord in weatherRecords)
            {
                if (weatherRecord.T2M >= WeatherThresholds.VeryHot) NumOfHotDays++;
                else if (weatherRecord.T2M <= WeatherThresholds.VeryCold) NumOfColdDays++;

                if (weatherRecord.WS2M >= WeatherThresholds.VeryWindy) NumOfWindyDays++;

                if (weatherRecord.RH2M >= WeatherThresholds.VeryHumid) NumOfHumidDays++;
                else if (weatherRecord.RH2M <= WeatherThresholds.VeryDry) NumOfDryDays++;

                if (weatherRecord.PRECTOTCORR >= WeatherThresholds.VeryWet) NumOfWetDays++;

                if (weatherRecord.SNODP >= WeatherThresholds.HeavySnowDepth) NumOfSnowDays++;
                if (weatherRecord.PRECSNO >= WeatherThresholds.HeavySnowfall) NumOfSnowPrecipitation++;

                TotalTemp += weatherRecord.T2M;
                TotalHumidty += weatherRecord.RH2M;
                TotalPrecipitation += weatherRecord.PRECTOTCORR;
                TotalWindSpeed += weatherRecord.WS2M;
                TotalSnowDepth += weatherRecord.SNODP;
                TotalSnowPrecipitation += weatherRecord.PRECSNO;
            }
            #region Calculate AVG
            float AvgTemp = TotalTemp / count;
            float AvgHumidity = TotalHumidty / count;
            float AvgPrecipitation = TotalPrecipitation / count;
            float AvgWindSpeed = TotalWindSpeed / count;
            float AvgSnowDepth = TotalSnowDepth / count;
            float AvgSnowPrecipitation = TotalSnowPrecipitation / count;
            #endregion

            var Result = new WeatherPredictionResult
            {
                #region Object Initialization
                Temperature = new TempDetails
                {
                    AvgTemp = AvgTemp,
                    ColdTempPercent = NumOfColdDays / count * 100.0f,
                    HotTempPercent = NumOfHotDays / count * 100.0f,
                    Description = WeatherConditionHelper.GetTemperatureCondition(AvgTemp)
                },
                Humidity = new HumidityDetails
                {
                    AvgHumidity = AvgHumidity,
                    HighHumidityPercent = NumOfHumidDays / count * 100.0f,
                    Description = WeatherConditionHelper.GetHumidityCondition(AvgHumidity)
                },
                Precipitation = new PrecicptationDetails
                {
                    AvgPrecipitation = AvgPrecipitation,
                    PrecipitationPercent = NumOfWetDays / count * 100.0f,
                    Description = WeatherConditionHelper.GetPrecipitationCondition(AvgPrecipitation)
                },
                WindSpeed = new WindSpeedDetails
                {
                    AvgWindSpeed = AvgWindSpeed,
                    HighWindSpeedPercent = NumOfWindyDays / count * 100.0f,
                    Description = WeatherConditionHelper.GetWindCondition(AvgWindSpeed)
                },
                SnowDepth = new SnowDepthDetails
                {
                    AvgSnowDepth = AvgSnowDepth,
                    SnowDepthPercent = NumOfSnowDays / count * 100.0f,
                    Description = WeatherConditionHelper.GetSnowDepthCondition(AvgSnowDepth)
                },
                SnowPrecipitation = new SnowPrecicptationDetails
                {
                    AvgSnowPrecipitation = AvgSnowPrecipitation,
                    SnowPrecipitationPercent = NumOfSnowPrecipitation / count * 100.0f,
                    Description = WeatherConditionHelper.GetSnowPrecipCondition(AvgSnowPrecipitation)
                }

                #endregion            
            };
            
                return Result;
        }
    }
}

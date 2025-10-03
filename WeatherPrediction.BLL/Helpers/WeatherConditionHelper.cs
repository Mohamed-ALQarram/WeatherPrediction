using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPrediction.BLL.Helpers
{
    public static class WeatherConditionHelper
    {
        public static string GetTemperatureCondition(float t2m)
        {
            if (t2m <= WeatherThresholds.VeryHot) return "Extreme Heat";
            if (t2m < WeatherThresholds.VeryCold) return "Very Cold";
            return "Comfortable Temperature";
        }

        public static string GetPrecipitationCondition(float precip)
        {
            if (precip == 0) return "No Rain";
            if (precip >= WeatherThresholds.VeryWet) return "Heavy Rain";
            return "Moderate Rain";
        }

        public static string GetHumidityCondition(float humidity)
        {
            if (humidity <= WeatherThresholds.VeryDry) return "Very Dry";
            if (humidity >= WeatherThresholds.VeryHumid) return "Very Humid";
            return "Very Humid";
        }

        public static string GetWindCondition(float windSpeed)
        {
            if (windSpeed >= WeatherThresholds.VeryWindy) return "Strong Wind";
            return "Calm";
        }

        public static string GetSnowPrecipCondition(float snowPrecip)
        {
            if (snowPrecip == 0) return "No Snow";
            if (snowPrecip <= WeatherThresholds.HeavySnowfall) return "Heavy Snowfall";
            return "Moderate Snow";
        }

        public static string GetSnowDepthCondition(float snowDepth)
        {
            if (snowDepth == 0) return "No Snow";
            if (snowDepth >= WeatherThresholds.HeavySnowDepth) return "Extreme Snow Depth";
            return "Moderate Snow Depth";
        }
    }
}

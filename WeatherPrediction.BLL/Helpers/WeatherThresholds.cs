using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPrediction.BLL.Helpers
{
    public static class WeatherThresholds
    {
        public const float VeryHot = 40f;          // °C
        public const float VeryCold = 5f;          // °C
        public const float VeryWindy = 15f;        // m/s
        public const float VeryWet = 50f;          // mm/day
        public const float VeryHumid = 85f;        // %
        public const float VeryDry = 20.0f;

        public const float HeavySnowDepth = 20f;   // cm
        public const float HeavySnowfall = 10f;    // mm/day

    }
}

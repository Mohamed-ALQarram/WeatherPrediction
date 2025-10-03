using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPrediction.BLL.Helpers
{
    public static class WeatherThresholds
    {
        public const float VeryHot = 40f;         // °C (daily avg → peak ~40)
        public const float VeryCold = 5f;         // °C (daily avg → night ~0 or -2)
        public const float VeryWindy = 10f;       // m/s (avg → gusts much higher)
        public const float VeryWet = 25f;         // mm/day (avg → heavy rainfall)
        public const float VeryHumid = 70f;       // %
        public const float VeryDry = 40f;         // %

        public const float HeavySnowDepth = 20f;   // cm
        public const float HeavySnowfall = 10f;    // mm/day
    }
}

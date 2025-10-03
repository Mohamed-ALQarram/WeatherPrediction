using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPrediction.BLL.DTOs
{
    public class WeatherPredictionResult
    {
        public TempDetails Temperature { get; set; } = new TempDetails();
        public HumidityDetails Humidity { get; set; } = new HumidityDetails();
        public PrecicptationDetails Precipitation { get; set; } = new PrecicptationDetails();
        public SnowPrecicptationDetails SnowPrecipitation { get; set; } = new SnowPrecicptationDetails();
        public WindSpeedDetails WindSpeed { get; set; } = new WindSpeedDetails();
        public SnowDepthDetails SnowDepth { get; set; } = new SnowDepthDetails();
    }

}

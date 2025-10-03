using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPrediction.DAL.DTOs
{
    public class WeatherParameters
    {
        public float T2M { get; set; }
        public float PRECTOTCORR { get; set; }
        public float RH2M { get; set; }
        public float WS2M { get; set; }
        public float PRECSNO { get; set; }
        public float SNODP { get; set; }
        public DateTime Date { get; set; }
    }
}

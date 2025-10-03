namespace WeatherPrediction.DTOs
{
    public class WeatherRequestDTO
    {
        public float lat { get; set; }
        public float lon { get; set; }
        public DateTime date { get; set; }

        public bool HigherAccuracy { get; set; }
    }
}

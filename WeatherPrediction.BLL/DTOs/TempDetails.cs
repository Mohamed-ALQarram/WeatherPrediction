namespace WeatherPrediction.BLL.DTOs
{
    public class TempDetails
    {
        public float AvgTemp { get; set; }
        public string? Description { get; set; }
        public float HotTempPercent { get; set; }
        public float ColdTempPercent { get; set; }
    }

}

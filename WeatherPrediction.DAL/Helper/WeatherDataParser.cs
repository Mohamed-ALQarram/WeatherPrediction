using System.Text.Json;
using WeatherPrediction.DAL.DTOs;

namespace WeatherPrediction.DAL.Helper
{
    public class WeatherDataParser
    {
        public static async IAsyncEnumerable<WeatherParameters> ReadWeatherAsync(Stream stream, DateTime startDate, DateTime endDate)
        {
            using var doc = await JsonDocument.ParseAsync(stream);

            var param = doc.RootElement
                .GetProperty("properties")
                .GetProperty("parameter");

            var t2m = param.GetProperty("T2M");
            var prectot = param.GetProperty("PRECTOTCORR");
            var rh2m = param.GetProperty("RH2M");
            var ws2m = param.GetProperty("WS2M");
            var precsno = param.GetProperty("PRECSNO");
            var snodp = param.GetProperty("SNODP");

            for (int year = startDate.Year; year <= endDate.Year; year++)
            {
                string dateStr = new DateTime(year, startDate.Month, startDate.Day).ToString("yyyyMMdd");

                if (!t2m.TryGetProperty(dateStr, out var t2mValue)) continue;
                if (!prectot.TryGetProperty(dateStr, out var prectotValue)) continue;
                if (!rh2m.TryGetProperty(dateStr, out var rh2mValue)) continue;
                if (!ws2m.TryGetProperty(dateStr, out var ws2mValue)) continue;
                if (!precsno.TryGetProperty(dateStr, out var precsnoValue)) continue;
                if (!snodp.TryGetProperty(dateStr, out var snodpValue)) continue;

                yield return new WeatherParameters
                {
                    Date = new DateTime(year, startDate.Month, startDate.Day),
                    T2M = t2mValue.GetSingle(),
                    PRECTOTCORR = prectotValue.GetSingle(),
                    RH2M = rh2mValue.GetSingle(),
                    WS2M = ws2mValue.GetSingle(),
                    PRECSNO = precsnoValue.GetSingle(),
                    SNODP= snodpValue.GetSingle()
                };
            }
        }
    }
}

using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPrediction.BLL.DTOs;
using WeatherPrediction.BLL.Interfaces;

namespace WeatherPrediction.BLL.Services
{
    public class CachedWeatherService : IWeatherProbabilityService
    {
        private readonly IWeatherProbabilityService _inner;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _ttl;

        public CachedWeatherService(IWeatherProbabilityService inner, IMemoryCache cache, TimeSpan? ttl = null)
        {
            _inner = inner;
            _cache = cache;
            _ttl = ttl ?? TimeSpan.FromMinutes(30);
        }

        public async Task<WeatherPredictionResult> GetDailyProbabilities(
            float lat, float lon, DateTime date, bool HigherAccuracy = true)
        {
            string key = $"{lat}_{lon}_{date:yyyyMMdd}_{HigherAccuracy}";

            // Get or create a Lazy<Task<T>> to avoid duplicate concurrent calls
            var lazyTask = _cache.GetOrCreate(key, entry =>
            {
                Console.WriteLine($"[CACHE MISS] {key}");
                entry.AbsoluteExpirationRelativeToNow = _ttl;
                entry.Priority = CacheItemPriority.Normal;

                return new Lazy<Task<WeatherPredictionResult>>(() =>
                    _inner.GetDailyProbabilities(lat, lon, date, HigherAccuracy));
            });

            // lazyTask should never be null, but guard against it
            if (lazyTask != null)
            {
                var result = await lazyTask.Value;

                // Optional: don’t cache null results
                if (result == null)
                {
                    _cache.Remove(key);
                }


                return result;
            }
            return await _inner.GetDailyProbabilities(lat, lon, date, HigherAccuracy);
        }
    }
}

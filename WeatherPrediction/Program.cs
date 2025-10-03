
using Microsoft.Extensions.Caching.Memory;
using WeatherPrediction.BLL.Interfaces;
using WeatherPrediction.BLL.Services;
using WeatherPrediction.DAL.Clients;
using WeatherPrediction.DAL.Interfaces;
using WeatherPrediction.Middle_ware;

namespace WeatherPrediction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient<INasaPowerApiClient, NasaPowerApiClient>();

            builder.Services.AddMemoryCache();  //Add built-in memory cache
            builder.Services.AddScoped<IWeatherProbabilityService>(sp =>
            {
                var inner = ActivatorUtilities.CreateInstance<WeatherProbabilityService>(sp);
                var cache = sp.GetRequiredService<IMemoryCache>();
                return new CachedWeatherService(inner, cache, ttl: TimeSpan.FromMinutes(30));
            });

            builder.Services.AddScoped<IGoogleApiClient, GoogleApiClient>();
            builder.Services.AddScoped<IGoogleAiService, GoogleApiService>();
          

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()   // allow requests from any domain
                        .AllowAnyHeader()   // allow any headers
                        .AllowAnyMethod();  // allow GET, POST, PUT, DELETE, etc.
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();
            

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

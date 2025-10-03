# 🌦️ Weather API

This project is a **.NET-based Weather API** that integrates with multiple services to provide accurate and efficient weather predictions.  
It leverages **NASA POWER API** for weather data, **Google Gemini AI** for intelligent insights, and includes a **caching mechanism** for improved performance.

---
## 🔗 Live Demo

👉 [Production API](https://weatherprediction-production-2120.up.railway.app/index.html)  

## 🚀 Features

- **NASA POWER API Integration**  
  Retrieves historical and current weather data (e.g., temperature, precipitation) from NASA's POWER (Prediction Of Worldwide Energy Resources) API.

- **Google Gemini AI Integration**  
  Enhances weather analysis by generating insights and recommendations using Gemini.

- **In-Memory Caching**  
  Optimized performance with `IMemoryCache` to reduce redundant API calls and speed up responses.

- **Clean Architecture**  
  Separation of concerns with layered architecture for maintainability.

---

## 📂 Project Structure

WeatherAPI/
┣ Controllers/ # API endpoints
┣ Services/ # Business logic & integrations
┣ Models/ # DTOs and response models
┣ Caching/ # Cached weather service wrapper
┣ Program.cs # Entry point
┗ README.md # Documentation



## ⚙️ Installation & Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/weather-api.git
   cd weather-api
Restore dependencies:


dotnet restore
Configure your appsettings.json with the required API keys:


{
  "Gemini": {
    "ApiKey": "YOUR_GEMINI_API_KEY"
  },
  "NASA": {
    "BaseUrl": "https://power.larc.nasa.gov/api/"
  }
}
Run the API:

bash
Copy code
dotnet run
📡 API Endpoints
Weather Data (NASA POWER)
http
Copy code
GET /api/weather/daily?lat={latitude}&lon={longitude}&date={yyyy-mm-dd}&higherAccuracy={true|false}
Fetches weather data for the given location and date.

Uses caching to prevent redundant NASA API calls.

AI Insights (Gemini)
http
Copy code
POST /api/weather/ask
Body: "Will it rain tomorrow in Cairo?"
Sends a custom weather-related prompt to Gemini AI for recommendations.

🧠 Caching Strategy
Uses IMemoryCache for short-term in-memory storage.

Configurable TTL (Time-To-Live) and max entries.

Reduces load on NASA API and accelerates repeated requests.

📊 Example Response
{
  "temperature": {
    "maxTemp": 42.28,
    "avgTemp": 30.856365,
    "minTemp": 21.98,
    "description": "Extreme Heat",
    "hotTempPercent": 100,
    "coldTempPercent": 0
  },
  "humidity": {
    "avgHumidity": 44.549088,
    "description": "Moderate Humid",
    "highHumidityPercent": 0
  },
  "precipitation": {
    "avgPrecipitation": 0,
    "description": "No Rain",
    "precipitationPercent": 0
  },
  "snowPrecipitation": {
    "avgSnowPrecipitation": 0,
    "description": "No Snow",
    "snowPrecipitationPercent": 0
  },
  "windSpeed": {
    "avgWindSpeed": 2.9918177,
    "description": "Calm",
    "highWindSpeedPercent": 0
  },
  "snowDepth": {
    "avgSnowDepth": 0,
    "description": "No Snow",
    "snowDepthPercent": 0
  },
 
}
🛠️ Tech Stack
.NET 8 Web API

NASA POWER API

Google Gemini AI

IMemoryCache

📌 Future Improvements
Redis distributed caching

More AI-driven insights (long-term predictions)

Frontend dashboard for visualization

🤝 Contributing
Contributions are welcome!
Fork the repo, create a branch, and submit a PR.





---

Do you want me to make it **short and professional for GitHub** (like many repos have) or a **detailed o

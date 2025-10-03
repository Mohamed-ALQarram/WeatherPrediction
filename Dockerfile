# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ ملفات المشاريع
COPY ["WeatherPrediction/WeatherPrediction.csproj", "WeatherPrediction/"]
COPY ["WeatherPrediction.BLL/WeatherPrediction.BLL.csproj", "WeatherPrediction.BLL/"]
COPY ["WeatherPrediction.DAL/WeatherPrediction.DAL.csproj", "WeatherPrediction.DAL/"]

# استعادة الباكدجات
RUN dotnet restore "WeatherPrediction/WeatherPrediction.csproj"

# نسخ كل الملفات
COPY . .

# بناء ونشر
WORKDIR "/src/WeatherPrediction"
RUN dotnet build "WeatherPrediction.csproj" -c Release -o /app/build
RUN dotnet publish "WeatherPrediction.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "WeatherPrediction.dll"]

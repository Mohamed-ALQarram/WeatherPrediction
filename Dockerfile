# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ ملفات المشاريع
COPY ["WillItRainOnMyParade/WillItRainOnMyParade.csproj", "WillItRainOnMyParade/"]
COPY ["WillItRainOnMyParade.BLL/WillItRainOnMyParade.BLL.csproj", "WillItRainOnMyParade.BLL/"]
COPY ["WillItRainOnMyParade.DAL/WillItRainOnMyParade.DAL.csproj", "WillItRainOnMyParade.DAL/"]

# استعادة الباكدجات
RUN dotnet restore "WillItRainOnMyParade/WillItRainOnMyParade.csproj"

# نسخ كل الملفات
COPY . .

# بناء ونشر
WORKDIR "/src/WillItRainOnMyParade"
RUN dotnet build "WillItRainOnMyParade.csproj" -c Release -o /app/build
RUN dotnet publish "WillItRainOnMyParade.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "WillItRainOnMyParade.dll"]

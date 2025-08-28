using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ServiceControl.Domain.Interfaces;

namespace ServiceControl.Infrastructure.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _configuration;
    private readonly string _apiKey;
    private readonly string _baseUrl;

    public WeatherService(HttpClient httpClient, IMemoryCache cache, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _cache = cache;
        _configuration = configuration;
        _apiKey = _configuration["OpenWeatherMap:ApiKey"] ?? string.Empty;
        _baseUrl = "https://api.openweathermap.org/data/2.5/weather";
    }

    public async Task<decimal> GetTemperatureAsync(string city)
    {
        var cacheKey = $"weather_{city}_{DateTime.UtcNow:yyyy-MM-dd_HH}";
        
        if (_cache.TryGetValue(cacheKey, out decimal cachedTemperature))
        {
            return cachedTemperature;
        }

        try
        {
            var url = $"{_baseUrl}?q={Uri.EscapeDataString(city)}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<WeatherApiResponse>(content);
                
                if (weatherData?.Main?.Temp != 0)
                {
                    var main = weatherData.Main;
                    if (main != null)
                    {
                        var temperature = main.Temp;
                        _cache.Set(cacheKey, temperature, TimeSpan.FromMinutes(30));
                        return temperature;
                    }
                }
            }
        }
        catch
        {
        }

        return 20.0m;
    }

    private class WeatherApiResponse
    {
        public Main? Main { get; set; }
    }

    private class Main
    {
        public decimal Temp { get; set; }
    }
}

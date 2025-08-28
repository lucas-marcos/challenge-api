using System.Net;
using Newtonsoft.Json;
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
                var weatherData = JsonConvert.DeserializeObject<WeatherApiResponse>(content);
                
                if (weatherData?.Main?.Temp != null)
                {
                    var temperature = weatherData.Main.Temp;
                    _cache.Set(cacheKey, temperature, TimeSpan.FromMinutes(30));
                    return temperature;
                }
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception("Cidade não encontrada.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter temperatura: {ex.Message}");
        }

        return 20.0m;
    }

    private class WeatherApiResponse
    {
        public Coord? Coord { get; set; }
        public List<Weather>? Weather { get; set; }
        public string? Base { get; set; }
        public Main? Main { get; set; }
        public int Visibility { get; set; }
        public Wind? Wind { get; set; }
        public Clouds? Clouds { get; set; }
        public long Dt { get; set; }
        public Sys? Sys { get; set; }
        public int Timezone { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Cod { get; set; }
    }

    private class Coord
    {
        public decimal Lon { get; set; }
        public decimal Lat { get; set; }
    }

    private class Weather
    {
        public int Id { get; set; }
        public string? Main { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
    }

    private class Main
    {
        public decimal Temp { get; set; }
        public decimal FeelsLike { get; set; }
        public decimal TempMin { get; set; }
        public decimal TempMax { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int SeaLevel { get; set; }
        public int GrndLevel { get; set; }
    }

    private class Wind
    {
        public decimal Speed { get; set; }
        public int Deg { get; set; }
        public decimal Gust { get; set; }
    }

    private class Clouds
    {
        public int All { get; set; }
    }

    private class Sys
    {
        public int Type { get; set; }
        public int Id { get; set; }
        public string? Country { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
    }
}

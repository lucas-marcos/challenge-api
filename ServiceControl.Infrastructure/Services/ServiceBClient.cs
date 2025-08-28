using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ServiceControl.Domain.Entities;
using ServiceControl.Domain.Interfaces;

namespace ServiceControl.Infrastructure.Services;

public class ServiceBClient : IServiceBClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _serviceBUrl;

    public ServiceBClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _serviceBUrl = _configuration["ServiceB:BaseUrl"] ?? "http://localhost:5002/api/serviceb";
    }

    public async Task<bool> SendProcessedOrderAsync(Order processedOrder)
    {
        try
        {
            var dto = new
            {
                Id = processedOrder.Id.Value,
                OrderDescription = processedOrder.OrderDescription.Value,
                ExecutionDate = processedOrder.ExecutionDate.Value,
                ResponsiblePerson = processedOrder.ResponsiblePerson.Value,
                Temperature = processedOrder.Temperature.Value,
                WeatherCondition = processedOrder.WeatherCondition.Value,
                ProcessedAt = processedOrder.ProcessedAt.Value
            };

            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync(_serviceBUrl, content);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}

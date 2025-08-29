using ServiceControl.Application.DTOs;
using ServiceControl.Application.Interfaces;
using ServiceControl.Domain.Entities;
using ServiceControl.Domain.Interfaces;

namespace ServiceControl.Application.Services;

public class ProcessOrderService : IProcessOrderService
{
    private readonly IWeatherService _weatherService;
    private readonly IOrderRepository _repository;
    private readonly IServiceBClient _serviceBClient;
    private readonly INotificationService _notificationService;

    public ProcessOrderService(
        IWeatherService weatherService,
        IOrderRepository repository,
        IServiceBClient serviceBClient,
        INotificationService notificationService)
    {
        _weatherService = weatherService;
        _repository = repository;
        _serviceBClient = serviceBClient;
        _notificationService = notificationService;
    }

    public async Task<ProcessOrderResult> ExecuteAsync(OrderDto order, string city)
    {
        var weatherData = await GetWeatherDataAsync(city);
        if (HasWeatherErrors()) return CreateFailureResult();

        var entity = await CreateAndSaveOrderAsync(order, weatherData);
        var processedOrder = CreateProcessedOrderDto(entity, order, weatherData);
        
        await _serviceBClient.SendProcessedOrderAsync(entity);

        return CreateSuccessResult(processedOrder);
    }

    private async Task<WeatherData> GetWeatherDataAsync(string city)
    {
        var temperature = await _weatherService.GetTemperatureAsync(city);
        return new WeatherData(temperature, DetermineWeatherCondition(temperature));
    }

    private bool HasWeatherErrors()
    {
        if (!_notificationService.HasNotifications()) return false;
        
        return true;
    }

    private async Task<Order> CreateAndSaveOrderAsync(OrderDto order, WeatherData weatherData)
    {
        var entity = Order.Create(order.OrderDescription, order.ExecutionDate, 
            order.ResponsiblePerson, weatherData.Temperature, weatherData.WeatherCondition);
        await _repository.AddAsync(entity);
        return entity;
    }

    private static ProcessedOrderDto CreateProcessedOrderDto(Order entity, OrderDto order, WeatherData weatherData)
    {
        return new ProcessedOrderDto
        {
            Id = entity.Id,
            OrderDescription = order.OrderDescription,
            ExecutionDate = order.ExecutionDate,
            ResponsiblePerson = order.ResponsiblePerson,
            Temperature = weatherData.Temperature,
            WeatherCondition = weatherData.WeatherCondition,
            ProcessedAt = DateTime.UtcNow
        };
    }

    private ProcessOrderResult CreateFailureResult()
    {
        return new ProcessOrderResult
        {
            Success = false,
            Notifications = _notificationService.GetNotifications(),
            ProcessedOrder = null
        };
    }

    private static ProcessOrderResult CreateSuccessResult(ProcessedOrderDto processedOrder)
    {
        return new ProcessOrderResult
        {
            Success = true,
            Notifications = new List<Notification>(),
            ProcessedOrder = processedOrder
        };
    }

    private static string DetermineWeatherCondition(decimal temperature)
    {
        return temperature switch
        {
            >= 15 and <= 30 => "ótimas condições",
            >= 10 and <= 14 => "agradável",
            _ => "impraticável"
        };
    }

    private record WeatherData(decimal Temperature, string WeatherCondition);
}
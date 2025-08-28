using ServiceControl.Application.DTOs;
using ServiceControl.Application.Interfaces;
using ServiceControl.Domain.Entities;
using ServiceControl.Domain.Interfaces;

namespace ServiceControl.Application.UseCases;

public class ProcessOrderUseCase : IProcessOrderUseCase
{
    private readonly IWeatherService _weatherService;
    private readonly IOrderRepository _repository;
    private readonly IServiceBClient _serviceBClient;
    private readonly INotificationService _notificationService;

    public ProcessOrderUseCase(
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
        var temperature = await _weatherService.GetTemperatureAsync(city);
        var weatherCondition = GetWeatherConditionFromTemperature(temperature);

        if (_notificationService.HasNotifications())
        {
            var notifications = _notificationService.GetNotifications();
            return new ProcessOrderResult
            {
                Success = false,
                Notifications = notifications,
                ProcessedOrder = null
            };
        }

        var entity = Order.Create(
            order.OrderDescription,
            order.ExecutionDate,
            order.ResponsiblePerson,
            temperature,
            weatherCondition
        );

        await _repository.AddAsync(entity);

        var processedOrder = new ProcessedOrderDto
        {
            Id = entity.Id,
            OrderDescription = order.OrderDescription,
            ExecutionDate = order.ExecutionDate,
            ResponsiblePerson = order.ResponsiblePerson,
            Temperature = temperature,
            WeatherCondition = weatherCondition,
            ProcessedAt = DateTime.UtcNow
        };


        await _serviceBClient.SendProcessedOrderAsync(entity);


        return new ProcessOrderResult
        {
            Success = true,
            Notifications = new List<Notification>(),
            ProcessedOrder = processedOrder
        };
    }

    private static string GetWeatherConditionFromTemperature(decimal temperature)
    {
        return temperature switch
        {
            >= 15 and <= 30 => "ótimas condições",
            >= 10 and <= 14 => "agradável",
            _ => "impraticável"
        };
    }
}
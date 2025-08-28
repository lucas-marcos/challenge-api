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

    public ProcessOrderUseCase(
        IWeatherService weatherService,
        IOrderRepository repository,
        IServiceBClient serviceBClient)
    {
        _weatherService = weatherService;
        _repository = repository;
        _serviceBClient = serviceBClient;
    }

    public async Task<ProcessedOrderDto> ExecuteAsync(OrderDto order, string city)
    {
        var temperature = await _weatherService.GetTemperatureAsync(city);
        var weatherCondition = GetWeatherConditionFromTemperature(temperature);

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

        _ = Task.Run(async () =>
        {
            try
            {
                await _serviceBClient.SendProcessedOrderAsync(entity);
            }
            catch
            {
            }
        });

        return processedOrder;
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

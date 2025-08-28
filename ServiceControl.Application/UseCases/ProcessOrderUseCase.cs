using ServiceControl.Application.DTOs;
using ServiceControl.Application.Interfaces;
using ServiceControl.Domain.Entities;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Domain.ValueObjects;

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
        var weatherCondition = WeatherCondition.CreateFromTemperature(temperature);

        var entity = Order.Create(
            order.Id,
            order.OrderDescription,
            order.ExecutionDate,
            order.ResponsiblePerson,
            temperature,
            weatherCondition.Value
        );

        await _repository.AddAsync(entity);

        var processedOrder = new ProcessedOrderDto
        {
            Id = order.Id,
            OrderDescription = order.OrderDescription,
            ExecutionDate = order.ExecutionDate,
            ResponsiblePerson = order.ResponsiblePerson,
            Temperature = temperature,
            WeatherCondition = weatherCondition.Value,
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
}

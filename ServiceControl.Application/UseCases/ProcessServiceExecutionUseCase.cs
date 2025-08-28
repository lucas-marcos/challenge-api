using ServiceControl.Application.DTOs;
using ServiceControl.Application.Interfaces;
using ServiceControl.Domain.Entities;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Domain.ValueObjects;

namespace ServiceControl.Application.UseCases;

public class ProcessServiceExecutionUseCase : IProcessServiceExecutionUseCase
{
    private readonly IWeatherService _weatherService;
    private readonly IServiceExecutionRepository _repository;
    private readonly IServiceBClient _serviceBClient;

    public ProcessServiceExecutionUseCase(
        IWeatherService weatherService,
        IServiceExecutionRepository repository,
        IServiceBClient serviceBClient)
    {
        _weatherService = weatherService;
        _repository = repository;
        _serviceBClient = serviceBClient;
    }

    public async Task<ProcessedServiceExecutionDto> ExecuteAsync(ServiceExecutionDto serviceExecution, string city)
    {
        var temperature = await _weatherService.GetTemperatureAsync(city);
        var weatherCondition = WeatherCondition.CreateFromTemperature(temperature);

        var entity = ServiceExecution.Create(
            serviceExecution.Id,
            serviceExecution.ServicoExecutado,
            serviceExecution.Data,
            serviceExecution.Responsavel,
            temperature,
            weatherCondition.Value
        );

        await _repository.AddAsync(entity);

        var processedExecution = new ProcessedServiceExecutionDto
        {
            Id = serviceExecution.Id,
            ServicoExecutado = serviceExecution.ServicoExecutado,
            Data = serviceExecution.Data,
            Responsavel = serviceExecution.Responsavel,
            Temperature = temperature,
            WeatherCondition = weatherCondition.Value,
            ProcessedAt = DateTime.UtcNow
        };

        _ = Task.Run(async () =>
        {
            try
            {
                await _serviceBClient.SendProcessedExecutionAsync(entity);
            }
            catch
            {
            }
        });

        return processedExecution;
    }
}

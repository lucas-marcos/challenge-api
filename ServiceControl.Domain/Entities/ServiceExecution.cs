using ServiceControl.Domain.ValueObjects;

namespace ServiceControl.Domain.Entities;

public class ServiceExecution : Entity
{
    public ServiceExecutionId Id { get; private set; } = null!;
    public ServiceDescription ServicoExecutado { get; private set; } = null!;
    public ExecutionDate Data { get; private set; } = null!;
    public ResponsiblePerson Responsavel { get; private set; } = null!;
    public Temperature Temperature { get; private set; } = null!;
    public WeatherCondition WeatherCondition { get; private set; } = null!;
    public ProcessedDate ProcessedAt { get; private set; } = null!;
    public CreatedDate CreatedAt { get; private set; } = null!;

    private ServiceExecution() { }

    public ServiceExecution(
        ServiceExecutionId id,
        ServiceDescription servicoExecutado,
        ExecutionDate data,
        ResponsiblePerson responsavel,
        Temperature temperature,
        WeatherCondition weatherCondition)
    {
        Id = id;
        ServicoExecutado = servicoExecutado;
        Data = data;
        Responsavel = responsavel;
        Temperature = temperature;
        WeatherCondition = weatherCondition;
        ProcessedAt = ProcessedDate.Create(DateTime.UtcNow);
        CreatedAt = CreatedDate.Create(DateTime.UtcNow);
    }

    public static ServiceExecution Create(
        string id,
        string servicoExecutado,
        DateTime data,
        string responsavel,
        decimal temperature,
        string weatherCondition)
    {
        return new ServiceExecution(
            ServiceExecutionId.Create(id),
            ServiceDescription.Create(servicoExecutado),
            ExecutionDate.Create(data),
            ResponsiblePerson.Create(responsavel),
            Temperature.Create(temperature),
            WeatherCondition.Create(weatherCondition)
        );
    }
}

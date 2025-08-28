using ServiceControl.Domain.ValueObjects;

namespace ServiceControl.Domain.Entities;

public class Order : Entity
{
    public OrderId Id { get; private set; } = null!;
    public OrderDescription OrderDescription { get; private set; } = null!;
    public ExecutionDate ExecutionDate { get; private set; } = null!;
    public ResponsiblePerson ResponsiblePerson { get; private set; } = null!;
    public Temperature Temperature { get; private set; } = null!;
    public WeatherCondition WeatherCondition { get; private set; } = null!;
    public ProcessedDate ProcessedAt { get; private set; } = null!;
    public CreatedDate CreatedAt { get; private set; } = null!;

    private Order() { }

    public Order(
        OrderId id,
        OrderDescription orderDescription,
        ExecutionDate executionDate,
        ResponsiblePerson responsiblePerson,
        Temperature temperature,
        WeatherCondition weatherCondition)
    {
        Id = id;
        OrderDescription = orderDescription;
        ExecutionDate = executionDate;
        ResponsiblePerson = responsiblePerson;
        Temperature = temperature;
        WeatherCondition = weatherCondition;
        ProcessedAt = ProcessedDate.Create(DateTime.UtcNow);
        CreatedAt = CreatedDate.Create(DateTime.UtcNow);
    }

    public static Order Create(
        string id,
        string orderDescription,
        DateTime executionDate,
        string responsiblePerson,
        decimal temperature,
        string weatherCondition)
    {
        return new Order(
            OrderId.Create(id),
            OrderDescription.Create(orderDescription),
            ExecutionDate.Create(executionDate),
            ResponsiblePerson.Create(responsiblePerson),
            Temperature.Create(temperature),
            WeatherCondition.Create(weatherCondition)
        );
    }
}

namespace ServiceControl.Domain.Entities;

public class Order : Entity
{
    public int Id { get; private set; }
    public string OrderDescription { get; private set; } = string.Empty;
    public DateTime ExecutionDate { get; private set; }
    public string ResponsiblePerson { get; private set; } = string.Empty;
    public decimal Temperature { get; private set; }
    public string WeatherCondition { get; private set; } = string.Empty;
    public DateTime ProcessedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Order() { }

    public Order(
        string orderDescription,
        DateTime executionDate,
        string responsiblePerson,
        decimal temperature,
        string weatherCondition)
    {
        OrderDescription = orderDescription;
        ExecutionDate = executionDate;
        ResponsiblePerson = responsiblePerson;
        Temperature = temperature;
        WeatherCondition = weatherCondition;
        ProcessedAt = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
    }

    public static Order Create(
        string orderDescription,
        DateTime executionDate,
        string responsiblePerson,
        decimal temperature,
        string weatherCondition)
    {
        return new Order(
            orderDescription,
            executionDate,
            responsiblePerson,
            temperature,
            weatherCondition
        );
    }
}

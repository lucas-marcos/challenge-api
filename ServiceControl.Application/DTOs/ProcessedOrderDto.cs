namespace ServiceControl.Application.DTOs;

public class ProcessedOrderDto
{
    public string Id { get; set; } = string.Empty;
    public string OrderDescription { get; set; } = string.Empty;
    public DateTime ExecutionDate { get; set; }
    public string ResponsiblePerson { get; set; } = string.Empty;
    public decimal Temperature { get; set; }
    public string WeatherCondition { get; set; } = string.Empty;
    public DateTime ProcessedAt { get; set; }
}

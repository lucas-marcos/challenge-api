namespace ServiceControl.Application.DTOs;

public class ProcessedServiceExecutionDto
{
    public string Id { get; set; } = string.Empty;
    public string ServicoExecutado { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string Responsavel { get; set; } = string.Empty;
    public decimal Temperature { get; set; }
    public string WeatherCondition { get; set; } = string.Empty;
    public DateTime ProcessedAt { get; set; }
}

namespace ServiceControl.Application.DTOs;

public class ServiceExecutionDto
{
    public string Id { get; set; } = string.Empty;
    public string ServicoExecutado { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string Responsavel { get; set; } = string.Empty;
}

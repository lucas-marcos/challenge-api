namespace ServiceControl.Application.DTOs;

public class OrderDto
{
    public string Id { get; set; } = string.Empty;
    public string OrderDescription { get; set; } = string.Empty;
    public DateTime ExecutionDate { get; set; }
    public string ResponsiblePerson { get; set; } = string.Empty;
}

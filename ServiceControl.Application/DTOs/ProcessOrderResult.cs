using ServiceControl.Domain.Interfaces;

namespace ServiceControl.Application.DTOs;

public class ProcessOrderResult
{
    public bool Success { get; set; }
    public List<Notification> Notifications { get; set; } = new();
    public ProcessedOrderDto? ProcessedOrder { get; set; }
}

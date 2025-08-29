using ServiceControl.Application.DTOs;

namespace ServiceControl.Application.Interfaces;

public interface IProcessOrderService
{
    Task<ProcessOrderResult> ExecuteAsync(OrderDto order, string city);
}

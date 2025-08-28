using ServiceControl.Application.DTOs;

namespace ServiceControl.Application.Interfaces;

public interface IProcessOrderUseCase
{
    Task<ProcessOrderResult> ExecuteAsync(OrderDto order, string city);
}

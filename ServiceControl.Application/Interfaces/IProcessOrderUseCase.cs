using ServiceControl.Application.DTOs;

namespace ServiceControl.Application.Interfaces;

public interface IProcessOrderUseCase
{
    Task<ProcessedOrderDto> ExecuteAsync(OrderDto order, string city);
}

using ServiceControl.Domain.Entities;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Application.Interfaces;

namespace ServiceControl.Application.Services;

public class GetOrderService : IGetOrderService
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        return await _orderRepository.Get();
    }
}

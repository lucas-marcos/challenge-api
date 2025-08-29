using ServiceControl.Domain.Entities;

namespace ServiceControl.Application.Interfaces;

public interface IGetOrderService
{
    Task<IEnumerable<Order>> GetAllOrders();
}

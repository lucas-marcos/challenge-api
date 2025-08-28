using ServiceControl.Domain.Entities;

namespace ServiceControl.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order> AddAsync(Order order);
    Task<Order?> GetByIdAsync(string id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<bool> ExistsAsync(string id);
}

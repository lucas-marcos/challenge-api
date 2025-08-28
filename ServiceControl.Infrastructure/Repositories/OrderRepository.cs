using Microsoft.EntityFrameworkCore;
using ServiceControl.Domain.Entities;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Infrastructure.Data;

namespace ServiceControl.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ServiceControlDbContext _context;

    public OrderRepository(ServiceControlDbContext context)
    {
        _context = context;
    }

    public async Task<Order> AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Orders.AnyAsync(e => e.Id == id);
    }
}

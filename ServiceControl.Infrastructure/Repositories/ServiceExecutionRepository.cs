using Microsoft.EntityFrameworkCore;
using ServiceControl.Domain.Entities;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Infrastructure.Data;

namespace ServiceControl.Infrastructure.Repositories;

public class ServiceExecutionRepository : IServiceExecutionRepository
{
    private readonly ServiceControlDbContext _context;

    public ServiceExecutionRepository(ServiceControlDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceExecution> AddAsync(ServiceExecution serviceExecution)
    {
        _context.ServiceExecutions.Add(serviceExecution);
        await _context.SaveChangesAsync();
        return serviceExecution;
    }

    public async Task<ServiceExecution?> GetByIdAsync(string id)
    {
        return await _context.ServiceExecutions.FindAsync(id);
    }

    public async Task<IEnumerable<ServiceExecution>> GetAllAsync()
    {
        return await _context.ServiceExecutions.ToListAsync();
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.ServiceExecutions.AnyAsync(e => e.Id.Value == id);
    }
}

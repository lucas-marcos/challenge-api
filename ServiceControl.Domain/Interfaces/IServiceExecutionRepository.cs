using ServiceControl.Domain.Entities;

namespace ServiceControl.Domain.Interfaces;

public interface IServiceExecutionRepository
{
    Task<ServiceExecution> AddAsync(ServiceExecution serviceExecution);
    Task<ServiceExecution?> GetByIdAsync(string id);
    Task<IEnumerable<ServiceExecution>> GetAllAsync();
    Task<bool> ExistsAsync(string id);
}

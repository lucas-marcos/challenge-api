using ServiceControl.Domain.Entities;

namespace ServiceControl.Domain.Interfaces;

public interface IServiceBClient
{
    Task<bool> SendProcessedOrderAsync(Order processedOrder);
}

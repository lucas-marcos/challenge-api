using ServiceControl.Application.DTOs;

namespace ServiceControl.Application.Interfaces;

public interface IProcessServiceExecutionUseCase
{
    Task<ProcessedServiceExecutionDto> ExecuteAsync(ServiceExecutionDto serviceExecution, string city);
}

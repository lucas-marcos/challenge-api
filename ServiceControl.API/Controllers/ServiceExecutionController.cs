using Microsoft.AspNetCore.Mvc;
using ServiceControl.Application.DTOs;
using ServiceControl.Application.Interfaces;

namespace ServiceControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceExecutionController : ControllerBase
{
    private readonly IProcessServiceExecutionUseCase _processServiceExecutionUseCase;

    public ServiceExecutionController(IProcessServiceExecutionUseCase processServiceExecutionUseCase)
    {
        _processServiceExecutionUseCase = processServiceExecutionUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<ProcessedServiceExecutionDto>> ProcessServiceExecution(
        [FromBody] ServiceExecutionDto serviceExecution,
        [FromQuery] string city = "São Paulo")
    {
        try
        {
            var result = await _processServiceExecutionUseCase.ExecuteAsync(serviceExecution, city);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ServiceControl.Application.DTOs;
using ServiceControl.Application.Interfaces;

namespace ServiceControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IProcessOrderUseCase _processOrderUseCase;

    public OrderController(IProcessOrderUseCase processOrderUseCase)
    {
        _processOrderUseCase = processOrderUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<ProcessedOrderDto>> ProcessOrder(
        [FromBody] OrderDto order,
        [FromQuery] string city = "São Paulo")
    {
        try
        {
            var result = await _processOrderUseCase.ExecuteAsync(order, city);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal server error", message = ex.Message });
        }
    }
}

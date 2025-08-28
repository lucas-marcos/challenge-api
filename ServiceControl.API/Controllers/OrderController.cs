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
    public async Task<IActionResult> ProcessOrder([FromBody] OrderDto order, [FromQuery] string city)
    {
        var result = await _processOrderUseCase.ExecuteAsync(order, city);

        if (!result.Success)
        {
            return BadRequest(new
            {
                Success = false,
                Notifications = result.Notifications,
                Message = "Erro no processamento do pedido"
            });
        }

        return Ok(new
        {
            Success = true,
            ProcessedOrder = result.ProcessedOrder,
            Notifications = result.Notifications
        });
    }
}

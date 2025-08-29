using Microsoft.AspNetCore.Mvc;
using ServiceControl.Application.DTOs;
using ServiceControl.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ServiceControl.API.Controllers;

/// <summary>
/// Controlador para gerenciamento de pedidos
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class OrderController : ControllerBase
{
    private readonly IProcessOrderService _processOrderService;

    public OrderController(IProcessOrderService processOrderService)
    {
        _processOrderService = processOrderService;
    }

    /// <summary>
    /// Processa um novo pedido com informações de clima
    /// </summary>
    /// <param name="order">Dados do pedido a ser processado</param>
    /// <param name="city">Cidade para obter informações climáticas (padrão: São Paulo)</param>
    /// <returns>Resultado do processamento com pedido processado ou notificações de erro</returns>
    /// <response code="200">Pedido processado com sucesso</response>
    /// <response code="400">Erro no processamento com notificações</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [ProducesResponseType(typeof(ProcessOrderSuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProcessOrderErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ProcessOrder(
        [FromBody] OrderDto order, 
        [FromQuery] string city = "São Paulo")
    {
        var result = await _processOrderService.ExecuteAsync(order, city);

        if (!result.Success)
        {
            return BadRequest(new ProcessOrderErrorResponse
            {
                Success = false,
                Notifications = result.Notifications,
                Message = "Erro no processamento do pedido"
            });
        }

        return Ok(new ProcessOrderSuccessResponse
        {
            Success = true,
            ProcessedOrder = result.ProcessedOrder,
            Notifications = result.Notifications
        });
    }
}

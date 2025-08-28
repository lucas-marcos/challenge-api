using ServiceControl.Domain.Interfaces;

namespace ServiceControl.Application.DTOs;

/// <summary>
/// Resultado do processamento de um pedido
/// </summary>
public class ProcessOrderResult
{
    /// <summary>
    /// Indica se o processamento foi bem-sucedido
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Lista de notificações (erros, avisos, informações)
    /// </summary>
    public List<Notification> Notifications { get; set; } = new();

    /// <summary>
    /// Pedido processado com sucesso (null se houver falha)
    /// </summary>
    public ProcessedOrderDto? ProcessedOrder { get; set; }
}

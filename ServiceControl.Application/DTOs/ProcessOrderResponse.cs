using ServiceControl.Domain.Interfaces;

namespace ServiceControl.Application.DTOs;

/// <summary>
/// Resposta de sucesso para processamento de pedido
/// </summary>
public class ProcessOrderSuccessResponse
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Pedido processado com sucesso
    /// </summary>
    public ProcessedOrderDto ProcessedOrder { get; set; } = new();

    /// <summary>
    /// Lista de notificações (geralmente vazia em caso de sucesso)
    /// </summary>
    public List<Notification> Notifications { get; set; } = new();
}

/// <summary>
/// Resposta de erro para processamento de pedido
/// </summary>
public class ProcessOrderErrorResponse
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida
    /// </summary>
    /// <example>false</example>
    public bool Success { get; set; }

    /// <summary>
    /// Lista de notificações de erro
    /// </summary>
    public List<Notification> Notifications { get; set; } = new();

    /// <summary>
    /// Mensagem de erro geral
    /// </summary>
    /// <example>Erro no processamento do pedido</example>
    public string Message { get; set; } = string.Empty;
}

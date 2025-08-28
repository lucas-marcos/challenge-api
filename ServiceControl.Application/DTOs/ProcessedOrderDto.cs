namespace ServiceControl.Application.DTOs;

/// <summary>
/// DTO para pedido processado com informações climáticas
/// </summary>
public class ProcessedOrderDto
{
    /// <summary>
    /// ID único do pedido processado
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }

    /// <summary>
    /// Descrição do pedido
    /// </summary>
    /// <example>Manutenção preventiva do sistema</example>
    public string OrderDescription { get; set; } = string.Empty;

    /// <summary>
    /// Data de execução do pedido
    /// </summary>
    /// <example>2025-08-28T10:00:00Z</example>
    public DateTime ExecutionDate { get; set; }

    /// <summary>
    /// Pessoa responsável pela execução
    /// </summary>
    /// <example>João Silva</example>
    public string ResponsiblePerson { get; set; } = string.Empty;

    /// <summary>
    /// Temperatura atual na cidade (em Celsius)
    /// </summary>
    /// <example>22.5</example>
    public decimal Temperature { get; set; }

    /// <summary>
    /// Condição climática baseada na temperatura
    /// </summary>
    /// <example>ótimas condições</example>
    public string WeatherCondition { get; set; } = string.Empty;

    /// <summary>
    /// Data e hora do processamento
    /// </summary>
    /// <example>2025-08-27T23:55:00Z</example>
    public DateTime ProcessedAt { get; set; }
}

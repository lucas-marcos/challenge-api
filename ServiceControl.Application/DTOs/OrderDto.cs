using System.ComponentModel.DataAnnotations;

namespace ServiceControl.Application.DTOs;

/// <summary>
/// DTO para criação de um novo pedido
/// </summary>
public class OrderDto
{
    /// <summary>
    /// Descrição do pedido
    /// </summary>
    /// <example>Manutenção preventiva do sistema</example>
    [Required(ErrorMessage = "A descrição do pedido é obrigatória")]
    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
    public string OrderDescription { get; set; } = string.Empty;

    /// <summary>
    /// Data de execução do pedido
    /// </summary>
    /// <example>2025-08-28T10:00:00Z</example>
    [Required(ErrorMessage = "A data de execução é obrigatória")]
    public DateTime ExecutionDate { get; set; }

    /// <summary>
    /// Pessoa responsável pela execução
    /// </summary>
    /// <example>João Silva</example>
    [Required(ErrorMessage = "A pessoa responsável é obrigatória")]
    [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres")]
    public string ResponsiblePerson { get; set; } = string.Empty;
}

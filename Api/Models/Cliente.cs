using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class Cliente
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required, MaxLength(160), EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MaxLength(11)]
    public string Cpf { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Rg { get; set; }

    public int? ContatoId { get; set; }
    public Contato? Contato { get; set; }
}

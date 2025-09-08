using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Models.Enums;

namespace Api.Models;

public class Endereco
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public TipoEndereco Tipo { get; set; } = TipoEndereco.Preferencial;

    [Required, MaxLength(9)]
    public string Cep { get; set; } = string.Empty;

    [MaxLength(120)]
    public string? Logradouro { get; set; }

    public int Numero { get; set; }

    [MaxLength(80)]
    public string? Bairro { get; set; }

    [MaxLength(80)]
    public string? Complemento { get; set; }

    [MaxLength(80)]
    public string? Cidade { get; set; }

    [MaxLength(2)]
    public string? Estado { get; set; }

    [MaxLength(120)]
    public string? Referencia { get; set; }

    public Contato? Contato { get; set; }
}

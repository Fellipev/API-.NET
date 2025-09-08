using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Models.Enums;

namespace Api.Models;

public class Contato
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public TipoContato Tipo { get; set; } = TipoContato.Residencial;

    [Range(10, 99)]
    public int Ddd { get; set; }

    [Required]
    public decimal Telefone { get; set; }

    public int? EnderecoId { get; set; }
    public Endereco? Endereco { get; set; }

    public Cliente? Cliente { get; set; }
}

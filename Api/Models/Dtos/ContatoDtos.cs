using System.ComponentModel.DataAnnotations;
using Api.Models.Enums;

namespace Api.Dtos;

public class CreateContatoDto
{
    [Required] public TipoContato Tipo { get; set; }
    [Range(10, 99)] public int Ddd { get; set; }
    [Required] public decimal Telefone { get; set; }
    public int? EnderecoId { get; set; }
}
public class UpdateContatoDto : CreateContatoDto { }

public record ContatoDto(
    int Id,
    string Tipo,
    int Ddd,
    decimal Telefone,
    int? EnderecoId,
    string? EnderecoResumo
);

using System.ComponentModel.DataAnnotations;
using Api.Models.Enums;

namespace Api.Dtos;

public class CreateEnderecoDto
{
    [Required] public TipoEndereco Tipo { get; set; }
    [Required, MaxLength(9)] public string Cep { get; set; } = "";
    [MaxLength(120)] public string? Logradouro { get; set; }
    public int Numero { get; set; }
    [MaxLength(80)] public string? Bairro { get; set; }
    [MaxLength(80)] public string? Complemento { get; set; }
    [MaxLength(80)] public string? Cidade { get; set; }
    [MaxLength(2)] public string? Estado { get; set; }
    [MaxLength(120)] public string? Referencia { get; set; }
}
public class UpdateEnderecoDto : CreateEnderecoDto { }

public record EnderecoDto(
    int Id,
    string Tipo,
    string Cep,
    string? Logradouro,
    int Numero,
    string? Bairro,
    string? Complemento,
    string? Cidade,
    string? Estado,
    string? Referencia
);

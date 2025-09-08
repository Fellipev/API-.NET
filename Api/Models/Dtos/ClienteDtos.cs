using System.ComponentModel.DataAnnotations;
using Api.Validations;

namespace Api.Dtos;

public class CreateClienteDto
{
    [Required, MaxLength(120)] public string Nome { get; set; } = "";
    [Required, MaxLength(160), EmailAddress] public string Email { get; set; } = "";
    [Required, Cpf] public string Cpf { get; set; } = "";
    [MaxLength(20), Rg] public string? Rg { get; set; }
}
public class UpdateClienteDto : CreateClienteDto { }

public record ClienteDto(
    int Id,
    string Nome,
    string Email,
    string Cpf,
    string? Rg,
    int? ContatoId,
    string? TelefoneE164,
    string? LogradouroCompleto
);

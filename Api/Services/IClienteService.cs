using Api.Dtos;

namespace Api.Services;

public interface IClienteService
{
    Task<(IEnumerable<ClienteDto> items, int total)> SearchAsync(string? nome, string? email, string? cpf);
    Task<ClienteDto?> GetAsync(int id);
    Task<ClienteDto> CreateAsync(CreateClienteDto input);
    Task<bool> UpdateAsync(int id, UpdateClienteDto input);
    Task<bool> DeleteAsync(int id);

    Task<bool> LinkContatoAsync(int clienteId, int contatoId);
    Task<bool> UnlinkContatoAsync(int clienteId);
}

using Api.Dtos;

namespace Api.Services;

public interface IContatoService
{
    Task<IEnumerable<ContatoDto>> ListAsync();
    Task<ContatoDto?> GetAsync(int id);
    Task<ContatoDto> CreateAsync(CreateContatoDto input);
    Task<bool> UpdateAsync(int id, UpdateContatoDto input);
    Task<bool> DeleteAsync(int id);

    Task<bool> LinkEnderecoAsync(int contatoId, int enderecoId);
    Task<bool> UnlinkEnderecoAsync(int contatoId);
}

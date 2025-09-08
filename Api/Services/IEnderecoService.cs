using Api.Dtos;

namespace Api.Services;

public interface IEnderecoService
{
    Task<IEnumerable<EnderecoDto>> ListAsync();
    Task<EnderecoDto?> GetAsync(int id);
    Task<EnderecoDto> CreateAsync(CreateEnderecoDto input);
    Task<bool> UpdateAsync(int id, UpdateEnderecoDto input);
    Task<bool> DeleteAsync(int id);
}

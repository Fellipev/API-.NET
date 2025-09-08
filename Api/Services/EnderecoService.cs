using Api.Dtos;
using Api.Models;
using Api.Repositories;

namespace Api.Services;

public class EnderecoService(IEnderecoRepository repo) : IEnderecoService
{
    public async Task<IEnumerable<EnderecoDto>> ListAsync() => (await repo.GetAllAsync()).Select(Map);
    public async Task<EnderecoDto?> GetAsync(int id) => (await repo.GetByIdAsync(id)) is { } e ? Map(e) : null;

    public async Task<EnderecoDto> CreateAsync(CreateEnderecoDto input)
    {
        var e = new Endereco {
            Tipo = input.Tipo, Cep = input.Cep, Logradouro = input.Logradouro,
            Numero = input.Numero, Bairro = input.Bairro, Complemento = input.Complemento,
            Cidade = input.Cidade, Estado = input.Estado?.ToUpperInvariant(), Referencia = input.Referencia
        };
        await repo.AddAsync(e); await repo.SaveChangesAsync();
        return Map(e);
    }

    public async Task<bool> UpdateAsync(int id, UpdateEnderecoDto input)
    {
        var e = await repo.GetByIdAsync(id); if (e is null) return false;
        e.Tipo = input.Tipo; e.Cep = input.Cep; e.Logradouro = input.Logradouro;
        e.Numero = input.Numero; e.Bairro = input.Bairro; e.Complemento = input.Complemento;
        e.Cidade = input.Cidade; e.Estado = input.Estado?.ToUpperInvariant(); e.Referencia = input.Referencia;
        await repo.UpdateAsync(e); await repo.SaveChangesAsync(); return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var e = await repo.GetByIdAsync(id); if (e is null) return false;
        await repo.DeleteAsync(e); await repo.SaveChangesAsync(); return true;
    }

    private static EnderecoDto Map(Endereco e) =>
        new(e.Id, e.Tipo.ToString(), e.Cep, e.Logradouro, e.Numero, e.Bairro, e.Complemento, e.Cidade, e.Estado, e.Referencia);
}

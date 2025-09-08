using Api.Dtos;
using Api.Models;
using Api.Repositories;

namespace Api.Services;

public class ContatoService(IContatoRepository repo, IEnderecoRepository enderecos) : IContatoService
{
    public async Task<IEnumerable<ContatoDto>> ListAsync() => (await repo.GetAllAsync()).Select(Map);
    public async Task<ContatoDto?> GetAsync(int id) => (await repo.GetByIdAsync(id)) is { } c ? Map(c) : null;

    public async Task<ContatoDto> CreateAsync(CreateContatoDto input)
    {
        if (input.EnderecoId is not null && await enderecos.GetByIdAsync(input.EnderecoId.Value) is null)
            throw new ArgumentException("EnderecoId inválido.");

        var c = new Contato { Tipo = input.Tipo, Ddd = input.Ddd, Telefone = input.Telefone, EnderecoId = input.EnderecoId };
        await repo.AddAsync(c); await repo.SaveChangesAsync();
        return Map(c);
    }

    public async Task<bool> UpdateAsync(int id, UpdateContatoDto input)
    {
        var c = await repo.GetByIdAsync(id); if (c is null) return false;
        if (input.EnderecoId is not null && await enderecos.GetByIdAsync(input.EnderecoId.Value) is null)
            throw new ArgumentException("EnderecoId inválido.");

        c.Tipo = input.Tipo; c.Ddd = input.Ddd; c.Telefone = input.Telefone; c.EnderecoId = input.EnderecoId;
        await repo.UpdateAsync(c); await repo.SaveChangesAsync(); return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var c = await repo.GetByIdAsync(id); if (c is null) return false;
        await repo.DeleteAsync(c); await repo.SaveChangesAsync(); return true;
    }

    public async Task<bool> LinkEnderecoAsync(int contatoId, int enderecoId)
    {
        var c = await repo.GetByIdAsync(contatoId); if (c is null) return false;
        var e = await enderecos.GetByIdAsync(enderecoId); if (e is null) throw new ArgumentException("Endereço inexistente.");

        c.EnderecoId = enderecoId;
        await repo.UpdateAsync(c); await repo.SaveChangesAsync(); return true;
    }

    public async Task<bool> UnlinkEnderecoAsync(int contatoId)
    {
        var c = await repo.GetByIdAsync(contatoId); if (c is null) return false;
        c.EnderecoId = null; await repo.UpdateAsync(c); await repo.SaveChangesAsync(); return true;
    }

    private static ContatoDto Map(Contato c)
    {
        var resumo = c.Endereco is null ? null :
            $"{c.Endereco.Logradouro}, {c.Endereco.Numero} - {c.Endereco.Cidade}/{c.Endereco.Estado}";
        return new(c.Id, c.Tipo.ToString(), c.Ddd, c.Telefone, c.EnderecoId, resumo);
    }
}
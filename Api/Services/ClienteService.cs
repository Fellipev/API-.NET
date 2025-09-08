using Api.Dtos;
using Api.Models;
using Api.Repositories;

namespace Api.Services;

public class ClienteService(IClienteRepository repo, IContatoRepository contatos) : IClienteService
{
    public async Task<(IEnumerable<ClienteDto> items, int total)> SearchAsync(string? nome, string? email, string? cpf)
    {
        var (items, total) = await repo.SearchAsync(nome, email, cpf);
        return (items.Select(Map), total);
    }

    public async Task<ClienteDto?> GetAsync(int id) => (await repo.GetByIdAsync(id)) is { } c ? Map(c) : null;

    public async Task<ClienteDto> CreateAsync(CreateClienteDto input)
    {
        var c = new Cliente
        {
            Nome = input.Nome,
            Email = input.Email,
            Cpf = OnlyDigits(input.Cpf),
            Rg = NormalizeRg(input.Rg),
            ContatoId = null
        };
        await repo.AddAsync(c); await repo.SaveChangesAsync();
        return Map(c);
    }

    public async Task<bool> UpdateAsync(int id, UpdateClienteDto input)
    {
        var c = await repo.GetByIdAsync(id); if (c is null) return false;
        c.Nome = input.Nome; c.Email = input.Email; c.Cpf = OnlyDigits(input.Cpf); c.Rg = NormalizeRg(input.Rg);
        await repo.UpdateAsync(c); await repo.SaveChangesAsync(); return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var c = await repo.GetByIdAsync(id); if (c is null) return false;
        await repo.DeleteAsync(c); await repo.SaveChangesAsync(); return true;
    }

    public async Task<bool> LinkContatoAsync(int clienteId, int contatoId)
    {
        var cli = await repo.GetByIdAsync(clienteId); if (cli is null) return false;
        var ct  = await contatos.GetByIdAsync(contatoId); if (ct is null) throw new ArgumentException("Contato inexistente.");
        
        cli.ContatoId = contatoId;
        await repo.UpdateAsync(cli); await repo.SaveChangesAsync(); return true;
    }

    public async Task<bool> UnlinkContatoAsync(int clienteId)
    {
        var cli = await repo.GetByIdAsync(clienteId); if (cli is null) return false;
        cli.ContatoId = null; await repo.UpdateAsync(cli); await repo.SaveChangesAsync(); return true;
    }

    private static string OnlyDigits(string s) => new string((s ?? "").Where(char.IsDigit).ToArray());
    private static string? NormalizeRg(string? s) => string.IsNullOrWhiteSpace(s) ? null : new string(s.Where(char.IsLetterOrDigit).ToArray()).ToUpperInvariant();

    private static ClienteDto Map(Cliente c)
    {
        string? tel = null, end = null;
        if (c.Contato is not null)
        {
            if (c.Contato.Ddd > 0 && c.Contato.Telefone > 0) tel = $"+55{c.Contato.Ddd}{c.Contato.Telefone:0}";
            var e = c.Contato.Endereco;
            if (e is not null) end = $"{e.Logradouro}, {e.Numero} - {e.Cidade}/{e.Estado}";
        }
        return new(c.Id, c.Nome, c.Email, c.Cpf, c.Rg, c.ContatoId, tel, end);
    }
}

using Api.Models;

namespace Api.Repositories;

public interface IClienteRepository
{
    Task<(IEnumerable<Cliente> items, int total)> SearchAsync(string? nome, string? email, string? cpf);
    Task<Cliente?> GetByIdAsync(int id);
    Task AddAsync(Cliente c);
    Task UpdateAsync(Cliente c);
    Task DeleteAsync(Cliente c);
    Task SaveChangesAsync();
}

using Api.Models;

namespace Api.Repositories;

public interface IContatoRepository
{
    Task<IEnumerable<Contato>> GetAllAsync();
    Task<Contato?> GetByIdAsync(int id);
    Task AddAsync(Contato c);
    Task UpdateAsync(Contato c);
    Task DeleteAsync(Contato c);
    Task SaveChangesAsync();
}

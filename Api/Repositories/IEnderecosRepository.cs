using Api.Models;

namespace Api.Repositories;

public interface IEnderecoRepository
{
    Task<IEnumerable<Endereco>> GetAllAsync();
    Task<Endereco?> GetByIdAsync(int id);
    Task AddAsync(Endereco e);
    Task UpdateAsync(Endereco e);
    Task DeleteAsync(Endereco e);
    Task SaveChangesAsync();
}

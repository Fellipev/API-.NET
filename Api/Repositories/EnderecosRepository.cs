using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class EnderecoRepository(AppDbContext db) : IEnderecoRepository
{
    public async Task<IEnumerable<Endereco>> GetAllAsync() => await db.Enderecos.AsNoTracking().ToListAsync();
    public Task<Endereco?> GetByIdAsync(int id) => db.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
    public async Task AddAsync(Endereco e) => await db.Enderecos.AddAsync(e);
    public Task UpdateAsync(Endereco e) { db.Enderecos.Update(e); return Task.CompletedTask; }
    public Task DeleteAsync(Endereco e) { db.Enderecos.Remove(e); return Task.CompletedTask; }
    public Task SaveChangesAsync() => db.SaveChangesAsync();
}

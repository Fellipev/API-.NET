using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class ContatoRepository(AppDbContext db) : IContatoRepository
{
    public async Task<IEnumerable<Contato>> GetAllAsync() =>
        await db.Contatos.Include(c => c.Endereco).AsNoTracking().ToListAsync();

    public Task<Contato?> GetByIdAsync(int id) =>
        db.Contatos.Include(c => c.Endereco).FirstOrDefaultAsync(c => c.Id == id);

    public async Task AddAsync(Contato c) => await db.Contatos.AddAsync(c);
    public Task UpdateAsync(Contato c) { db.Contatos.Update(c); return Task.CompletedTask; }
    public Task DeleteAsync(Contato c) { db.Contatos.Remove(c); return Task.CompletedTask; }
    public Task SaveChangesAsync() => db.SaveChangesAsync();
}

using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class ClienteRepository(AppDbContext db) : IClienteRepository
{
    public async Task<(IEnumerable<Cliente> items, int total)> SearchAsync(string? nome, string? email, string? cpf)
    {
        var q = db.Clientes.Include(c => c.Contato).ThenInclude(ct => ct.Endereco).AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))  q = q.Where(c => EF.Functions.Like(c.Nome, $"%{nome}%"));
        if (!string.IsNullOrWhiteSpace(email)) q = q.Where(c => c.Email == email);
        if (!string.IsNullOrWhiteSpace(cpf))
        {
            var only = new string(cpf.Where(char.IsDigit).ToArray());
            q = q.Where(c => c.Cpf == only);
        }

        q = q.OrderBy(c => c.Nome).AsNoTracking();
        var total = await q.CountAsync();
        return (await q.ToListAsync(), total);
    }

    public Task<Cliente?> GetByIdAsync(int id) =>
        db.Clientes.Include(c => c.Contato).ThenInclude(ct => ct.Endereco)
           .FirstOrDefaultAsync(c => c.Id == id);

    public async Task AddAsync(Cliente c) => await db.Clientes.AddAsync(c);
    public Task UpdateAsync(Cliente c) { db.Clientes.Update(c); return Task.CompletedTask; }
    public Task DeleteAsync(Cliente c) { db.Clientes.Remove(c); return Task.CompletedTask; }
    public Task SaveChangesAsync() => db.SaveChangesAsync();
}

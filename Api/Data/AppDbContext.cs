using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Endereco> Enderecos => Set<Endereco>();
    public DbSet<Contato>  Contatos  => Set<Contato>();
    public DbSet<Cliente>  Clientes  => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Endereco>(e =>
        {
            e.Property(x => x.Tipo).HasConversion<string>().HasMaxLength(20).IsRequired();
            e.Property(x => x.Cep).HasMaxLength(9).IsRequired();

            e.HasIndex(x => new { x.Cep, x.Logradouro, x.Numero }).IsUnique();
        });

        mb.Entity<Contato>(c =>
        {
            c.Property(x => x.Tipo).HasConversion<string>().HasMaxLength(20).IsRequired();
            c.Property(x => x.Telefone).HasColumnType("decimal(15,0)");

            c.HasOne(x => x.Endereco)
             .WithOne(e => e.Contato)
             .HasForeignKey<Contato>(x => x.EnderecoId)
             .IsRequired(false);

            c.HasIndex(x => x.EnderecoId).IsUnique();
        });

        mb.Entity<Cliente>(c =>
        {
            c.HasIndex(x => x.Email).IsUnique();
            c.HasIndex(x => x.Cpf).IsUnique();

            c.HasOne(x => x.Contato)
             .WithOne(y => y.Cliente)
             .HasForeignKey<Cliente>(x => x.ContatoId)
             .IsRequired(false);

            c.HasIndex(x => x.ContatoId).IsUnique();
        });
    }
}

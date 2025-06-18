using Microsoft.EntityFrameworkCore;
using RegistroDeTecnicos.Components.Model;

namespace RegistroDeTecnicos.Components.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Tecnicos> Tecnicos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public object Sistema { get; internal set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.Nombres)
            .IsUnique();

        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.Rnc)
            .IsUnique();

        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Tecnico)
            .WithMany()
            .HasForeignKey(c => c.TecnicoId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}

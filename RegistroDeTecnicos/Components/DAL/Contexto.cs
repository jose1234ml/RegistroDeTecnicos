using Microsoft.EntityFrameworkCore;
using RegistroDeTecnicos.Components.Model; // Asegúrate de que todos tus modelos están aquí

namespace RegistroDeTecnicos.Components.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        // Definición de tus DbSet
        public DbSet<Tecnicos> Tecnicos { get; set; } = default!;
        public DbSet<Cliente> Clientes { get; set; } = default!;
        public DbSet<Ticket> Tickets { get; set; } = default!;
        public DbSet<Sistema> Sistemas { get; set; } = default!;
        public DbSet<Venta> Ventas { get; set; } = default!;
        public DbSet<VentaDetalle> VentaDetalles { get; set; } = default!; // Corregido a VentaDetalles (plural)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones de Cliente
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
                .IsRequired(false) // Permite que TecnicoId sea NULL
                .OnDelete(DeleteBehavior.Restrict);

            // Configuraciones de Venta y VentaDetalle
            modelBuilder.Entity<VentaDetalle>()
                .HasOne<Venta>(vd => vd.Venta)
                .WithMany(v => v.Detalles)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación de VentaDetalle con Sistema
            modelBuilder.Entity<VentaDetalle>()
                .HasOne<Sistema>(vd => vd.Sistema)
                .WithMany()
                .HasForeignKey(d => d.SistemaId)
                .OnDelete(DeleteBehavior.Restrict); // O .Cascade si la eliminación de un Sistema debe eliminar sus detalles de venta
        }
    }
}
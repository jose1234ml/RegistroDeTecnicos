using Microsoft.EntityFrameworkCore;
using RegistroDeTecnicos.Components.Model;

namespace RegistroDeTecnicos.Components.DAL
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options): base(options) { }

        public DbSet<Tecnicos> Tecnicos { get; set; }
    }
}

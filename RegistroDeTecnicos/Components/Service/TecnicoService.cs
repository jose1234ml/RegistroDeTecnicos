using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroDeTecnicos.Components.DAL;
using RegistroDeTecnicos.Components.Model;

namespace RegistroDeTecnicos.Components.Service
{
    public class TecnicoService
    {
        private readonly IDbContextFactory<Contexto> DbFactory;

        public TecnicoService(IDbContextFactory<Contexto> dbFactory)
        {
            DbFactory = dbFactory;
        }

        // Guardar: Inserta o modifica dependiendo de si el técnico existe
        public async Task<bool> Guardar(Tecnicos tecnicos)
        {
            if (!await ExisteId(tecnicos.TecnicoId))
            {
                return await InsertarTecnico(tecnicos);
            }
            else
            {
                return await ModificarTecnico(tecnicos);
            }
        }

        // Verifica si el ID de un técnico ya existe
        public async Task<bool> ExisteId(int TecnicoId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Tecnicos.AnyAsync(t => t.TecnicoId == TecnicoId);
        }

        // Verifica si ya existe un técnico con el mismo nombre
        public async Task<bool> ExisteNombre(string TecnicoNombre)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Tecnicos.AnyAsync(n => n.NombreTecnico == TecnicoNombre);
        }

        // Insertar un nuevo técnico en la base de datos
        public async Task<bool> InsertarTecnico(Tecnicos tecnicos)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            context.Tecnicos.Add(tecnicos);
            return await context.SaveChangesAsync() > 0;
        }

        // Buscar un técnico por su ID
        public async Task<Tecnicos?> Buscar(int TecnicoId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Tecnicos.FirstOrDefaultAsync(t => t.TecnicoId == TecnicoId);
        }

        // Modificar los detalles de un técnico existente
        public async Task<bool> ModificarTecnico(Tecnicos tecnicos)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            context.Update(tecnicos);
            return await context.SaveChangesAsync() > 0;
        }

        // Listar técnicos basados en un criterio específico
        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Tecnicos.Where(criterio).AsNoTracking().ToListAsync();
        }

        // Eliminar un técnico de la base de datos por su ID
        public async Task<bool> Eliminar(int TecnicoId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            var tecnico = await context.Tecnicos.FindAsync(TecnicoId);
            if (tecnico == null) return false;

            context.Tecnicos.Remove(tecnico);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
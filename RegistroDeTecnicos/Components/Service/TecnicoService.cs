using Microsoft.EntityFrameworkCore;
using RegistroDeTecnicos.Components.DAL;
using RegistroDeTecnicos.Components.Model;
using System.Linq.Expressions;

namespace RegistroDeTecnicos.Components.Service
{
    public class TecnicosService
    {
        private readonly IDbContextFactory<Contexto> _contextFactory;

        public TecnicosService(IDbContextFactory<Contexto> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> GuardarAsync(Tecnicos tecnico)
        {
            if (await ExisteAsync(tecnico.TecnicoId))
                return await ModificarAsync(tecnico);
            else
                return await InsertarAsync(tecnico);
        }

        public async Task<bool> ExisteAsync(int tecnicoId)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.Tecnicos.AnyAsync(t => t.TecnicoId == tecnicoId);
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.Tecnicos.AnyAsync(t => t.NombreTecnico.ToLower() == nombre.ToLower());
        }

        private async Task<bool> InsertarAsync(Tecnicos tecnico)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            db.Tecnicos.Add(tecnico);
            return await db.SaveChangesAsync() > 0;
        }

        private async Task<bool> ModificarAsync(Tecnicos tecnico)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            db.Tecnicos.Update(tecnico);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<Tecnicos?> BuscarAsync(int tecnicoId)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.Tecnicos.FirstOrDefaultAsync(t => t.TecnicoId == tecnicoId);
        }

        public async Task<bool> EliminarAsync(int tecnicoId)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.Tecnicos
                           .Where(t => t.TecnicoId == tecnicoId)
                           .AsNoTracking()
                           .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Tecnicos>> ListarAsync(Expression<Func<Tecnicos, bool>> criterio)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.Tecnicos
                           .Where(criterio)
                           .AsNoTracking()
                           .ToListAsync();
        }
    }
}
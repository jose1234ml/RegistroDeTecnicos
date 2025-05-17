﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroDeTecnicos.Components.DAL;
using RegistroDeTecnicos.Components.Model;

namespace RegistroDeTecnicos.Components.Service
{
    public class TecnicoService(IDbContextFactory<Contexto> DbFactory)
    {

        public async Task<bool> Guardar(Tecnicos tecnicos)
        {
            if (!await ExisteId(tecnicos.TecnicoId))
            { return await InsertarTecnico(tecnicos); }

            else { return await ModificarTecnico(tecnicos); }

        }


        public async Task<bool> ExisteId(int TecnicoId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Tecnicos.AnyAsync(t => t.TecnicoId == TecnicoId);
        }

        public async Task<bool> ExisteNombre(string TecnicoNombre)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Tecnicos.AnyAsync(n => n.NombreTecnico == TecnicoNombre);

        }
        public async Task<bool> InsertarTecnico(Tecnicos tecnicos)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            context.Tecnicos.Add(tecnicos);
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<Tecnicos?> Buscar(int TecnicoId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Tecnicos.FirstOrDefaultAsync(t => t.TecnicoId == TecnicoId);
        }

        public async Task<bool> ModificarTecnico(Tecnicos tecnicos)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            context.Update(tecnicos);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Tecnicos.Where(criterio).AsNoTracking().ToListAsync();
        }



        public async Task<bool> Eliminar(int TecnicoId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Tecnicos.AsNoTracking().Where(i => i.TecnicoId == TecnicoId).ExecuteDeleteAsync() > 0;
        }



    }
}

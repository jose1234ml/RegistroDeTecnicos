﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroDeTecnicos.Components.DAL;
using RegistroDeTecnicos.Components.Model; 

namespace RegistroDeTecnicos.Components.Service;


public class PaginacionResultado<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int TotalElementos { get; set; }
}
public class TecnicoService
{
    private readonly IDbContextFactory<Contexto> DbFactory;
    public TecnicoService(IDbContextFactory<Contexto> dbFactory)
    {
        DbFactory = dbFactory;
    }
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

    public async Task<PaginacionResultado<Tecnicos>> ListarConPaginacion(
        int pageNumber,
        int pageSize,
        Expression<Func<Tecnicos, bool>>? criterio = null)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        IQueryable<Tecnicos> query = context.Tecnicos.AsNoTracking();
        if (criterio != null)
        {
            query = query.Where(criterio);
        }
        int totalElementos = await query.CountAsync();

        List<Tecnicos> items = await query
            .Skip((pageNumber - 1) * pageSize) 
            .Take(pageSize) 
            .ToListAsync();

        return new PaginacionResultado<Tecnicos>
        {
            Items = items,
            TotalElementos = totalElementos
        };
    }
    public async Task<bool> Eliminar(int TecnicoId)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        var tecnico = await context.Tecnicos.FindAsync(TecnicoId);
        if (tecnico == null) return false;

        context.Tecnicos.Remove(tecnico);
        return await context.SaveChangesAsync() > 0;
    }
    public async Task<List<Tecnicos>> ListarTecnicos()
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        return await context.Tecnicos.AsNoTracking().ToListAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using RegistroDeTecnicos.Components.DAL;
using System.Linq.Expressions;

public class SistemaService
{
    private readonly Contexto _context;

    public SistemaService(Contexto context)
    {
        _context = context;
    }

    public async Task<List<Sistema>> Listar()
    {
        return await _context.Sistemas.ToListAsync();
    }
    public async Task<List<Sistema>> Listar(Expression<Func<Sistema, bool>> filtro)
    {
        return await _context.Sistemas
            .Where(filtro)
            .ToListAsync();
    }
    public async Task<List<Sistema>> ObtenerSistemas()
    {
        return await _context.Sistemas.ToListAsync();
    }
    public async Task<Sistema?> Buscar(int sistemaId)
    {
        return await _context.Sistemas.FindAsync(sistemaId);
    }
    public async Task<bool> Guardar(Sistema sistema)
    {
        if (await ExisteDescripcion(sistema.Descripcion))
            return false;

        if (sistema.Existencia < 0)
            sistema.Existencia = 0;

        _context.Sistemas.Add(sistema);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> Actualizar(Sistema sistema)
    {
        var existente = await Buscar(sistema.SistemaId);
        if (existente is null) return false;

        existente.Descripcion = sistema.Descripcion;
        existente.Complejidad = sistema.Complejidad;
        existente.Existencia = sistema.Existencia;

        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> Eliminar(int sistemaId)
    {
        var sistema = await Buscar(sistemaId);
        if (sistema is null) return false;

        _context.Sistemas.Remove(sistema);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> ExisteDescripcion(string descripcion)
    {
        return await _context.Sistemas
            .AnyAsync(s => s.Descripcion.ToLower() == descripcion.ToLower());
    }
    public async Task<bool> ActualizarExistencia(int sistemaId, int cantidad)
    {
        var sistema = await Buscar(sistemaId);
        if (sistema == null || cantidad < 0) return false;

        sistema.Existencia += cantidad;
        if (sistema.Existencia < 0)
            sistema.Existencia = 0;

        return await _context.SaveChangesAsync() > 0;
    }
}

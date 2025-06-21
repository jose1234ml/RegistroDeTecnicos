using Microsoft.EntityFrameworkCore;
using RegistroDeTecnicos.Components.DAL;
using RegistroDeTecnicos.Components.Model;
using System.Linq.Expressions;

namespace RegistroDeTecnicos.Components.Services;

public class VentaService
{
    private readonly Contexto _contexto;
    private readonly SistemaService _sistemaService;

    public VentaService(Contexto contexto, SistemaService sistemaService)
    {
        _contexto = contexto;
        _sistemaService = sistemaService;
    }

    public async Task<bool> Existe(int id)
    {
        return await _contexto.Ventas.AnyAsync(v => v.VentaId == id);
    }

    public async Task<bool> GuardarVentaAsync(Venta venta)
    {
        bool exito = false;
        using var transaction = await _contexto.Database.BeginTransactionAsync();
        try
        {
            if (venta.VentaId == 0)
            {
                _contexto.Ventas.Add(venta);
            }
            else
            {
                _contexto.Entry(venta).State = EntityState.Modified;

                var detallesActualesDb = await _contexto.VentaDetalles
                                                         .Where(vd => vd.VentaId == venta.VentaId)
                                                         .AsNoTracking()
                                                         .ToListAsync();

                foreach (var detalleDb in detallesActualesDb)
                {
                    var detalleNuevo = venta.Detalles.FirstOrDefault(d => d.VentaDetalleId == detalleDb.VentaDetalleId);

                    if (detalleNuevo == null)
                    {
                        var sistemaAumentar = await _sistemaService.Buscar(detalleDb.SistemaId);
                        if (sistemaAumentar != null)
                        {
                            sistemaAumentar.Existencia += detalleDb.Cantidad;
                            await _sistemaService.Guardar(sistemaAumentar);
                        }
                        _contexto.Entry(detalleDb).State = EntityState.Deleted;
                    }
                    else if (detalleNuevo.Cantidad != detalleDb.Cantidad)
                    {
                        var sistemaAjustar = await _sistemaService.Buscar(detalleDb.SistemaId);
                        if (sistemaAjustar != null)
                        {
                            sistemaAjustar.Existencia += detalleDb.Cantidad;
                            sistemaAjustar.Existencia -= detalleNuevo.Cantidad;
                            await _sistemaService.Guardar(sistemaAjustar);
                        }
                        _contexto.Entry(detalleNuevo).State = EntityState.Modified;
                    }
                }
            }

            foreach (var detalleNuevo in venta.Detalles)
            {
                detalleNuevo.VentaId = venta.VentaId;

                if (detalleNuevo.VentaDetalleId == 0)
                {
                    _contexto.Entry(detalleNuevo).State = EntityState.Added;
                    var sistemaAfectado = await _sistemaService.Buscar(detalleNuevo.SistemaId);
                    if (sistemaAfectado != null)
                    {
                        sistemaAfectado.Existencia -= detalleNuevo.Cantidad;
                        await _sistemaService.Guardar(sistemaAfectado);
                    }
                }
            }

            await _contexto.SaveChangesAsync();
            await transaction.CommitAsync();
            exito = true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error al guardar la venta y actualizar existencia: {ex.Message}");
            exito = false;
        }
        return exito;
    }

    public async Task<Venta?> Buscar(int id)
    {
        return await _contexto.Ventas
            .Include(v => v.Detalles)
            .ThenInclude(vd => vd.Sistema)
            .Include(v => v.Cliente)
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.VentaId == id);
    }

    public async Task<Venta?> BuscarVentaConDetalles(int id)
    {
        return await _contexto.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Detalles)
            .ThenInclude(d => d.Sistema)
            .FirstOrDefaultAsync(v => v.VentaId == id);
    }

    public async Task<bool> ActualizarVentaAsync(Venta venta)
    {
        return await GuardarVentaAsync(venta);
    }

    public async Task<List<Venta>> GetList(Expression<Func<Venta, bool>> criterio)
    {
        return await _contexto.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Detalles)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync() ?? new List<Venta>();
    }

    public async Task<bool> Eliminar(int id)
    {
        bool exito = false;
        using var transaction = await _contexto.Database.BeginTransactionAsync();
        try
        {
            var venta = await _contexto.Ventas
                                       .Include(v => v.Detalles)
                                       .FirstOrDefaultAsync(v => v.VentaId == id);
            if (venta == null)
            {
                return false;
            }

            foreach (var detalle in venta.Detalles)
            {
                var sistema = await _sistemaService.Buscar(detalle.SistemaId);
                if (sistema != null)
                {
                    sistema.Existencia += detalle.Cantidad;
                    await _sistemaService.Guardar(sistema);
                }
            }

            _contexto.Ventas.Remove(venta);
            await _contexto.SaveChangesAsync();
            await transaction.CommitAsync();
            exito = true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error al eliminar la venta y revertir existencia: {ex.Message}");
            exito = false;
        }
        return exito;
    }
}

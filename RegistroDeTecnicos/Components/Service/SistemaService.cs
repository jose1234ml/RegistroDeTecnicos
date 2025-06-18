using RegistroDeTecnicos.Components.Model;
using System.Linq;

public class SistemaService
{
    private List<Sistema> sistemas = new();

    public async Task<List<Sistema>> ObtenerSistemas()
    {
        await Task.Delay(100);
        return sistemas;
    }

    public async Task<Sistema?> Buscar(int sistemaId)
    {
        await Task.Delay(100);
        return sistemas.FirstOrDefault(s => s.SistemaId == sistemaId);
    }

    public async Task<bool> Actualizar(Sistema sistema)
    {
        await Task.Delay(100);
        var existente = sistemas.FirstOrDefault(s => s.SistemaId == sistema.SistemaId);
        if (existente is null) return false;

        existente.Descripcion = sistema.Descripcion;
        existente.Complejidad = sistema.Complejidad;
        return true;
    }

    public async Task<bool> Eliminar(int sistemaId)
    {
        var sistema = await Buscar(sistemaId);
        if (sistema is null) return false;

        sistemas.Remove(sistema);
        return true;
    }
    public async Task<bool> ExisteDescripcion(string descripcion)
    {
        await Task.Delay(100);
        return sistemas.Any(s => s.Descripcion.Equals(descripcion, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<bool> Guardar(Sistema sistema)
    {
        if (await ExisteDescripcion(sistema.Descripcion)) return false;

        sistema.SistemaId = sistemas.Any() ? sistemas.Max(s => s.SistemaId) + 1 : 1;

        await Task.Delay(100);
        sistemas.Add(sistema);
        return true;
    }
}

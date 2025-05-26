using Microsoft.EntityFrameworkCore;
using RegistroDeTecnicos.Components.DAL;
using RegistroDeTecnicos.Components.Model;
using System.Linq.Expressions;

namespace RegistroDeTecnicos.Components.Service
{
    public class ClienteService
    {
        private readonly Contexto contexto;

        public ClienteService(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> ExisteNombreO_Rnc(string nombre, string rnc)
        {
            return await contexto.Clientes.AnyAsync(c => c.Nombres == nombre || c.Rnc == rnc);
        }

        public async Task<List<Cliente>> ListarConTecnico()
        {
            return await contexto.Clientes
                .Include(c => c.Tecnico)
                .ToListAsync();
        }
        public async Task<List<Cliente>> ListarConTecnico(Expression<Func<Cliente, bool>> criterio)
        {
            return await contexto.Clientes
                .Include(c => c.Tecnico)
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<Cliente?> Guardar(Cliente cliente)
        {
            if (cliente.ClienteId == 0)
                contexto.Clientes.Add(cliente);
            else
                contexto.Clientes.Update(cliente);

            await contexto.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente?> Buscar(int clienteId)
        {
            return await contexto.Clientes
                .Include(c => c.Tecnico)
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
        }

        public async Task<Cliente?> Eliminar(int clienteId)
        {
            var cliente = await contexto.Clientes.FindAsync(clienteId);
            if (cliente != null)
            {
                contexto.Clientes.Remove(cliente);
                await contexto.SaveChangesAsync();
                return cliente;
            }
            return null;
        }
    }
}
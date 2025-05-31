using Microsoft.EntityFrameworkCore;
using RegistroDeTecnicos.Components.DAL;
using RegistroDeTecnicos.Components.Model;
using System.Linq.Expressions;

namespace RegistroDeTecnicos.Components.Service;

public class TicketService
{
    private readonly Contexto _contexto;

    public TicketService(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> ExisteAsuntoOPrioridad(string asunto, string prioridad)
    {
        return await _contexto.Tickets
            .AnyAsync(t => t.Asunto == asunto || t.Prioridad == prioridad);
    }

    public async Task<List<Ticket>> Listar()
    {
        return await _contexto.Tickets
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .ToListAsync();
    }

    public async Task<List<Ticket>> Listar(Expression<Func<Ticket, bool>> criterio)
    {
        return await _contexto.Tickets
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .Where(criterio)
            .ToListAsync();
    }
    public async Task<Ticket?> Buscar(int ticketId)
    {
        return await _contexto.Tickets
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .FirstOrDefaultAsync(t => t.TicketId == ticketId);
    }
    public async Task<Ticket?> Guardar(Ticket ticket)
    {
        if (ticket.TicketId == 0)
        {
            _contexto.Tickets.Add(ticket);
        }
        else
        {
            _contexto.Entry(ticket).State = EntityState.Modified;
        }

        await _contexto.SaveChangesAsync();
        return ticket;
    }
    public async Task<Ticket?> Eliminar(int ticketId)
    {
        var ticket = await _contexto.Tickets.FindAsync(ticketId);
        if (ticket != null)
        {
            _contexto.Tickets.Remove(ticket);
            await _contexto.SaveChangesAsync();
            return ticket;
        }
        return null;
    }
    public async Task<List<Ticket>> ObtenerTickets()
    {
        return await _contexto.Tickets
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .ToListAsync();
    }
}

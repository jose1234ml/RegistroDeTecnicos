namespace RegistroDeTecnicos.Components.Model
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public string Prioridad { get; set; }
        public DateTime Fecha { get; set; }

        public int TiempoInvertido { get; set; } 

    public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int TecnicoId { get; set; }
        public Tecnicos Tecnico { get; set; }
    }
}

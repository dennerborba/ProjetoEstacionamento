using ControleEstacionamento.Utils.Entidades;

namespace ControleEstacionamento.Models.Tickets
{
    public class TicketModel
    {
        public long ID { get; set; }
        public string Placa { get; set; }
        public DateTime? Hora_chegada { get; set; }
        public DateTime? Hora_saida { get; set; }
        public decimal Preco {  get; set; }

        public TicketModel()
        {
            
        }

        public TicketModel(Ticket ticket)
        {
            ID = ticket.ID;
            Placa = ticket.Placa;
            Hora_chegada = ticket.Hora_chegada;
            Hora_saida = ticket.Hora_saida;
            Preco = ticket.Preco;
        }

        public Ticket GetEntidade()
        {
            return new Ticket()
            {
                ID = ID,
                Placa = Placa,
                Hora_chegada = Hora_chegada,
                Hora_saida = Hora_saida,
                Preco = Preco
            };
        }

    }
}

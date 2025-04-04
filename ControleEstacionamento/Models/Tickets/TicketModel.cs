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
        public string Duracao
        {
            get
            {
                if (Hora_chegada.HasValue && Hora_saida.HasValue)
                {
                    TimeSpan duracao = Hora_saida.Value - Hora_chegada.Value;
                    return $"{(int)duracao.TotalHours}:{duracao.Minutes}:{duracao.Seconds}";
                }
            return "";
            }
        }
        public int TempoCobrado
        {
            get
            {
                if (Hora_chegada.HasValue && Hora_saida.HasValue)
                {
                    TimeSpan duracao = Hora_saida.Value - Hora_chegada.Value;
                    if (duracao.TotalMinutes <= 30)
                    {
                        return 1;
                    }
                    int horaCompleta = (int)(duracao.TotalMinutes / 60);
                    int tolerancia = (int)(duracao.TotalMinutes % 60);
                    if (tolerancia > 10)
                    {
                        horaCompleta += 1;
                    }
                    return horaCompleta;
                }
                return 0;
            }
        }

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

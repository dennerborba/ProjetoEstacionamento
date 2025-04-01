namespace ControleEstacionamento.Models.Ticket
{
    public class TicketModel
    {
        public long ID { get; set; }
        public string Placa_carro { get; set; }
        public DateTime? Hora_chegada { get; set; }
        public DateTime? Hora_saida { get; set; }
        public float Preco {  get; set; }

        public TicketModel()
        {
            
        }

    }
}

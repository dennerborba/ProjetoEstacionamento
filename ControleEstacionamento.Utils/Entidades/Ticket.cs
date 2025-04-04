using MySql.Data.MySqlClient;

namespace ControleEstacionamento.Utils.Entidades
{
    public class Ticket : EntidadeBase<Ticket>
    {
        protected override List<string> Fields => new List<string>()
        {
            "PLACA",
            "HR_CHEGADA",
            "HR_SAIDA",
            "PRECO"
        };

        protected override string TableName => "VAGA";

        public string Placa { get; set; }
        public DateTime? Hora_chegada { get; set; }
        public DateTime? Hora_saida { get; set; }
        public decimal Preco {  get; set; }

        protected override Ticket Fill(MySqlDataReader reader)
        {
            var aux = new Ticket();

            aux.ID = reader.GetInt32("ID");
            aux.Placa = reader.GetString("PLACA");
            aux.Hora_chegada = reader.GetDateTime("HR_CHEGADA");
            aux.Hora_saida = reader.IsDBNull(reader.GetOrdinal("HR_SAIDA")) ? (DateTime?)null : reader.GetDateTime("HR_SAIDA");
            aux.Preco = reader.IsDBNull(reader.GetOrdinal("PRECO")) ? 0.0m : reader.GetDecimal("PRECO");

            return aux;
        }

        protected override void FillParameters(MySqlParameterCollection parameters)
        {
            parameters.Add(new MySqlParameter("pPLACA", Placa));
            parameters.Add(new MySqlParameter("pHR_CHEGADA", Hora_chegada));
            parameters.Add(new MySqlParameter("pHR_SAIDA", Hora_saida));
            parameters.Add(new MySqlParameter("pPRECO", Preco));
        }

        public void MarcarEntrada(string placa)
        {
            var ticket = new Ticket
            {
                Placa = placa,
                Hora_chegada = DateTime.Now,
                Hora_saida = null,
                Preco = 0
            };
            ticket.Insert();
        }

        public void MarcarSaida(string placa)
        {
            var ticket = new Ticket().Get($"PLACA = '{placa}' AND HR_SAIDA IS NULL");

            if (ticket == null)
                throw new Exception("Veículo não encontrado.");

            ticket.Hora_saida = DateTime.Now;
            ticket.Preco = CalcularPreco(ticket.Hora_chegada.Value, ticket.Hora_saida.Value);

            ticket.Update(); 
        }

        private decimal CalcularPreco(DateTime entrada, DateTime saida)
        {
            TimeSpan duracao = saida - entrada;
            int anosPassados = entrada.Year > 2025 ? entrada.Year - 2025 : 0;

            decimal precoHoraInicial = 2.00m + anosPassados;
            decimal precoHoraAdicional = 1.00m + anosPassados;

            if (duracao.TotalMinutes <= 30)
            {
                return precoHoraInicial / 2;
            }            

            decimal total = precoHoraAdicional;
            int tempoAdicional = (int)Math.Ceiling((duracao.TotalMinutes - 60) / 60);

            if (duracao.TotalMinutes > 60 && (duracao.TotalMinutes - 60) % 60 <= 10)
            {
                tempoAdicional = Math.Max(0, tempoAdicional - 1);
            }

            total += tempoAdicional * precoHoraAdicional;
            return total;
        }
    }

}
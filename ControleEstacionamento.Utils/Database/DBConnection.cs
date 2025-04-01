using MySql.Data.MySqlClient;

namespace ControleEstacionamento.Utils.Database
{
    public class DBConnection
    {
        public const string CONNECTION_STRING = "Server=localhost;Database=estacionamento;User ID=root;Password=root;";
        
        public static bool TestarConexao()
        {
            try
            {
                using (MySqlConnection conn  = new MySqlConnection(CONNECTION_STRING))
                {
                    conn.Open();
                    return true;
                }
            } 
            catch
            {
                return false;
            }
        }
    }
}

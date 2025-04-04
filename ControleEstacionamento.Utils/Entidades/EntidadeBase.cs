using ControleEstacionamento.Utils.Database;
using MySql.Data.MySqlClient;

namespace ControleEstacionamento.Utils.Entidades
{
    public abstract class EntidadeBase<T>
    {
        public long ID { get; set; }
        protected abstract string TableName { get; }
        protected abstract List<string> Fields { get; }

        protected abstract T Fill(MySqlDataReader reader);
        protected abstract void FillParameters(MySqlParameterCollection parameters);

        public T Get(string condicao)
        {
            using (MySqlConnection conn = new MySqlConnection(DBConnection.CONNECTION_STRING))
            {
                conn.Open();
                var query = $"SELECT ID, {string.Join(", ", Fields)} FROM {TableName} WHERE {condicao}";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add(new MySqlParameter("ID", ID));

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return Fill(reader);
                }
            }

            return default(T);
        }

        public List<T> GetAll()
        {
            var result = new List<T>();

            using (MySqlConnection conn = new MySqlConnection(DBConnection.CONNECTION_STRING))
            {
                conn.Open();
                var query = $"SELECT ID, {string.Join(", ", Fields)} FROM {TableName}";
                var cmd = new MySqlCommand(query, conn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(Fill(reader));
                }
            }

            return result;
        }

        public void Insert()
        {
            using (MySqlConnection conn = new MySqlConnection(DBConnection.CONNECTION_STRING))
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = @$"INSERT INTO {TableName} ({string.Join(", ", Fields)}) 
                                        VALUES ({string.Join(", ", Fields.Select(campo => $"@p{campo}"))})";

                FillParameters(cmd.Parameters);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update()
        {
            using (MySqlConnection conn = new MySqlConnection(DBConnection.CONNECTION_STRING))
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = @$"UPDATE {TableName} SET {string.Join(", ", Fields.Select(campo => $"{campo} = @p{campo}"))}
                                   WHERE ID = @pID";

                cmd.Parameters.Add(new MySqlParameter("pID", ID));
                FillParameters(cmd.Parameters);

                cmd.ExecuteNonQuery();
            }
        }
    }
}

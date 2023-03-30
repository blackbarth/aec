using AeCAutomation.Domain.Entity;
using System.Data;
using System.Data.SQLite;

namespace AeCAutomation.Repository
{
    public class SearchRepository : ISearchRepository
    {
        string connectionString;

        public SearchRepository()
        {
            this.connectionString = "Data Source=AeCDatabase.db;Version=3;";
        }

        public void CriarBase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = "CREATE TABLE IF NOT EXISTS search_results (id INTEGER PRIMARY KEY, titulo TEXT, area TEXT, autor TEXT, descricao TEXT, data TEXT);";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

            }
        }

        public DataTable ListaBuscas()
        {

            using (var connection = new SQLiteConnection(connectionString))
            {
                string selectQuery = "SELECT * FROM search_results;";
                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public DataTable Pesquisa(Busca busca)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                string selectQuery = $"SELECT * FROM search_results WHERE titulo = '{busca.Titulo.Trim()}' and area = '{busca.Area.Trim()}' AND data = '{busca.Data.Trim()}';";
                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public void Salvar(Busca busca)
        {
      
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = "CREATE TABLE IF NOT EXISTS search_results (id INTEGER PRIMARY KEY, titulo TEXT, area TEXT, autor TEXT, descricao TEXT, data TEXT);";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                string insertQuery = "INSERT INTO search_results (Titulo, Area, Autor, Descricao, Data) VALUES(@Titulo, @Area, @Autor, @Descricao, @Data); ";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", busca.Titulo);
                    command.Parameters.AddWithValue("@Area", busca.Area);
                    command.Parameters.AddWithValue("@Autor", busca.Autor);
                    command.Parameters.AddWithValue("@Descricao", busca.Descricao);
                    command.Parameters.AddWithValue("@Data", busca.Data);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

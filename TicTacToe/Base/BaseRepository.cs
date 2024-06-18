using System.Data.SqlClient;

namespace TicTacToe.Base
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;
        public BaseRepository()
        {
            _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\TicTacToe.mdf;Integrated Security=True;Connect Timeout=30";
            //_connectionString = "data source=ADMIN\\ADMIN;initial catalog=TicTacToe;integrated security=True;trustservercertificate=True;";
        }
        protected SqlConnection GetConnection() => new SqlConnection(_connectionString);
    }
}

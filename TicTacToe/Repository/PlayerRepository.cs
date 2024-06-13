using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Net;
using TicTacToe.Base;
using TicTacToe.Model;

namespace TicTacToe.Repository
{
    public class PlayerRepository : BaseRepository, IPlayerRepository
    {
        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [Player] WHERE [username]=@username AND [password]=@password";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() != null;
            }

            return validUser;
        }
        public void AddPlayer(NetworkCredential credential)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Player([username], [password]) VALUES (@username, @password)";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = credential.Password;
                command.ExecuteScalar();
            }
        }

        public void UpdatePlayer(PlayerModel player)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE Player SET [username] = @username, [password] = @password WHERE [player_id] = @playerID";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = player.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = player.Password;
                command.Parameters.Add("@playerID", System.Data.SqlDbType.Int).Value = player.PlayerID;
                command.ExecuteNonQuery();
            }
        }

        public void DeletePlayer(PlayerModel player)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Player WHERE [player_id] = @playerID";
                command.Parameters.Add("@playerID", System.Data.SqlDbType.Int).Value = player.PlayerID;
                command.ExecuteNonQuery();
            }
        }

        public PlayerModel GetPlayer(PlayerModel player)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT [player_id], [username], [password] FROM Player WHERE [username] = @username AND [password] = @password";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = player.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = player.Password;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PlayerModel
                        {
                            PlayerID = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2)
                        };
                    }
                }
            }
            return null;
        }

        public ObservableCollection<PlayerModel> GetAllPlayers()
        {
            var players = new ObservableCollection<PlayerModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT [player_id], [username], [password] FROM Player";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        players.Add(new PlayerModel
                        {
                            PlayerID = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2)
                        });
                    }
                }
            }

            return players;
        }
    }
}

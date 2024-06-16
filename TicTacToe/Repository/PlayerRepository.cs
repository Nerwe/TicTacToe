using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using TicTacToe.Base;
using TicTacToe.Model;

namespace TicTacToe.Repository
{
    public class PlayerRepository : BaseRepository, IPlayerRepository
    {
        public bool AuthenticateUser(PlayerModel player)
        {
            bool validUser;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [Player] WHERE [username]=@username AND [password]=@password AND [is_active]=@isActive";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = player.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = player.Password;
                command.Parameters.Add("@isActive", System.Data.SqlDbType.Bit).Value = player.IsActive;
                validUser = command.ExecuteScalar() != null;
            }

            return validUser;
        }
        public void AddPlayer(PlayerModel player)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Player([username], [password]) VALUES (@username, @password)";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = player.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = player.Password;
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
                command.CommandText = "UPDATE Player SET [username] = @username, [password] = @password, [is_active] = @isActive WHERE [player_id] = @playerID";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = player.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = player.Password;
                command.Parameters.Add("@isActive", System.Data.SqlDbType.Bit).Value = player.IsActive;
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
                command.CommandText = "SELECT [player_id], [username], [password], [is_active] FROM Player WHERE [username] = @username AND [password] = @password AND [is_active] = @isActive ";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = player.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = player.Password;
                command.Parameters.Add("@isActive", System.Data.SqlDbType.Bit).Value = player.IsActive;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PlayerModel
                        {
                            PlayerID = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                            IsActive = reader.GetBoolean(3)
                        };
                    }
                }
            }
            return null;
        }

        public PlayerModel GetPlayerStats(int playerID)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("GetPlayerStats", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@playerID", playerID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PlayerModel
                        {
                            Wins = reader.GetInt32(0),
                            Losses = reader.GetInt32(1),
                            Draws = reader.GetInt32(2),
                            Rank = reader.GetInt32(3)
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
                command.CommandText = "SELECT [player_id], [username] FROM Player";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        players.Add(new PlayerModel
                        {
                            PlayerID = reader.GetInt32(0),
                            Username = reader.GetString(1)
                        });
                    }
                }
            }

            return players;
        }
        public ObservableCollection<PlayerModel> GetPlayersByRank()
        {
            var players = new ObservableCollection<PlayerModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec GetPlayersByRank";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        players.Add(new PlayerModel
                        {
                            Username = reader.GetString(0),
                            Wins = reader.GetInt32(1),
                            Losses = reader.GetInt32(2),
                            Draws = reader.GetInt32(3),
                            Rank = reader.GetInt32(4)
                        });
                    }
                }
            }

            return players;

        }
    }
}

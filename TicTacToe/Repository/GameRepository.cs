using System.Collections.ObjectModel;
using System.Data.SqlClient;
using TicTacToe.Base;
using TicTacToe.Model;

namespace TicTacToe.Repository
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        public void AddGame(GameModel game)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Games([player_id], [score], [date]) VALUES (@player_id, @score, @date)";
                command.Parameters.Add("@player_id", System.Data.SqlDbType.NVarChar).Value = game.PlayerID;
                command.Parameters.Add("@score", System.Data.SqlDbType.NVarChar).Value = game.Score;
                command.Parameters.Add("@date", System.Data.SqlDbType.NVarChar).Value = game.Date;
                command.ExecuteScalar();
            }
        }

        public ObservableCollection<GameModel> GetAllGames()
        {
            var games = new ObservableCollection<GameModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT [game_id], [player_id], [score], [date] FROM Games";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        games.Add(new GameModel
                        {
                            GameID = reader.GetInt32(0),
                            PlayerID = reader.GetInt32(1),
                            Score = reader.GetInt16(2),
                            Date = reader.GetDateTime(3)
                        });
                    }
                }
            }

            return games;
        }
        public ObservableCollection<GameModel> GetGamesByPlayer(int playerID)
        {
            var games = new ObservableCollection<GameModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT [game_id], [player_id], [score], [date] FROM Games WHERE [player_id] = @playerID";
                command.Parameters.Add("@player_id", System.Data.SqlDbType.Int).Value = playerID;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        games.Add(new GameModel
                        {
                            GameID = reader.GetInt32(0),
                            PlayerID = reader.GetInt32(1),
                            Score = reader.GetInt16(2),
                            Date = reader.GetDateTime(3)
                        });
                    }
                }
            }

            return games;
        }
    }
}

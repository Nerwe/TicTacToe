using System.Collections.ObjectModel;
using TicTacToe.Model;

namespace TicTacToe.Repository
{
    public interface IPlayerRepository
    {
        bool AuthenticateUser(PlayerModel player);
        void AddPlayer(PlayerModel player);
        void UpdatePlayer(PlayerModel player);
        void DeletePlayer(PlayerModel player);
        PlayerModel GetPlayer(PlayerModel player);
        PlayerModel GetPlayerStats(int playerID);
        ObservableCollection<PlayerModel> GetAllPlayers();
        ObservableCollection<PlayerModel> GetPlayersByRank();
    }
}

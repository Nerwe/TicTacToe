using System.Collections.ObjectModel;
using System.Net;
using TicTacToe.Model;

namespace TicTacToe.Repository
{
    public interface IPlayerRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void AddPlayer(NetworkCredential credential);
        void UpdatePlayer(PlayerModel player);
        void DeletePlayer(PlayerModel player);
        PlayerModel GetPlayer(PlayerModel player);
        ObservableCollection<PlayerModel> GetAllPlayers();
    }
}

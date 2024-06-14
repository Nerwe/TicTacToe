using System.Collections.ObjectModel;
using TicTacToe.Model;

namespace TicTacToe.Repository
{
    public interface IGameRepository
    {
        void AddGame(GameModel game);
        ObservableCollection<GameModel> GetAllGames();
        ObservableCollection<GameModel> GetGamesByPlayer(int playerID);
    }
}

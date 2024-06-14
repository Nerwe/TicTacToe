using TicTacToe.Base;
using TicTacToe.Repository;

namespace TicTacToe.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        //Fields
        private readonly MainViewModel _mainViewModel;
        private IPlayerRepository _playerRepository;
        private IGameRepository _gameRepository;

        //Properties


        //Commands

        public GameViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }


    }
}

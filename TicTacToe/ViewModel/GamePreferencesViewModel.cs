using System;
using System.Windows.Input;
using TicTacToe.Base;
using TicTacToe.Repository;

namespace TicTacToe.ViewModel
{
    public class GamePreferencesViewModel : BaseViewModel
    {
        //Fields
        private readonly MainViewModel _mainViewModel;
        private IPlayerRepository _playerRepository;
        private IGameRepository _gameRepository;

        //Properties


        //Commands
        public ICommand PlayerTopViewCommand { get; }
        public ICommand ProfileViewCommand { get; }
        public ICommand GameViewCommand { get; }

        public GamePreferencesViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            _playerRepository = new PlayerRepository();
            _gameRepository = new GameRepository();

            PlayerTopViewCommand = new ViewModelCommand(ExecutePlayerTopViewCommand);
            ProfileViewCommand = new ViewModelCommand(ExecuteProfileViewCommand);
            GameViewCommand = new ViewModelCommand(ExecuteGameViewCommand);
        }

        private void ExecutePlayerTopViewCommand(object obj)
        {
            _mainViewModel.ExecutePlayerTopViewCommand(null);
        }
        private void ExecuteProfileViewCommand(object obj)
        {
            _mainViewModel.ExecuteProfileViewCommand(null);
        }
        private void ExecuteGameViewCommand(object obj)
        {
            _mainViewModel.ExecuteGameViewCommand(null);
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TicTacToe.Base;
using TicTacToe.Model;
using TicTacToe.Repository;

namespace TicTacToe.ViewModel
{
    public class PlayerTopViewModel : BaseViewModel
    {
        //Fields
        private readonly MainViewModel _mainViewModel;
        private IPlayerRepository _playerRepository;
        private IGameRepository _gameRepository;

        private ObservableCollection<PlayerModel> _topPlayers;

        //Properties
        public ObservableCollection<PlayerModel> TopPlayers
        {
            get => _topPlayers;
            set
            {
                _topPlayers = value;
                OnPropertyChanged(nameof(TopPlayers));
            }
        }

        //Commands
        public ICommand ShowTopPlayersCommand { get; }
        public ICommand GamePreferencesViewCommand { get; }
        public ICommand ProfileViewCommand { get; }

        public PlayerTopViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _playerRepository = new PlayerRepository();
            _gameRepository = new GameRepository();

            TopPlayers = new ObservableCollection<PlayerModel>();

            ShowTopPlayersCommand = new ViewModelCommand(ExecuteShowTopPlayersCommand);
            GamePreferencesViewCommand = new ViewModelCommand(ExecuteGamePreferencesViewCommand);
            ProfileViewCommand = new ViewModelCommand(ExecuteProfileViewCommand);

            ExecuteShowTopPlayersCommand(null);
        }

        private void ExecuteShowTopPlayersCommand(object obj)
        {
            TopPlayers.Clear();
            var playerList = _playerRepository.GetPlayersByRank();
            foreach (var player in playerList)
            {
                TopPlayers.Add(player);
            }
        }

        private void ExecuteGamePreferencesViewCommand(object obj)
        {
            _mainViewModel.ExecuteGamePreferencesViewCommand(null);
        }

        private void ExecuteProfileViewCommand(object obj)
        {
            _mainViewModel.ExecuteProfileViewCommand(null);
        }
    }
}

using System.Windows.Input;
using TicTacToe.Base;
using TicTacToe.Helpers;
using TicTacToe.Model;
using TicTacToe.Repository;

namespace TicTacToe.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        //Fields
        private readonly MainViewModel _mainViewModel;
        private IPlayerRepository _playerRepository;
        private IGameRepository _gameRepository;

        private PlayerModel _currentPlayer;
        private PlayerModel _currentPlayerStats;

        //Properties
        public PlayerModel CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                _currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }
        public PlayerModel CurrentPlayerStats
        {
            get => _currentPlayerStats;
            set
            {
                _currentPlayerStats = value;
                OnPropertyChanged(nameof(CurrentPlayerStats));
            }
        }

        public ICommand ProfilePreferencesViewCommand { get; }
        public ICommand GamePreferencesViewCommand { get; }

        public ProfileViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _playerRepository = new PlayerRepository();

            CurrentPlayer = PlayerSession.Instance.CurrentPlayer;
            _currentPlayerStats = new PlayerModel();

            ProfilePreferencesViewCommand = new ViewModelCommand(ExecuteProfilePreferencesViewCommand);
            GamePreferencesViewCommand = new ViewModelCommand(ExecuteGamePreferencesViewCommand);

            LoadPlayerStats();
        }

        private void ExecuteProfilePreferencesViewCommand(object obj)
        {
            _mainViewModel.ExecuteProfilePreferencesViewCommand(null);
        }

        private void ExecuteGamePreferencesViewCommand(object obj)
        {
            _mainViewModel.ExecuteGamePreferencesViewCommand(null);
        }

        private void LoadPlayerStats()
        {
            CurrentPlayerStats = _playerRepository.GetPlayerStats(CurrentPlayer.PlayerID);

            CurrentPlayer.Wins = CurrentPlayerStats.Wins;
            CurrentPlayer.Losses = CurrentPlayerStats.Losses;
            CurrentPlayer.Draws = CurrentPlayerStats.Draws;
            CurrentPlayer.Rank = CurrentPlayerStats.Rank;

            PlayerSession.Instance.SetCurrentPlayer(CurrentPlayer);
        }
    }
}
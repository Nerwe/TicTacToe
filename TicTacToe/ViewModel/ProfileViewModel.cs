using System.Collections.ObjectModel;
using System.Linq;
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
        private ObservableCollection<GameModel> _playerGames;

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
        public ObservableCollection<GameModel> PlayerGames
        {
            get => _playerGames;
            set
            {
                _playerGames = value;
                OnPropertyChanged(nameof(PlayerGames));
            }
        }

        public ICommand ProfilePreferencesViewCommand { get; }
        public ICommand GamePreferencesViewCommand { get; }

        public ProfileViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _playerRepository = new PlayerRepository();
            _playerGames = new ObservableCollection<GameModel>();

            CurrentPlayer = PlayerSession.Instance.CurrentPlayer;

            ProfilePreferencesViewCommand = new ViewModelCommand(ExecuteProfilePreferencesViewCommand);
            GamePreferencesViewCommand = new ViewModelCommand(ExecuteGamePreferencesViewCommand);
        }

        private void ExecuteProfilePreferencesViewCommand(object obj)
        {
            _mainViewModel.ExecuteProfilePreferencesViewCommand(null);
        }

        private void ExecuteGamePreferencesViewCommand(object obj)
        {
            _mainViewModel.ExecuteGamePreferencesViewCommand(null);
        }

        private void LoadPlayerGames()
        {
            PlayerGames = _gameRepository.GetGamesByPlayer(CurrentPlayer.PlayerID);

            CurrentPlayer.Wins = PlayerGames.Count(game => game.Score == 1);
            CurrentPlayer.Losses = PlayerGames.Count(game => game.Score == 0);
            CurrentPlayer.Draws = PlayerGames.Count(game => game.Score == 2);
            CurrentPlayer.Rank = CurrentPlayer.Wins + CurrentPlayer.Draws;
        }
    }
}
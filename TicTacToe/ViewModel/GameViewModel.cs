using System.Collections.ObjectModel;
using System.Windows.Input;
using TicTacToe.Base;
using TicTacToe.Helpers;
using TicTacToe.Model;
using TicTacToe.Repository;
using TicTacToe.Services;

namespace TicTacToe.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        //Fields
        private readonly MainViewModel _mainViewModel;
        private IPlayerRepository _playerRepository;
        private IGameRepository _gameRepository;

        private PlayerModel _currentPlayer;

        private GamePreferences _gamePreferences;
        private Game _game;

        private bool _isGameRunning;

        private ObservableCollection<ObservableCollection<CellType>> _board;

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

        public GamePreferences GamePreferences
        {
            get => _gamePreferences;
            set
            {
                _gamePreferences = value;
                OnPropertyChanged(nameof(GamePreferences));
            }
        }

        public ObservableCollection<ObservableCollection<CellType>> Board
        {
            get => _board;
            set
            {
                _board = value;
                OnPropertyChanged(nameof(Board));
            }
        }

        //Commands
        public ICommand StartGameCommand { get; }
        public ICommand CellClickCommand { get; }
        public ICommand GamePreferencesViewCommand { get; }
        public ICommand ProfileViewCommand { get; }

        public GameViewModel(MainViewModel mainViewModel)
        {
            _isGameRunning = false;

            _mainViewModel = mainViewModel;

            _playerRepository = new PlayerRepository();
            _gameRepository = new GameRepository();

            CurrentPlayer = PlayerSession.Instance.CurrentPlayer;
            _gamePreferences = GameSettings.Instance.CurrentGame;

            Board = new ObservableCollection<ObservableCollection<CellType>>();

            StartGameCommand = new ViewModelCommand(ExecuteStartGameCommand, CanExecuteStartGameCommand);
            CellClickCommand = new ViewModelCommand(ExecuteCellClickCommand, CanExecuteCellClickCommand);
            GamePreferencesViewCommand = new ViewModelCommand(ExecuteGamePreferencesViewCommand, CanExecuteGamePreferencesViewCommand);
            ProfileViewCommand = new ViewModelCommand(ExecuteProfileViewCommand, CanExecuteProfileViewCommand);

            FillBoard();
        }

        //Checks
        private bool CanExecuteStartGameCommand(object obj)
        {
            return !_isGameRunning;
        }
        private bool CanExecuteCellClickCommand(object obj)
        {
            return _isGameRunning;
        }
        private bool CanExecuteGamePreferencesViewCommand(object obj)
        {
            return !_isGameRunning;
        }
        private bool CanExecuteProfileViewCommand(object obj)
        {
            return !_isGameRunning;
        }

        //Executes
        private void ExecuteStartGameCommand(object obj)
        {
            FillBoard();
            _game = new Game();
            _isGameRunning = true;
        }
        private void ExecuteGamePreferencesViewCommand(object obj)
        {
            _mainViewModel.ExecuteGamePreferencesViewCommand(null);
        }
        private void ExecuteProfileViewCommand(object obj)
        {
            _mainViewModel.ExecuteProfileViewCommand(null);
        }

        private void FillBoard()
        {
            Board.Clear();

            for (int i = 0; i < 3; i++)
            {
                var row = new ObservableCollection<CellType>();
                for (int j = 0; j < 3; j++)
                {
                    row.Add(CellType.Empty);
                }
                Board.Add(row);
            }
        }
        private void ExecuteCellClickCommand(object obj)
        {
            var param = (string)obj;

            int row = int.Parse(param[0].ToString());
            int col = int.Parse(param[1].ToString());

            if (Board[row][col] == CellType.Empty)
            {
                Board[row][col] = CellType.Cross;
                _game.MakeMove(row, col);

                if (!_game.IsGameOver())
                {
                    var move = _game.MakeBotMove();
                    Board[move.row][move.col] = CellType.Circle;

                    if (_game.IsGameOver())
                    {
                        _isGameRunning = false;
                    }
                }
                else
                {
                    _isGameRunning = false;
                }
            }

        }
    }
}

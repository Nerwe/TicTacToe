using System;
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
        private string _gameReuslt;

        private int _hintRow;
        private int _hintCol;

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
        public string GameResult
        {
            get => _gameReuslt;
            set
            {
                _gameReuslt = value;
                OnPropertyChanged(nameof(GameResult));
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
            _game = new Game();

            CurrentPlayer = PlayerSession.Instance.CurrentPlayer;
            GamePreferences = GameSettings.Instance.CurrentGame;

            Board = new ObservableCollection<ObservableCollection<CellType>>();

            StartGameCommand = new ViewModelCommand(ExecuteStartGameCommand, CanExecuteStartGameCommand);
            CellClickCommand = new ViewModelCommand(ExecuteCellClickCommand, CanExecuteCellClickCommand);
            GamePreferencesViewCommand = new ViewModelCommand(ExecuteGamePreferencesViewCommand, CanExecuteGamePreferencesViewCommand);
            ProfileViewCommand = new ViewModelCommand(ExecuteProfileViewCommand, CanExecuteProfileViewCommand);

            FillBoard();
            PlayerChooser();
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
        private void PlayerChooser()
        {
            Random random = new Random();
            bool isFirstMoveByBot = random.Next(2) == 0;

            if (isFirstMoveByBot)
            {
                _game.IsPlayerTurn = false;
            }
        }
        private void FillBoard()
        {
            var newBoard = new ObservableCollection<ObservableCollection<CellType>>();
            for (int i = 0; i < 3; i++)
            {
                var row = new ObservableCollection<CellType>();
                for (int j = 0; j < 3; j++)
                {
                    row.Add(CellType.Empty);
                }
                newBoard.Add(row);
            }
            Board = newBoard;
        }

        private void ExecuteStartGameCommand(object obj)
        {
            CurrentPlayer = PlayerSession.Instance.CurrentPlayer;

            GameResult = "";
            _game.RestartGame();
            _isGameRunning = true;

            FillBoard();

            if (!_game.IsPlayerTurn)
            {
                var move = _game.MakeBotMove("Noob");
                Board[move.row][move.col] = CellType.Circle;
            }
            if (_gamePreferences.Hint)
            {
                var hintMove = _game.HintMove();
                _hintRow = hintMove.row;
                _hintCol = hintMove.col;
                Board[_hintRow][_hintCol] = CellType.Hint;
            }
        }
        private void ExecuteGamePreferencesViewCommand(object obj)
        {
            _mainViewModel.GamePreferencesViewCommand.Execute(null);
        }
        private void ExecuteProfileViewCommand(object obj)
        {
            _mainViewModel.ProfileViewCommand.Execute(null);
        }
        private void ExecuteCellClickCommand(object obj)
        {
            var param = (string)obj;

            int row = int.Parse(param[0].ToString());
            int col = int.Parse(param[1].ToString());

            if (Board[row][col] == CellType.Empty || Board[row][col] == CellType.Hint)
            {
                if (_gamePreferences.Hint)
                {
                    Board[_hintRow][_hintCol] = CellType.Empty;
                }

                Board[row][col] = CellType.Cross;
                _game.MakeMove(row, col);

                if (_game.IsGameOver())
                {
                    GameResult = _game.GameResult;
                    CheckGameStatus();
                    _isGameRunning = false;
                    return;
                }

                var move = _game.MakeBotMove(_gamePreferences.Difficulty);
                Board[move.row][move.col] = CellType.Circle;

                if (_game.IsGameOver())
                {
                    GameResult = _game.GameResult;
                    CheckGameStatus();
                    _isGameRunning = false;
                    return;
                }

                if (_gamePreferences.Hint)
                {
                    var hintMove = _game.HintMove();
                    _hintRow = hintMove.row;
                    _hintCol = hintMove.col;
                    Board[_hintRow][_hintCol] = CellType.Hint;
                }
            }
        }

        private void CheckGameStatus()
        {
            if (GameResult == "Cross")
            {
                CurrentPlayer.Wins += 1;
                GameModel game = new GameModel()
                {
                    PlayerID = CurrentPlayer.PlayerID,
                    Score = 2,
                    Date = System.DateTime.Now
                };
                _gameRepository.AddGame(game);
            }
            else if (GameResult == "Circle")
            {
                CurrentPlayer.Losses += 1;
                GameModel game = new GameModel()
                {
                    PlayerID = CurrentPlayer.PlayerID,
                    Score = 0,
                    Date = System.DateTime.Now
                };
                _gameRepository.AddGame(game);
            }
            else if (GameResult == "Draw")
            {
                CurrentPlayer.Draws += 1;
                GameModel game = new GameModel()
                {
                    PlayerID = CurrentPlayer.PlayerID,
                    Score = 1,
                    Date = System.DateTime.Now
                };
                _gameRepository.AddGame(game);
            }
        }
    }
}
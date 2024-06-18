using System.Collections.ObjectModel;
using System.Windows.Input;
using TicTacToe.Base;
using TicTacToe.Helpers;
using TicTacToe.Model;
using TicTacToe.Repository;

namespace TicTacToe.ViewModel
{
    public class GamePreferencesViewModel : BaseViewModel
    {
        //Fields
        private readonly MainViewModel _mainViewModel;
        private IPlayerRepository _playerRepository;
        private IGameRepository _gameRepository;

        private GamePreferences _gamePreferences;

        private ObservableCollection<string> _difficults;
        private bool _hint;

        private int _currentIndex;
        private string _currentDifficult;

        //Properties
        public ObservableCollection<string> Difficults
        {
            get => _difficults;
            set
            {
                _difficults = value;
                OnPropertyChanged(nameof(Difficults));
            }
        }
        public bool Hint
        {
            get => _hint;
            set
            {
                _hint = value;
                OnPropertyChanged(nameof(Hint));
            }
        }
        public string CurrentDifficult
        {
            get => _currentDifficult;
            set
            {
                _currentDifficult = value;
                OnPropertyChanged(nameof(CurrentDifficult));
            }
        }

        //Commands
        public ICommand PlayerTopViewCommand { get; }
        public ICommand ProfileViewCommand { get; }
        public ICommand GameViewCommand { get; }
        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }

        public GamePreferencesViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            _gamePreferences = new GamePreferences();

            _playerRepository = new PlayerRepository();
            _gameRepository = new GameRepository();

            Difficults = new ObservableCollection<string> { "Noob", "Defence", "Attack", "Pro", "AI" };
            _currentIndex = 0;
            CurrentDifficult = Difficults[_currentIndex];

            PlayerTopViewCommand = new ViewModelCommand(ExecutePlayerTopViewCommand);
            ProfileViewCommand = new ViewModelCommand(ExecuteProfileViewCommand);
            GameViewCommand = new ViewModelCommand(ExecuteGameViewCommand);

            NextCommand = new ViewModelCommand(ExecuteNextCommand, CanExecuteNextCommand);
            PreviousCommand = new ViewModelCommand(ExecutePreviousCommand, CanExecutePreviousCommand);
        }

        //Checks
        private bool CanExecuteNextCommand(object obj)
        {
            return _currentIndex < Difficults.Count - 1;
        }

        private bool CanExecutePreviousCommand(object obj)
        {
            return _currentIndex > 0;
        }

        //Executes
        private void ExecutePlayerTopViewCommand(object obj)
        {
            _mainViewModel.PlayerTopViewCommand.Execute(null);
        }
        private void ExecuteProfileViewCommand(object obj)
        {
            _mainViewModel.ProfileViewCommand.Execute(null);
        }
        private void ExecuteGameViewCommand(object obj)
        {
            _gamePreferences.Difficulty = CurrentDifficult;
            _gamePreferences.Hint = Hint;

            GameSettings.Instance.SetCurrentGame(_gamePreferences);
            _mainViewModel.GameViewCommand.Execute(null);
        }

        private void ExecuteNextCommand(object obj)
        {
            _currentIndex++;
            CurrentDifficult = Difficults[_currentIndex];
        }

        private void ExecutePreviousCommand(object obj)
        {
            _currentIndex--;
            CurrentDifficult = Difficults[_currentIndex];
        }
    }
}

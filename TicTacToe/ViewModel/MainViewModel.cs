using System.Windows.Input;
using TicTacToe.Base;
using TicTacToe.Model;

namespace TicTacToe.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        //Fields
        private BaseViewModel _currentViewModel;
        private string _title;

        //Properties
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        //Commands
        public ICommand LoginViewCommand { get; }
        public ICommand RegisterViewCommand { get; }
        public ICommand ProfileViewCommand { get; }

        public PlayerModel CurrentUser { get; set; }

        public MainViewModel()
        {
            LoginViewCommand = new ViewModelCommand(ExecuteLoginViewCommand);
            RegisterViewCommand = new ViewModelCommand(ExecuteRegisterViewCommand);
            ProfileViewCommand = new ViewModelCommand(ExecuteProfileViewCommand);

            ExecuteLoginViewCommand(null);
        }

        public void ExecuteLoginViewCommand(object obj)
        {
            CurrentViewModel = new LoginViewModel(this);
            Title = "LOGIN";
        }
        public void ExecuteRegisterViewCommand(object obj)
        {
            CurrentViewModel = new RegisterViewModel(this);
            Title = "REGISTER";
        }
        public void ExecuteProfileViewCommand(object obj)
        {
            CurrentViewModel = new ProfileViewModel(this);
            Title = "PROFILE";
        }
        public void ExecuteProfilePreferencesViewCommand(object obj)
        {
            CurrentViewModel = new ProfilePreferencesViewModel(this);
            Title = "PROFILE PREFERENCES";
        }
        public void ExecuteGameViewCommand(object obj)
        {
            CurrentViewModel = new GameViewModel(this);
            Title = "GAME";
        }
        public void ExecuteGamePreferencesViewCommand(object obj)
        {
            CurrentViewModel = new GamePreferencesViewModel(this);
            Title = "GAME PREFERENCES";
        }
        public void ExecutePlayerTopViewCommand(object obj)
        {
            CurrentViewModel = new PlayerTopViewModel(this);
            Title = "TOP PLAYERS";
        }

    }
}

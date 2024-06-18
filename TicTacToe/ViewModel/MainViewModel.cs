using System;
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
        public ICommand InfoViewCommand { get; }
        public ICommand ProfileViewCommand { get; }
        public ICommand ProfilePreferencesViewCommand { get; }
        public ICommand PlayerTopViewCommand { get; }
        public ICommand GameViewCommand { get; }
        public ICommand GamePreferencesViewCommand { get; }

        public PlayerModel CurrentUser { get; set; }

        public MainViewModel()
        {
            LoginViewCommand = new ViewModelCommand(ExecuteLoginViewCommand);
            RegisterViewCommand = new ViewModelCommand(ExecuteRegisterViewCommand);
            InfoViewCommand = new ViewModelCommand(ExecuteInfoViewCommand, CanExecuteInfoViewCommand);
            ProfileViewCommand = new ViewModelCommand(ExecuteProfileViewCommand);
            ProfilePreferencesViewCommand = new ViewModelCommand(ExecuteProfilePreferencesViewCommand);
            PlayerTopViewCommand = new ViewModelCommand(ExecutePlayerTopViewCommand);
            GameViewCommand = new ViewModelCommand(ExecuteGameViewCommand);
            GamePreferencesViewCommand = new ViewModelCommand(ExecuteGamePreferencesViewCommand);

            ExecuteLoginViewCommand(null);
        }

        private bool CanExecuteInfoViewCommand(object obj)
        {
            return !(CurrentViewModel is LoginViewModel) && !(CurrentViewModel is RegisterViewModel);
        }

        private void ExecuteLoginViewCommand(object obj)
        {
            CurrentViewModel = new LoginViewModel(this);
            Title = "LOGIN";
        }
        private void ExecuteRegisterViewCommand(object obj)
        {
            CurrentViewModel = new RegisterViewModel(this);
            Title = "REGISTER";
        }
        private void ExecuteProfileViewCommand(object obj)
        {
            CurrentViewModel = new ProfileViewModel(this);
            Title = "PROFILE";
        }
        private void ExecuteProfilePreferencesViewCommand(object obj)
        {
            CurrentViewModel = new ProfilePreferencesViewModel(this);
            Title = "PROFILE PREFERENCES";
        }
        private void ExecuteGameViewCommand(object obj)
        {
            CurrentViewModel = new GameViewModel(this);
            Title = "GAME";
        }
        private void ExecuteGamePreferencesViewCommand(object obj)
        {
            CurrentViewModel = new GamePreferencesViewModel(this);
            Title = "GAME PREFERENCES";
        }
        private void ExecutePlayerTopViewCommand(object obj)
        {
            CurrentViewModel = new PlayerTopViewModel(this);
            Title = "TOP PLAYERS";
        }
        private void ExecuteInfoViewCommand(object obj)
        {
            CurrentViewModel = new InfoViewModel(this);
            Title = "INFO";
        }
    }
}

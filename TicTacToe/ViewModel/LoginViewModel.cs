using System;
using System.Security;
using System.Windows;
using System.Windows.Input;
using TicTacToe.Base;
using TicTacToe.Model;
using TicTacToe.Repository;

namespace TicTacToe.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        //Fields
        private readonly MainViewModel _mainViewModel;
        private IPlayerRepository _playerRepository;

        private string _username;
        private SecureString _password;
        private string _errorMessage;

        //Properties
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        //Commands
        public ICommand LoginCommand { get; }
        public ICommand RegisterViewCommand { get; }

        public LoginViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _playerRepository = new PlayerRepository();

            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RegisterViewCommand = new ViewModelCommand(ExecuteRegisterViewCommand);
        }

        //Checks
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;

            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;

            return validData;
        }

        //Executes
        private void ExecuteLoginCommand(object obj)
        {
            var isValidPlayer = _playerRepository.AuthenticateUser(new System.Net.NetworkCredential(Username, Password));
            if (isValidPlayer)
            {
                MessageBox.Show("Profile.xaml");
            }
            else
                ErrorMessage = "* Invalid Username or Password";
        }
        private void ExecuteRegisterViewCommand(object obj)
        {
            _mainViewModel.ExecuteRegisterViewCommand(null);
        }
    }
}

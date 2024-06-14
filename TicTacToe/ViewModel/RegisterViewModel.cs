using System.Windows.Input;
using TicTacToe.Base;
using TicTacToe.Model;
using TicTacToe.Repository;

namespace TicTacToe.ViewModel
{
    public class RegisterViewModel : BaseViewModel
    {
        //Fields
        private readonly MainViewModel _mainViewModel;
        private IPlayerRepository _playerRepository;

        private string _username;
        private string _password;
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
        public string Password
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
        public ICommand RegisterCommand { get; }
        public ICommand LoginViewCommand { get; }

        public RegisterViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _playerRepository = new PlayerRepository();

            RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
            LoginViewCommand = new ViewModelCommand(ExecuteLoginViewCommand);
        }

        //Checks
        private bool CanExecuteRegisterCommand(object obj)
        {
            bool validData;

            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
            {
                validData = false;
                ErrorMessage = "* Invalid Username or Password";
            }
            else
                validData = true;

            return validData;
        }

        //Executes
        private void ExecuteRegisterCommand(object obj)
        {
            var _player = new PlayerModel()
            {
                Username = Username,
                Password = Password
            };

            _playerRepository.AddPlayer(_player);
            _mainViewModel.ExecuteLoginViewCommand(null);
        }
        private void ExecuteLoginViewCommand(object obj)
        {
            _mainViewModel.ExecuteLoginViewCommand(null);
        }
    }
}

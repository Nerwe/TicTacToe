using System.Windows.Input;
using TicTacToe.Base;
using TicTacToe.Model;

namespace TicTacToe.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        //Fields
        private BaseViewModel _currentViewModel;

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

        //Commands
        public ICommand LoginViewCommand { get; }
        public ICommand RegisterViewCommand { get; }
        public ICommand ProfileViewCommand { get; }

        public PlayerModel CurrentUser { get; set; }

        public MainViewModel()
        {
            LoginViewCommand = new ViewModelCommand(ExecuteLoginViewCommand);
            RegisterViewCommand = new ViewModelCommand(ExecuteRegisterViewCommand);

            ExecuteLoginViewCommand(null);
        }

        public void ExecuteLoginViewCommand(object obj)
        {
            CurrentViewModel = new LoginViewModel(this);
        }

        public void ExecuteRegisterViewCommand(object obj)
        {
            CurrentViewModel = new RegisterViewModel(this);
        }
    }
}

using System.Windows.Input;
using TicTacToe.Base;

namespace TicTacToe.ViewModel
{
    public class InfoViewModel : BaseViewModel
    {
        //Fields
        private readonly MainViewModel _mainViewModel;


        //Commands
        public ICommand ProfileViewCommand { get; }
        public ICommand GamePreferencesViewCommand { get; }

        public InfoViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            GamePreferencesViewCommand = new ViewModelCommand(ExecuteGamePreferencesViewCommand);
            ProfileViewCommand = new ViewModelCommand(ExecuteProfileViewCommand);
        }

        private void ExecuteGamePreferencesViewCommand(object obj)
        {
            _mainViewModel.GamePreferencesViewCommand.Execute(null);
        }

        private void ExecuteProfileViewCommand(object obj)
        {
            _mainViewModel.GamePreferencesViewCommand.Execute(null);
        }
    }
}

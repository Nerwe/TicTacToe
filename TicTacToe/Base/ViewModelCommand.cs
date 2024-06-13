using System;
using System.Windows.Input;

namespace TicTacToe.Base
{
    internal class ViewModelCommand : ICommand
    {
        //Fields
        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExucuteAction;

        //Constructors
        public ViewModelCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canExucuteAction = null;
        }

        public ViewModelCommand(Action<object> executeAction, Predicate<object> canExucuteAction)
        {
            _executeAction = executeAction;
            _canExucuteAction = canExucuteAction;
        }

        //Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

        //Methods
        public bool CanExecute(object parameter) => _canExucuteAction == null ? true : _canExucuteAction(parameter);

        public void Execute(object parameter) => _executeAction(parameter);
    }
}
﻿using System;
using System.Windows.Input;
using TicTacToe.Base;
using TicTacToe.Helpers;
using TicTacToe.Model;
using TicTacToe.Repository;

namespace TicTacToe.ViewModel
{
    public class ProfilePreferencesViewModel : BaseViewModel
    {
        //Fields
        private readonly MainViewModel _mainViewModel;
        private IPlayerRepository _playerRepository;
        private PlayerModel _currentPlayer;

        private string _errorMessage;
        private string _doneMessage;

        private string _originalUsername;
        private string _originalPassword;


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
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public string DoneMessage
        {
            get => _doneMessage;
            set
            {
                _doneMessage = value;
                OnPropertyChanged(nameof(DoneMessage));
            }
        }
        public string OriginalUsername
        {
            get => _originalUsername;
            set
            {
                _originalUsername = value;
                OnPropertyChanged(nameof(OriginalUsername));
            }
        }
        public string OriginalPassword
        {
            get => _originalPassword;
            set
            {
                _originalPassword = value;
                OnPropertyChanged(nameof(OriginalPassword));
            }
        }

        //Commands
        public ICommand SaveChangesCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand DelAccountCommand { get; }
        public ICommand GamePreferencesViewCommand { get; }
        public ICommand ProfileViewCommand { get; }

        public ProfilePreferencesViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _playerRepository = new PlayerRepository();

            CurrentPlayer = PlayerSession.Instance.CurrentPlayer;
            OriginalUsername = CurrentPlayer.Username;
            OriginalPassword = CurrentPlayer.Password;

            SaveChangesCommand = new ViewModelCommand(ExecuteSaveChangesCommand, CanExecuteSaveChangesCommand);
            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);
            DelAccountCommand = new ViewModelCommand(ExecuteDelAccountCommand, CanExecuteDelAccountCommand);
            GamePreferencesViewCommand = new ViewModelCommand(ExecuteGamePreferencesViewCommand);
            ProfileViewCommand = new ViewModelCommand(ExecuteProfileViewCommand);
        }

        //Checks
        private bool CanExecuteSaveChangesCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(CurrentPlayer.Username) || CurrentPlayer.Username.Length < 3 ||
                CurrentPlayer.Password == null || CurrentPlayer.Password.Length < 3)
            {
                validData = false;
                DoneMessage = "";
                ErrorMessage = "* Invalid Username or Password";
            }
            else
                validData = true;

            return validData;
        }

        private bool CanExecuteDelAccountCommand(object obj)
        {
            return true;
        }

        //Executes
        private void ExecuteSaveChangesCommand(object obj)
        {
            ErrorMessage = "";
            CurrentPlayer.Username = OriginalUsername;
            CurrentPlayer.Password = OriginalPassword;
            _playerRepository.UpdatePlayer(CurrentPlayer);
            PlayerSession.Instance.SetCurrentPlayer(CurrentPlayer);
            DoneMessage = "* Changes saved";
        }

        private void ExecuteLogoutCommand(object obj)
        {
            PlayerSession.Instance.ClearCurrentPlayer();
            _mainViewModel.LoginViewCommand.Execute(null);
        }

        private void ExecuteDelAccountCommand(object obj)
        {
            if (DateTime.Now.Subtract(CurrentPlayer.RegisterDate).TotalDays > 60)
            {
                CurrentPlayer.IsActive = false;
                _playerRepository.UpdatePlayer(CurrentPlayer);
                PlayerSession.Instance.ClearCurrentPlayer();
                _mainViewModel.LoginViewCommand.Execute(null);
            }
            else
            {
                ErrorMessage = "Register date < 60 days";
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
    }
}

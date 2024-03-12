using Homework_7_2.Commands;
using Homework_7_2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Homework_7_2.ViewModels
{
    public class LogOnViewModel : ViewModelBase
    {
        private bool _canCloseWindow;
        private UserCredentials _userCredentials;
        public LogOnViewModel(bool canCloseWindow)
        {
            ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
            CloseCommand = new RelayCommand(Close);
            ClosedWindowEventCommand = new RelayCommand(ClosedWindowEvent);
            _userCredentials = new UserCredentials();
            _canCloseWindow = canCloseWindow;

            //do usuniecia
            UserCredentials.Login = ConfigurationManager.AppSettings["Login"];
            UserCredentials.Password = ConfigurationManager.AppSettings["Password"];
        }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand ClosedWindowEventCommand { get; set; }

        public UserCredentials UserCredentials
        {
            get { return _userCredentials; }
            set
            {
                _userCredentials = value;
                OnPropertyChanged();
            }
        }

        private void Confirm(object obj)
        {
            //walidacja
            if (!UserCredentials.IsValid)
                return;

            var logOnParams = obj as LogOnParams;
            UserCredentials.Password = logOnParams.PasswordBox.Password;

            if (IsLogOnSuccess())
            {
                _canCloseWindow = true;
                CloseWindow(logOnParams.Window);
            }
        }
        private bool CanConfirm(object obj)
        {
            return UserCredentials.IsValid;
        }
        private bool IsLogOnSuccess()
        {
            return UserCredentials.Login == ConfigurationManager.AppSettings["Login"] &&
                UserCredentials.Password == ConfigurationManager.AppSettings["Password"] ? true : false;
        }

        private void ClosedWindowEvent(object obj)
        {
            if (!_canCloseWindow)
                Application.Current.Shutdown();
        }
        private void Close(object obj)
        {
            if (_canCloseWindow)
                CloseWindow(obj as Window);
            else
                Application.Current.Shutdown();
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}

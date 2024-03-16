using Homework_7_2.Commands;
using Homework_7_2.Models;
using Homework_7_2.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Homework_7_2.ViewModels
{
    public class ConnectionSettingsViewModel : ViewModelBase
    {
        private UserSettings _userSettings;
        private readonly bool _canCloseWindow;
        public ConnectionSettingsViewModel(bool canCloseWindow)
        {
            ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
            CloseCommand = new RelayCommand(Close);
            ClosedWindowEventCommand = new RelayCommand(ClosedWindowEvent);
            _userSettings = new UserSettings();
            _canCloseWindow = canCloseWindow;
        }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand ClosedWindowEventCommand { get; set; }

        private void ClosedWindowEvent(object obj)
        {
            Settings.Default.Reload();
            if (!_canCloseWindow)
               Application.Current.Shutdown();
        }

        private void Close(object obj)
        {
            Settings.Default.Reload();
            if (_canCloseWindow)
                CloseWindow(obj as Window);
            else
                Application.Current.Shutdown();
        }

        private bool CanConfirm(object obj)
        {
            return UserSettings.IsValid;
        }

        private void Confirm(object obj)
        {
            if (!UserSettings.IsValid)
                return;

            UserSettings.Save();
            RestartApplication();
        }

        private void RestartApplication()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        public UserSettings UserSettings
        {
            get { return _userSettings; }
            set
            {
                _userSettings = value;
                OnPropertyChanged();
            }
        }
    }
}

using Homework_7_2.Properties;
using System.ComponentModel;

namespace Homework_7_2.Models
{
    public class UserSettings : IDataErrorInfo
    {
        public string ServerAddress
        {
            get
            {
                return Settings.Default.ServerAddress;
            }
            set
            {
                Settings.Default.ServerAddress = value;
            }
        }
        public string ServerName
        {
            get
            {
                return Settings.Default.ServerName;
            }
            set
            {
                Settings.Default.ServerName = value;
            }
        }
        public string DatabaseName
        {
            get
            {
                return Settings.Default.DatabaseName;
            }
            set
            {
                Settings.Default.DatabaseName = value;
            }
        }
        public string DatabaseLogin
        {
            get
            {
                return Settings.Default.DatabaseLogin;
            }
            set
            {
                Settings.Default.DatabaseLogin = value;
            }
        }
        public string DatabasePassword
        {
            get
            {
                return Settings.Default.DatabasePassword;
            }
            set
            {
                Settings.Default.DatabasePassword = value;
            }
        }

        public void Save()
        {
            Settings.Default.Save();
        }

        private bool _isServerAddressValid, _isServerNameValid, _isDatabaseNameValid, _isDatabaseLoginValid, _isDatabasePasswordValid;
        public string Error { get; set; }

        public string this[string columnName]
        {
            get 
            {
                switch (columnName)
                {
                    case nameof(ServerAddress):
                        if (string.IsNullOrWhiteSpace(ServerAddress))
                        {
                            Error = "Pole \"Adres Serwera\" jest obowiązkowe!";
                            _isServerAddressValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerAddressValid = true;
                        }
                        break;
                    case nameof(ServerName):
                        if (string.IsNullOrWhiteSpace(ServerName))
                        {
                            Error = "Pole \"Nazwa Serwera\" jest obowiązkowe!";
                            _isServerNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerNameValid = true;
                        }
                        break;
                    case nameof(DatabaseName):
                        if (string.IsNullOrWhiteSpace(DatabaseName))
                        {
                            Error = "Pole \"Nazwa Bazy Danych\" jest obowiązkowe!";
                            _isDatabaseNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isDatabaseNameValid = true;
                        }
                        break;
                    case nameof(DatabaseLogin):
                        if (string.IsNullOrWhiteSpace(DatabaseLogin))
                        {
                            Error = "Pole \"Login\" jest obowiązkowe!";
                            _isDatabaseLoginValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isDatabaseLoginValid = true;
                        }
                        break;
                    case nameof(DatabasePassword):
                        if (string.IsNullOrWhiteSpace(DatabasePassword))
                        {
                            Error = "Pole \"Hasło\" jest obowiązkowe!";
                            _isDatabasePasswordValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isDatabasePasswordValid = true;
                        }
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }

        public bool IsValid
        {
            get
            {
                return _isServerAddressValid && _isServerNameValid && _isDatabaseNameValid && _isDatabaseLoginValid && _isDatabasePasswordValid;
            }
        }
    }
}

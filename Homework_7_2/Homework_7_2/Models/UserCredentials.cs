using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_7_2.Models
{
    public class UserCredentials : IDataErrorInfo
    {
        public string Login { get; set; }
        public string Password { get; set; }


        //walidacja

        private bool _isLoginValid;
        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Login):
                        if (string.IsNullOrWhiteSpace(Login))
                        {
                            Error = "Pole \"Login\" jest wymagane!";
                            _isLoginValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isLoginValid = true;
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
                return _isLoginValid;
            }
        }
    }
}

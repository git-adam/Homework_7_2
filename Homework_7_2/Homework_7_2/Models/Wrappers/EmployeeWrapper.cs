using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Homework_7_2.Models.Wrappers
{
    public class EmployeeWrapper : IDataErrorInfo
    {
        public EmployeeWrapper()
        {
            Status = new StatusWrapper();
            HireDate = new DateTime(DateTime.Now.Year, 1, 1);
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? DismissDate { get; set; }
        public decimal Salary { get; set; }
        public bool Bonus { get; set; }
        public string Comments { get; set; }
        public StatusWrapper Status { get; set; }

        private bool _isFirstNameValid, _isLastNameValid, _isNumberValid, _isHireDateValid, _isSalaryValid;

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            Error = "Pole \"Imię\" jest wymagane!";
                            _isFirstNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isFirstNameValid = true;
                        }
                        break;
                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            Error = "Pole \"Nazwisko\" jest wymagane!";
                            _isLastNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isLastNameValid = true;
                        }
                        break;
                    case nameof(Number):
                        if (string.IsNullOrWhiteSpace(Number.ToString()))
                        {
                            Error = "Pole \"Numer\" jest wymagane!";
                            _isNumberValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isNumberValid = true;
                        }
                        break;
                    case nameof(HireDate):
                        if (string.IsNullOrWhiteSpace(HireDate.ToString()))
                        {
                            Error = "Pole \"Data zatrudnienia\" jest wymagane!";
                            _isHireDateValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isHireDateValid = true;
                        }
                        break;
                    case nameof(Salary):
                        if (string.IsNullOrWhiteSpace(Salary.ToString()))
                        {
                            Error = "Pole \"Wypłata\" jest wymagane!";
                            _isSalaryValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isSalaryValid = true;
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
                return _isFirstNameValid && _isLastNameValid && _isNumberValid && _isHireDateValid && _isSalaryValid && Status.IsValid;
            }
        }

    }
}

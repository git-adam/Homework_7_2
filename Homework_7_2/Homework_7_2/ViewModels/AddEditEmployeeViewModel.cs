using Homework_7_2.Commands;
using Homework_7_2.Models.Converters;
using Homework_7_2.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Homework_7_2.ViewModels
{
    public class AddEditEmployeeViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();
        public AddEditEmployeeViewModel(EmployeeWrapper employee = null)
        {
            ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
            CloseCommand = new RelayCommand(Close);

            if (employee == null)
            {
                Employee = new EmployeeWrapper();
            }
            else
            {
                Employee = employee;
                IsUpdate = true;

                if (!string.IsNullOrWhiteSpace(Employee.DismissDate.ToString()))
                    IsDismiss = true;
            }

            InitStatuses();
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private EmployeeWrapper _employee;

        public EmployeeWrapper Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }
        }

        private bool _isDismiss;

        public bool IsDismiss
        {
            get { return _isDismiss; }
            set
            {
                _isDismiss = value;
                OnPropertyChanged();
            }
        }

        private int _selectedStatusId;

        public int SelectedStatusId
        {
            get { return _selectedStatusId; }
            set
            {
                _selectedStatusId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StatusWrapper> _statuses;

        public ObservableCollection<StatusWrapper> Statuses
        {
            get { return _statuses; }
            set
            {
                _statuses = value;
                OnPropertyChanged();
            }
        }


        private void Confirm(object obj)
        {
            if (!Employee.IsValid)
             return;

            if (!IsUpdate)
                AddEmployee();
            else
                UpdateEmployee();

            CloseWindow(obj as Window);
        }

        private void UpdateEmployee()
        {
            //baza danych
            _repository.UpdateEmployee(Employee);
        }

        private void AddEmployee()
        {
            //baza danych
            _repository.AddEmployee(Employee);
        }

        private bool CanConfirm(object obj)
        {
            return Employee.IsValid;
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
        private void InitStatuses()
        {

            var statuses = _repository
                .GetStatuses()
                .Select(x => x.ToWrapper())
                .ToList();

            statuses.Insert(0, new StatusWrapper() { Id = 0, Name = "-- BRAK --" });

            Statuses = new ObservableCollection<StatusWrapper>(statuses);

            //SelectedStatusId = Employee.Status.Id;
            //Employee.Status.Id = 0;
            //Statuses = new ObservableCollection<StatusWrapper>()
            //{
            //    new StatusWrapper() { Id = 0, Name = "--- Brak ---" },
            //    new StatusWrapper() { Id = 1, Name = "Urlop wypoczynkowy" },
            //    new StatusWrapper() { Id = 2, Name = "Urlop zdrowotny" },
            //    new StatusWrapper() { Id = 3, Name = "Delegacja" },
            //    new StatusWrapper() { Id = 4, Name = "Zwolniony" },
            //    new StatusWrapper() { Id = 5, Name = "Dostępny" },
            //};
            //Employee.Status.Id = 0;
        }

    }
}

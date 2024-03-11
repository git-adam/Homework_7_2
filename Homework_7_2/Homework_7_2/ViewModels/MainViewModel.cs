using Homework_7_2.Commands;
using Homework_7_2.Models.Wrappers;
using Homework_7_2.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {

            AddEmployeeCommand = new RelayCommand(AddEditEmployee);
            EditEmployeeCommand = new RelayCommand(AddEditEmployee, CanEditDismissEmployee);
            DismissEmployeeCommand = new RelayCommand(DismissEmployee, CanEditDismissEmployee);
            RefreshEmployeesCommand = new RelayCommand(RefreshEmployees);
            ConnectionSettingsCommand = new RelayCommand(SetConnectionSettings);
            LogOnCommand = new RelayCommand(LogOn);
            LoadedWindowCommand = new RelayCommand(LoadedWindow);

            //LoadedWindow(null);
            var logOnWindow = new LogOnView(false);
            logOnWindow.ShowDialog();

            InitEmployees();
            InitStatuses();
        }

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand DismissEmployeeCommand { get; set; }
        public ICommand RefreshEmployeesCommand { get; set; }
        public ICommand ConnectionSettingsCommand { get; set; }
        public ICommand LogOnCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
       

        private EmployeeWrapper _selectedEmployee;

        public EmployeeWrapper SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<EmployeeWrapper> _employees;

        public ObservableCollection<EmployeeWrapper> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
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

        private ObservableCollection<StatusWrapper> _status;

        public ObservableCollection<StatusWrapper> Status
        {
            get { return _status; }
            set 
            { 
                _status = value;
                OnPropertyChanged();
            }
        }

        private void AddEditEmployee(object obj)
        {
            MessageBox.Show("Dodaj/Edytuj");
        }
        private void DismissEmployee(object obj)
        {
            MessageBox.Show("Zwolnij");
        }
        private bool CanEditDismissEmployee(object obj)
        {
            return SelectedEmployee != null;
        }
        private void RefreshEmployees(object obj)
        {
            MessageBox.Show("Odśwież");
        }
        private void SetConnectionSettings(object obj)
        {
            MessageBox.Show("Ustawienia");
        }
        private void LogOn(object obj)
        {
            var logOnWindow = new LogOnView(true); //Nie jest to dobra praktyka, stosuje się mechanizm DEPENDENCY INJECTION
            logOnWindow.Closed += LogOnWindow_Closed;
            logOnWindow.ShowDialog();
        }

        private void LogOnWindow_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("Zaloguj");
        }

        private void LoadedWindow(object obj)
        {
        }

        private void InitEmployees()
        {
            Employees = new ObservableCollection<EmployeeWrapper>()
            {
                new EmployeeWrapper() {FirstName = "A", Bonus = true},
                new EmployeeWrapper() {FirstName = "B", Status = new StatusWrapper(){ Id = 1, Name = "Na urlopie"} },
                new EmployeeWrapper() {FirstName = "C"},
            };
        }

        private void InitStatuses()
        {
            Status = new ObservableCollection<StatusWrapper>()
            {
                new StatusWrapper() { Id = 0, Name = "Wszyscy" },
                new StatusWrapper() { Id = 1, Name = "Na urlopie wyp." },
                new StatusWrapper() { Id = 2, Name = "Na urlopie zdr." },
                new StatusWrapper() { Id = 3, Name = "W delegacji" },
                new StatusWrapper() { Id = 4, Name = "Zwolniony" },
                new StatusWrapper() { Id = 5, Name = "Dostępny" },
            };

            SelectedStatusId = 0;
        }



    }
}

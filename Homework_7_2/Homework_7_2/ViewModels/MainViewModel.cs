using Homework_7_2.Commands;
using Homework_7_2.Models.Converters;
using Homework_7_2.Models.Domains;
using Homework_7_2.Models.Wrappers;
using Homework_7_2.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Homework_7_2.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();
        public MainViewModel()
        {
            AddEmployeeCommand = new RelayCommand(AddEditEmployee);
            EditEmployeeCommand = new RelayCommand(AddEditEmployee, CanEditEmployee);
            DismissEmployeeCommand = new AsyncRelayCommand(DismissEmployee, CanDismissEmployee);
            RefreshEmployeesCommand = new RelayCommand(RefreshEmployees);
            ConnectionSettingsCommand = new RelayCommand(SetConnectionSettings);
            LoadedWindowCommand = new RelayCommand(LoadedWindow);
            LogOnCommand = new RelayCommand(LogOn);
            //SelectionChangedCommand = new RelayCommand(SelectionChanged);

            //LoadedWindow(null);
        }

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand DismissEmployeeCommand { get; set; }
        public ICommand RefreshEmployeesCommand { get; set; }
        public ICommand ConnectionSettingsCommand { get; set; }
        public ICommand LogOnCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }

        public ICommand SelectionChangedCommand { get; set; }


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
                var old = _selectedStatusId;

                _selectedStatusId = value;

                OnPropertyChanged();

                if (_selectedStatusId != old)
                    Refresh();
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

        private void AddEditEmployee(object obj)
        {
            var addEditEmployeeWindow = new AddEditEmployeeView(obj as EmployeeWrapper);
            addEditEmployeeWindow.Closed += AddEditEmployeeWindow_Closed;
            addEditEmployeeWindow.ShowDialog();

        }

        private void AddEditEmployeeWindow_Closed(object sender, EventArgs e)
        {
            Refresh();
        }

        private async Task DismissEmployee(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Zwalnianie pracownika",
                $"Czy na pewno chcesz zwolnić pracownika {SelectedEmployee.FirstName} {SelectedEmployee.LastName}?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DismissEmployee(SelectedEmployee.Id);

            Refresh();
        }
        private bool CanEditEmployee(object obj)
        {
            return SelectedEmployee != null;
        }

        private bool CanDismissEmployee(object obj)
        {
            return SelectedEmployee != null && SelectedEmployee.DismissDate == null;
        }

        private void RefreshEmployees(object obj)
        {
            Refresh();
        }
        private void SetConnectionSettings(object obj)
        {
            var setConnectionSettingsWindow = new ConnectionSettingsView(true);
            setConnectionSettingsWindow.Closed += SetConnectionSettingsWindow_Closed;
            setConnectionSettingsWindow.ShowDialog();
        }

        private void SetConnectionSettingsWindow_Closed(object sender, EventArgs e)
        {
            Refresh();
        }

        private async void LogOn(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Wylogowanie z konta",
                $"Czy aby na pewno chcesz zostać wylogowany z obecnego konta?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog == MessageDialogResult.Affirmative)
                RestartApplication();
        }

        private void RestartApplication()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private async void LoadedWindow(object obj)
        {
            var logOnWindow = new LogOnView(false);
            logOnWindow.ShowDialog();

            if (!IsDatabaseConnectionPossible())
            {
                var metroWindow = Application.Current.MainWindow as MetroWindow;
                var dialog = await metroWindow.ShowMessageAsync(
                    "Błąd połączenia",
                    $"Nie można połączyć się z bazą danych. Czy chcesz zmienić ustawienia?",
                    MessageDialogStyle.AffirmativeAndNegative);

                if (dialog != MessageDialogResult.Affirmative)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    var setConnectionStringWindow = new ConnectionSettingsView(false);
                    setConnectionStringWindow.ShowDialog();
                }
            }
            else
            {
                Refresh();
                //InitEmployees();
                InitStatuses();
            }
        }

        private void Refresh()
        {
            Employees = new ObservableCollection<EmployeeWrapper>(_repository.GetEmployees(SelectedStatusId));
        }

        public bool IsDatabaseConnectionPossible()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                }
                return true;
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return false;
            }
        }
        private void InitStatuses()
        {
            var statuses = _repository
                .GetStatuses()
                .Select(x => x.ToWrapper())
                .ToList();

            statuses.Insert(0, new StatusWrapper() { Id = 0, Name = "Wszystkie statusy" });

            Statuses = new ObservableCollection<StatusWrapper>(statuses);
            SelectedStatusId = 0;
        }

        private void InitEmployees()
        {
            Employees = new ObservableCollection<EmployeeWrapper>()
            {
                new EmployeeWrapper() {FirstName = "A", Bonus = true, Salary = 2},
                new EmployeeWrapper() {FirstName = "B", Status = new StatusWrapper(){ Id = 1 } },
                new EmployeeWrapper() {FirstName = "C"},
            };
        }

        private void SelectionChanged(object obj)
        {
            Refresh();
        }

        private void InsertRecordToStatusesTable(int id, string name)
        {
            var newStatus = new StatusWrapper();
            newStatus.Id = id;
            newStatus.Name = name;
            _repository.AddRecordToStatusesTable(newStatus);
        }

        private void InitDefaultStatusesInDatabase()
        {
            var names = new string[] {"Urlop wypoczynkowy", "Urlop zdrowotny", "Delegacja", "Zwolniony", "Dostępny"};
            for (int i = 1; i <= names.Length; i++)
                InsertRecordToStatusesTable(i, names[i - 1]);
        }
    }
}

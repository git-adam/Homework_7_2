using Homework_7_2.Commands;
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
        public MainViewModel()
        {
            AddEmployeeCommand = new RelayCommand(AddEditEmployee);
            EditEmployeeCommand = new RelayCommand(AddEditEmployee, CanEditDismissEmployee);
            DismissEmployeeCommand = new AsyncRelayCommand(DismissEmployee, CanEditDismissEmployee);
            RefreshEmployeesCommand = new RelayCommand(RefreshEmployees);
            ConnectionSettingsCommand = new RelayCommand(SetConnectionSettings);
            LoadedWindowCommand = new RelayCommand(LoadedWindow);
            LogOnCommand = new RelayCommand(LogOn);

            //var logOnWindow = new LogOnView(false);
            //logOnWindow.ShowDialog();
            //LoadedWindow(null);
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
            var addEditEmployeeWindow = new AddEditEmployeeView();
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

            //odpowiendnia modyfikacja w bazie danych
        }
        private bool CanEditDismissEmployee(object obj)
        {
            return SelectedEmployee != null;
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
                $"Czy aby na pewno chcesz zotać wylogowany z obecnego konta?",
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
            //OK
            //var logOnWindow = new LogOnView(false);
            //logOnWindow.ShowDialog();

            if (false)
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
                InitEmployees();
                InitStatuses();
            }
        }

        private void Refresh()
        {
            MessageBox.Show("Odśwież - do zaimplementowania");
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

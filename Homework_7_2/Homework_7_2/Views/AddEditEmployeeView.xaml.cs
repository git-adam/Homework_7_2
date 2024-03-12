using Homework_7_2.ViewModels;
using MahApps.Metro.Controls;

namespace Homework_7_2.Views
{
    /// <summary>
    /// Interaction logic for AddEditEmployeeView.xaml
    /// </summary>
    public partial class AddEditEmployeeView : MetroWindow
    {
        public AddEditEmployeeView()
        {
            InitializeComponent();
            DataContext = new AddEditEmployeeViewModel();
        }
    }
}

using System.Windows;
using Planner.Utilty;

namespace Planner
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainViewModel viewModel = new MainViewModel(new DataService());
            MainWindow windowView = new MainWindow(viewModel);
            windowView.Show();
        }
    }
}

using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            CultureInfo cultureInfo = new CultureInfo("en-EN");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            base.OnStartup(e);
        }
    }
}

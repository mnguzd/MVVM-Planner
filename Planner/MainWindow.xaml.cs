using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Planner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)  //Moving the window, this is UI logic so it is here.
        {
            DragMove();
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void CloseMenuButton_Click(object sender, RoutedEventArgs e)
        {
            CloseMenuButton.Visibility = Visibility.Hidden;
            OpenMenuButton.Visibility = Visibility.Visible;
        }

        private void OpenMenuButton_Click(object sender, RoutedEventArgs e)
        {
            CloseMenuButton.Visibility = Visibility.Visible;
            OpenMenuButton.Visibility = Visibility.Hidden;
        }
    }
}

using System.Windows;
using System.Windows.Input;

namespace Planner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) //Moving the window
        {
            DragMove();
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e) //Removes selection right-clicking
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

        private void NewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            NewTaskButton.Visibility = Visibility.Hidden;
            CloseInputButton.Visibility = Visibility.Visible;
        }

        private void CloseInputButton_Click(object sender, RoutedEventArgs e)
        {
            NewTaskButton.Visibility = Visibility.Visible;
            CloseInputButton.Visibility = Visibility.Hidden;
        }

        private void NewFolderButton_Click(object sender, RoutedEventArgs e)
        {
            NewFolderButton.Visibility = Visibility.Hidden;
            CloseFolderInputButton.Visibility = Visibility.Visible;
        }

        private void CloseFolderInputButton_Click(object sender, RoutedEventArgs e)
        {
            CloseFolderInputButton.Visibility = Visibility.Hidden;
            NewFolderButton.Visibility = Visibility.Visible;
        }
    }
}

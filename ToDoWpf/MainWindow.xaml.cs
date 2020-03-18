using System.Windows;
using System.Windows.Input;

namespace ToDoWpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TopBar_LeftButton_Down(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ExitBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lBox.ItemsSource = Class1.ToDo;

        }
        private void MinimizeBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void Add_Task_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Add_Task add_task = new Add_Task();
            add_task.ShowDialog();
        }
        private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int index = lBox.SelectedIndex;
            editing NewEditing = new editing(index);
            NewEditing.ShowDialog();
            lBox.Items.Refresh();
        }

        private void DeleteBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Class1.ToDo.RemoveAt(lBox.SelectedIndex);
        }
    }
}

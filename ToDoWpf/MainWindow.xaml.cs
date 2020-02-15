using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoWpf;

namespace ToDoWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ToDoInfo> ToDoList = new List<ToDoInfo>();
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void TopBar_LeftButton_Down(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ExitBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ExitBar_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void ExitBar_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ToDoList.Add(new ToDoInfo { ToDo = "TaskNumber1", Done = true });
            ToDoList.Add(new ToDoInfo { ToDo = "TaskNumber2", Done = false });
            ToDoList.Add(new ToDoInfo { ToDo = "TaskNumber3", Done = true });
            listBox.ItemsSource = ToDoList;
        }
    }
}

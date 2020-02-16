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

namespace ToDoWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Doings> ToDoList = new List<Doings>();
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
            ToDoList.Add(new Doings { ToDo = "TaskNumber1", Done = true});
            ToDoList.Add(new Doings { ToDo = "TaskNumber2", Done = false });
            ToDoList.Add(new Doings { ToDo = "TaskNumber3", Done = true });
            lBox.ItemsSource = ToDoList;
        }
        private void MinimizeBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }
    }
}

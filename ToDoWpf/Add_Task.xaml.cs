using System.Windows;
using System.Windows.Input;

namespace ToDoWpf
{
    public partial class Add_Task : Window
    {
        public Add_Task()
        {
            InitializeComponent();
        }

        private void ExitBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void TopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Add_Task_Grid_Dop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TextInput.Text.Length > 0)
            {
                Class1.ToDo.Add(new Doings { ToDo = TextInput.Text, Done = false });
                TextInput.Text = "";
            }
            else
            {
                MessageBox.Show("Enter your task or don`t touch this button, ok?","Message",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (TextInput.Text.Length > 0)
                {
                    Class1.ToDo.Add(new Doings { ToDo = TextInput.Text, Done = false });
                    TextInput.Text = "";
                }
                else
                {
                    MessageBox.Show("Enter your task or don`t touch this button, ok?", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}

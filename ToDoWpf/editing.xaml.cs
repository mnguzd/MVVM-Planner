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
using System.Windows.Shapes;

namespace ToDoWpf
{
    /// <summary>
    /// Логика взаимодействия для editing.xaml
    /// </summary>
    public partial class editing : Window
    {
        private readonly int index = new int();
        private readonly string StartText;
        public editing(int SelectedItemIndex)
        {
            InitializeComponent();
            DateLabel.Content = Class1.ToDo[SelectedItemIndex].Date;
            TextInput.Text = Class1.ToDo[SelectedItemIndex].ToDo;
            TextInput.SelectionStart = TextInput.Text.Length;
            TextInput.Focus();
            index = SelectedItemIndex;
            StartText = TextInput.Text;
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

        private void EditButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TextInput.Text.Length == 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to delete this task?", "Empty task", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes: Class1.ToDo.RemoveAt(index); DialogResult = true; break;
                    case MessageBoxResult.No: TextInput.Text=StartText; TextInput.SelectionStart = TextInput.Text.Length;
                        TextInput.Focus(); break;
                }
            }
            else
            {
                Class1.ToDo[index].ToDo = TextInput.Text;
                DialogResult = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (TextInput.Text.Length == 0)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to delete this task?", "Empty task", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (result)
                    {
                        case MessageBoxResult.Yes: Class1.ToDo.RemoveAt(index); DialogResult = true; break;
                        case MessageBoxResult.No:
                            TextInput.Text = StartText; TextInput.SelectionStart = TextInput.Text.Length;
                            TextInput.Focus(); break;
                    }
                }
                else
                {
                    Class1.ToDo[index].ToDo = TextInput.Text;
                    DialogResult = true;
                }
            }
        }
    }
}

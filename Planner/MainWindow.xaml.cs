using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace Planner
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel main)
        {
            InitializeComponent();
            DataContext = main;
            ((INotifyCollectionChanged)ListOfTasks.Items).CollectionChanged += ListView_CollectionChanged;
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
        private void NewFolderButton_Click(object sender, RoutedEventArgs e)
        {
            NewFolderButton.Visibility = Visibility.Hidden;
            CloseFolderInputButton.Visibility = Visibility.Visible;
            BlurEffect effect = new BlurEffect
            {
                Radius = 2.7,
                KernelType = KernelType.Gaussian
            };
            FoldersGrid.Effect = effect;
        }

        private void CloseFolderInputButton_Click(object sender, RoutedEventArgs e)
        {
            CloseFolderInputButton.Visibility = Visibility.Hidden;
            NewFolderButton.Visibility = Visibility.Visible;
            FoldersGrid.Effect = null;
        }

        private void ListView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
              ListOfTasks.ScrollIntoView(e.NewItems[0]); // scroll the new item into view   
              ListViewItem item = ListOfTasks.ItemContainerGenerator.ContainerFromIndex(ListOfTasks.Items.Count - 1) as ListViewItem;
              if(item!=null)
                 item.Focusable=false;
            }
        }
    }
}

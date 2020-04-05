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
    }
}

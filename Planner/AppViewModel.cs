using Planner.Utilty;
using Planner.ViewModels;
using System.Windows;

namespace Planner
{
    public class AppViewModel : ObservableObject
    {
        public RelayCommand ClosingWindowCommand { get; private set;}
        public RelayCommand MinimizeWindowCommand { get; private set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { if (_currentView == value) return; 
                OnPropertyChanged(ref _currentView, value); }
        }
        private TasksViewModel _tasks;
        public TasksViewModel Tasks
        {
            get { return _tasks; }
            set{ if (_tasks == value) return;
                OnPropertyChanged(ref _tasks, value);}
        }
        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        public AppViewModel()
        {
            ClosingWindowCommand = new RelayCommand(p=> Application.Current.Shutdown(), p=>true);
            MinimizeWindowCommand = new RelayCommand(p => MinimizeWindow(), p => true);
            Tasks = new TasksViewModel();
            CurrentView = Tasks;
        }
    }
}

using Planner.Utilty;
using Planner.ViewModels;

namespace Planner
{
    public class AppViewModel : ObservableObject
    {
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
        public AppViewModel()
        {
            Tasks = new TasksViewModel();
            CurrentView = Tasks;
        }
    }
}

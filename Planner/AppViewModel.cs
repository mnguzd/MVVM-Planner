using Planner.Models;
using Planner.Utilty;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Planner
{
    public class AppViewModel : ObservableObject
    {
        public int i { get; set; } = 0;
        public RelayCommand ClosingWindowCommand { get; private set;}
        public RelayCommand MinimizeWindowCommand { get; private set; }
        public RelayCommand AddFolderCommand { get; private set; }
        public ObservableCollection<Folder> Folders { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { if (_currentView == value) return; 
                OnPropertyChanged(ref _currentView, value); }
        }
        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void AddFolder()
        {
            i++;
            Folders.Add(new Folder(DateTime.Now.ToShortTimeString(),i.ToString()));
        }
        public AppViewModel()
        {
            ClosingWindowCommand = new RelayCommand(p=> Application.Current.Shutdown(), p=>true);
            MinimizeWindowCommand = new RelayCommand(p => MinimizeWindow(), p => true);
            AddFolderCommand = new RelayCommand(p => AddFolder(), p => true);
            Folders = new ObservableCollection<Folder>();
        }
    }
}

using Planner.Models;
using Planner.Utilty;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Planner
{
    public class AppViewModel : ObservableObject
    {
        public RelayCommand ClosingWindowCommand { get; private set;}
        public RelayCommand MinimizeWindowCommand { get; private set; }
        public RelayCommand AddFolderCommand { get; private set; }
        public RelayCommand SelectFolderCommand { get; private set; }
        public RelayCommand AddTaskCommand { get; private set; }
        public ObservableCollection<Folder> Folders { get; set; }
        private string _inputText;
        public string InputText
        {
            get
            {
                return _inputText;
            }
            set
            {
                if (value == _inputText)
                    return;
                _inputText = value;
                OnPropertyChanged(nameof(InputText));
            }
        }
        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void AddFolder()
        {
            Folders.Add(new Folder(DateTime.Now.ToShortTimeString()));
        }

        private void SelectFolder(object parameter)
        {
            if (parameter != null)
            {
                foreach (Folder i in Folders)
                {
                    i.Selected = false;
                }
            (parameter as Folder).Selected = true;
            }
        }
        private void AddTask(object parameter)
        {
            if (parameter != null&&(parameter as string).Length>0)
                for(int i = 0; i < Folders.Count; i++)
                    if (Folders[i].Selected)
                    {
                        Folders[i].Tasks.Add(new Task(parameter.ToString()));
                        parameter = null;
                    }
            InputText = "";
        }
        private bool CanSelectFolder()
        {
            return Folders.Count > 0 ? true : false;
        }
        public AppViewModel()
        {
            ClosingWindowCommand = new RelayCommand(p=> Application.Current.Shutdown(), p=>true);
            MinimizeWindowCommand = new RelayCommand(p => MinimizeWindow(), p => true);
            AddFolderCommand = new RelayCommand(p => AddFolder(), p => true);
            SelectFolderCommand = new RelayCommand(p=>SelectFolder(p), p=>CanSelectFolder());
            AddTaskCommand = new RelayCommand(p => AddTask(p), p => true);
            Folders = new ObservableCollection<Folder>();
        }
    }
}

using Planner.Models;
using Planner.Utilty;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Planner
{
    public class AppViewModel : ObservableObject
    {
        private string _inputTaskText;
        private string _inputFolderText;
        private bool _isFolderInputVisible;
        
        public RelayCommand ClosingWindowCommand { get; private set;}
        public RelayCommand MinimizeWindowCommand { get; private set; }
        public RelayCommand AddFolderCommand { get; private set; }
        public RelayCommand SelectFolderCommand { get; private set; }
        public RelayCommand AddTaskCommand { get; private set; }
        public RelayCommand MakeTaskDoneCommand { get; private set; }
        
        public ObservableCollection<Folder> Folders { get; set; }
        public string InputText 
        {
            get
            {
                return _inputTaskText;
            }
            set
            {
                if (value == _inputTaskText)
                    return;
                _inputTaskText = value;
                OnPropertyChanged(nameof(InputText));
            }
        }
        public string InputFolderText
        {
            get
            {
                return _inputFolderText;
            }
            set
            {
                if (value == _inputFolderText)
                    return;
                _inputFolderText = value;
                OnPropertyChanged(nameof(InputFolderText));
            }
        }
        public bool IsFolderInputVisible
        {
            get
            {
                return _isFolderInputVisible;
            }
            set
            {
                if (value == _isFolderInputVisible)
                    return;
                _isFolderInputVisible = value;
                OnPropertyChanged(nameof(IsFolderInputVisible));
            }
        }
        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void MakeTaskDone(object parameter)
        {
            if (parameter != null)
               (parameter as Task).Done = !(parameter as Task).Done;
        }
        private void AddFolder(object parameter)
        {
            if (CanAddFolder(parameter.ToString())&&IsFolderInputVisible)
            {
                Folders.Add(new Folder(parameter as string));
                IsFolderInputVisible = !IsFolderInputVisible;
                InputFolderText = "";
                return;
            }
            else
                IsFolderInputVisible = !IsFolderInputVisible;
            
        }
        private bool CanAddFolder(string InputText)
        {
            if (InputText!=null&&InputText.Length>0)
                return true;
            return false;
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
        private void AddTask()
        {
            if (InputText!=null&&InputText.Length>0)
                for(int i = 0; i < Folders.Count; i++)
                    if (Folders[i].Selected)
                        Folders[i].Tasks.Add(new Task(InputText));
            InputText = "";
        }
        public AppViewModel()
        {
            Folders = new ObservableCollection<Folder>();
            ClosingWindowCommand = new RelayCommand(p=> Application.Current.Shutdown(), p=>true);
            MinimizeWindowCommand = new RelayCommand(p => MinimizeWindow(), p => true);
            AddFolderCommand = new RelayCommand(p => AddFolder(p), p=>true);
            SelectFolderCommand = new RelayCommand(p=>SelectFolder(p), p=>true);
            AddTaskCommand = new RelayCommand(p => AddTask(), p => true);
            MakeTaskDoneCommand = new RelayCommand(p => MakeTaskDone(p), p => true);
        }
    }
}

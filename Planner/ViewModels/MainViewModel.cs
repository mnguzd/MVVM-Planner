using Planner.Models;
using Planner.Utilty;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace Planner
{
    public class MainViewModel : ObservableObject
    {
        private string _inputTaskText;
        private string _inputFolderText;
        private double _rightColumnWidth = 800;
        private Folder _selectedFolder;
        private readonly DataService _service;

        public RelayCommand ClosingWindowCommand { get;}
        public RelayCommand MinimizeWindowCommand { get;}
        public RelayCommand AddFolderCommand { get;}
        public RelayCommand SelectFolderCommand { get;}
        public RelayCommand AddTaskCommand { get;}
        public RelayCommand MakeTaskDoneCommand { get;}
        public RelayCommand DeleteTaskCommand { get;}
        public RelayCommand DeleteFolderCommand { get;}
        public RelayCommand ChangeRightColumnWidthCommand { get;}
        public RelayCommand MakeTaskInProgressCommand { get;}


        public ObservableCollection<Folder> Folders { get;}

        public Folder SelectedFolder
        {
            get
            {
                return _selectedFolder;
            }
            set
            {
                OnPropertyChanged(ref _selectedFolder, value);
            }
        }

        public double RightColumnWidth
        {
            get
            {
                return _rightColumnWidth;
            }
            set
            {
                OnPropertyChanged(ref _rightColumnWidth, value);
            }
        }
        public string InputTaskText
        {
            get
            {
                return _inputTaskText;
            }
            set
            {
                OnPropertyChanged(ref _inputTaskText, value);
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
                OnPropertyChanged(ref _inputFolderText, value);
            }
        }

        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void DeleteFolder(object parameter)
        {
            if (parameter != null)
            {
                Folders.Remove(parameter as Folder);
                if (Folders.Count > 0)
                    Folders[Folders.Count - 1].Selected = true;
                else
                    foreach (Folder i in Folders)
                        i.Selected = false;
            }
        }
        private void MakeTaskDone(object parameter)
        {
            if (parameter != null)
            {
                (parameter as TaskModel).Done = !(parameter as TaskModel).Done;
                try
                {
                    if ((parameter as TaskModel).Done)
                    {
                        (parameter as TaskModel).InProgress = false;
                        SelectedFolder.NumberOfDoneTasks++;
                        SelectedFolder.NumberOfTasksInProgress--;
                    }
                    else
                    {
                        SelectedFolder.NumberOfDoneTasks--;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Oops, there was an error : " + ex.ToString());
                }
            }
        }

        private void AddFolder()
        {
            if (CanAddText(InputFolderText))
                Folders.Add(new Folder(InputFolderText));
            InputFolderText = "";
            foreach(Folder folder in Folders)
            {
                folder.Selected = false;
            }
            Folders[Folders.Count - 1].Selected = true; 
        }
        private bool CanAddText(string InputText)
        {
            return !string.IsNullOrWhiteSpace(InputText);
        }

        private void DeleteTask(object parameter)
        {
            if (parameter != null)
            {
                if ((parameter as TaskModel).Done)
                {
                    SelectedFolder.Tasks.Remove(parameter as TaskModel);
                    SelectedFolder.NumberOfDoneTasks--;
                }
                else if ((parameter as TaskModel).InProgress)
                {
                    SelectedFolder.Tasks.Remove(parameter as TaskModel);
                    SelectedFolder.NumberOfTasksInProgress--;
                }
                else
                {
                    SelectedFolder.Tasks.Remove(parameter as TaskModel);
                }
            }
        }
        private void ChangeRightColumnWidth()
        {
            if (RightColumnWidth == 800)
                RightColumnWidth = 1000; //This changes the LineWidth for ProgressLine 
            else
                RightColumnWidth = 800;
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
        private void ClosingWindow()
        {
            Application.Current.Shutdown();
            try
            {
                _service.SaveData(Folders);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Oops, there was an error : " + ex.ToString());
            }
        }

        private void MakeTaskInProgress(object parameter)
        {
            if (parameter != null)
            {
                int index = SelectedFolder.Tasks.IndexOf(parameter as TaskModel);
                SelectedFolder.Tasks[index].InProgress = !SelectedFolder.Tasks[index].InProgress;
                if (SelectedFolder.Tasks[index].InProgress)
                {
                    SelectedFolder.NumberOfTasksInProgress++;
                    SelectedFolder.Tasks[index].Done = false;
                    SelectedFolder.NumberOfDoneTasks--;
                    return;
                }
                SelectedFolder.NumberOfTasksInProgress--;
            }
        }


        private void AddTask()
        {
            if (CanAddText(InputTaskText))
                SelectedFolder.Tasks.Add(new TaskModel(InputTaskText));
            InputTaskText = "";
        }
        public MainViewModel(DataService dataService)
        {
            try
            {
                _service = dataService;
                Folders = dataService.LoadData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Oops, there was an error : " + ex.ToString());
            }
            ClosingWindowCommand = new RelayCommand(p => ClosingWindow(), p => true);
            MinimizeWindowCommand = new RelayCommand(p => MinimizeWindow(), p => true);
            AddFolderCommand = new RelayCommand(p => AddFolder(), p => true);
            SelectFolderCommand = new RelayCommand(p => SelectFolder(p), p => true);
            AddTaskCommand = new RelayCommand(p => AddTask(), p => true);
            MakeTaskDoneCommand = new RelayCommand(p => MakeTaskDone(p), p => true);
            DeleteTaskCommand = new RelayCommand(p => DeleteTask(p), p => true);
            DeleteFolderCommand = new RelayCommand(p => DeleteFolder(p), p => true);
            ChangeRightColumnWidthCommand = new RelayCommand(p => ChangeRightColumnWidth(), p => true);
            MakeTaskInProgressCommand = new RelayCommand(p => MakeTaskInProgress(p), p => true);
        }
    }
}

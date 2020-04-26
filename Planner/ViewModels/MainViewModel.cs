using Planner.Models;
using Planner.Utilty;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Planner
{
    public class MainViewModel : ObservableObject
    {
        private string _inputTaskText;
        private string _inputFolderText;
        private double _lineWidth;
        private double _rightColumnWidth = 800;
        private double _leftColumnWidth = 200;
        private readonly DataService service = new DataService("FolderData.txt");

        public RelayCommand ClosingWindowCommand { get; private set; }
        public RelayCommand MinimizeWindowCommand { get; private set; }
        public RelayCommand AddFolderCommand { get; private set; }
        public RelayCommand SelectFolderCommand { get; private set; }
        public RelayCommand AddTaskCommand { get; private set; }
        public RelayCommand MakeTaskDoneCommand { get; private set; }
        public RelayCommand DeleteTaskCommand { get; private set; }
        public RelayCommand DeleteFolderCommand { get; private set; }
        public RelayCommand ChangeRightColumnWidthCommand { get; private set; }
        public RelayCommand ChangeRightColumnWidthBackCommand { get; private set; }
        public RelayCommand MakeTaskInProgressCommand { get; private set; }


        public ObservableCollection<Folder> Folders { get; set; }

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
        public double LeftColumnWidth
        {
            get
            {
                return _leftColumnWidth;
            }
            set
            {
                OnPropertyChanged(ref _leftColumnWidth, value);
            }
        }

        public double LineWidth
        {
            get
            {
                if (SelectedFolder().Tasks.Count == 0 || SelectedFolder() == null)
                    return 0;
                double percentOfDone = Convert.ToDouble(SelectedFolder().NumberOfDoneTasks) / Convert.ToDouble(SelectedFolder().Tasks.Count);
                _lineWidth = RightColumnWidth * percentOfDone;
                return _lineWidth;
            }
            set
            {
                OnPropertyChanged(ref _lineWidth, value);
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
                OnPropertyChanged(nameof(LineWidth));
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
                        SelectedFolder().NumberOfDoneTasks++;
                        SelectedFolder().NumberOfTasksInProgress--;
                    }
                    else
                    {
                        SelectedFolder().NumberOfDoneTasks--;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Oops, there was an error : " + ex.ToString());
                }
                OnPropertyChanged(nameof(LineWidth));
            }
        }

        private Folder SelectedFolder()
        {
            foreach (Folder x in Folders)
            {
                if (x.Selected)
                    return x;
            }
            return null;
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
            OnPropertyChanged(nameof(LineWidth));
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
                    SelectedFolder().Tasks.Remove(parameter as TaskModel);
                    SelectedFolder().NumberOfDoneTasks--;
                }
                else if ((parameter as TaskModel).InProgress)
                {
                    SelectedFolder().Tasks.Remove(parameter as TaskModel);
                    SelectedFolder().NumberOfTasksInProgress--;
                }
                else
                {
                    SelectedFolder().Tasks.Remove(parameter as TaskModel);
                }
                OnPropertyChanged(nameof(LineWidth));
            }
        }
        private void ChangeRightColumnWidth()
        {
            RightColumnWidth = 1000; //This changes the LineWidth for ProgressLine 
            OnPropertyChanged(nameof(LineWidth));
        }
        private void ChangeRightColumnWidthBack()
        {
            RightColumnWidth = 800; //This changes the LineWidth for ProgressLine 
            OnPropertyChanged(nameof(LineWidth));
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
                OnPropertyChanged(nameof(LineWidth));
            }
        }
        private void ClosingWindow()
        {
            Application.Current.Shutdown();
            try
            {
                service.SaveData(Folders);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops, there was an error : " + ex.ToString());
            }
        }

        private void MakeTaskInProgress(object parameter)
        {
            if (parameter != null)
            {
                int index = SelectedFolder().Tasks.IndexOf(parameter as TaskModel);
                SelectedFolder().Tasks[index].InProgress = !SelectedFolder().Tasks[index].InProgress;
                if (SelectedFolder().Tasks[index].InProgress)
                {
                    SelectedFolder().NumberOfTasksInProgress++;
                    SelectedFolder().Tasks[index].Done = false;
                    SelectedFolder().NumberOfDoneTasks--;
                    OnPropertyChanged(nameof(LineWidth));
                    return;
                }
                SelectedFolder().NumberOfTasksInProgress--;
            }
        }



        private void AddTask()
        {
            if (CanAddText(InputTaskText))
                SelectedFolder().Tasks.Add(new TaskModel(InputTaskText));
            InputTaskText = "";
            OnPropertyChanged(nameof(LineWidth));
        }
        public MainViewModel()
        {
            try
            {
                Folders = service.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops, there was an error : " + ex.ToString());
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
            ChangeRightColumnWidthBackCommand = new RelayCommand(p => ChangeRightColumnWidthBack(), p => true);
            MakeTaskInProgressCommand = new RelayCommand(p => MakeTaskInProgress(p), p => true);
        }
    }
}

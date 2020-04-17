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
        private bool _isFolderInputFocused;
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


        public ObservableCollection<Folder> Folders { get; set; }

        public double RightColumnWidth
        {
            get
            {
                return _rightColumnWidth;
            }
            set
            {
                if (value == _rightColumnWidth)
                    return;
                _rightColumnWidth = value;
                OnPropertyChanged(nameof(RightColumnWidth));
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
                if (value == _leftColumnWidth)
                    return;
                _leftColumnWidth = value;
                OnPropertyChanged(nameof(LeftColumnWidth));
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
                if (value == _inputTaskText)
                    return;
                _inputTaskText = value;
                OnPropertyChanged(nameof(InputTaskText));
            }
        }
        public bool IsFolderInputFocused
        {
            get
            {
                return _isFolderInputFocused;
            }
            set
            {
                if (value == _isFolderInputFocused)
                    return;
                _isFolderInputFocused = value;
                OnPropertyChanged(nameof(IsFolderInputFocused));
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
        private void DeleteFolder(object parameter)
        {
            if (parameter != null)
            {
                Folders.Remove(parameter as Folder);
                if (Folders.Count > 0)
                    Folders[Folders.Count - 1].Selected = true;
                else
                    foreach(Folder i in Folders)
                        i.Selected = false;
            }
        }
        private void MakeTaskDone(object parameter)
        {
            if (parameter != null)
            {
                (parameter as TaskModel).Done = !(parameter as TaskModel).Done;
                if ((parameter as TaskModel).Done)
                    for (int i = 0; i < Folders.Count; i++)
                        if (Folders[i].Selected)
                            Folders[i].NumberOfDoneTasks++;
                        else
                            for (int g = 0; g < Folders.Count; g++)
                                if (Folders[g].Selected)
                                    Folders[g].NumberOfDoneTasks--;
            }
        }
        private void AddFolder(object parameter)
        {
            if (CanAddText(parameter.ToString()) && IsFolderInputVisible)
            {
                Folders.Add(new Folder(parameter as string));
                IsFolderInputVisible = !IsFolderInputVisible;
                return;
            }
            else
            {
                InputFolderText = "";
                IsFolderInputVisible = !IsFolderInputVisible;
            }
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
                    for (int i = 0; i < Folders.Count; i++)
                        if (Folders[i].Selected)
                        {
                            Folders[i].Tasks.Remove(parameter as TaskModel);
                            Folders[i].NumberOfDoneTasks--;
                        }
                }
                else
                {
                    for (int g = 0; g < Folders.Count; g++)
                        if (Folders[g].Selected)
                            Folders[g].Tasks.Remove(parameter as TaskModel);
                }
            }
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
                service.SaveData(Folders);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops, there was an error : " + ex.ToString());
            }
        }
        private void AddTask()
        {
            if (CanAddText(InputTaskText))
                for (int i = 0; i < Folders.Count; i++)
                    if (Folders[i].Selected)
                        Folders[i].Tasks.Add(new TaskModel(InputTaskText));
            InputTaskText = "";
        }
        public AppViewModel()
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
            AddFolderCommand = new RelayCommand(p => AddFolder(p), p => true);
            SelectFolderCommand = new RelayCommand(p => SelectFolder(p), p => true);
            AddTaskCommand = new RelayCommand(p => AddTask(), p => true);
            MakeTaskDoneCommand = new RelayCommand(p => MakeTaskDone(p), p => true);
            DeleteTaskCommand = new RelayCommand(p => DeleteTask(p), p => true);
            DeleteFolderCommand = new RelayCommand(p => DeleteFolder(p), p => true);
            IsFolderInputFocused = true;
        }
    }
}

using Planner.Models;
using Planner.Utilty;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace Planner
{
    public class AppViewModel : ObservableObject
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
                int index = new int();
                for (int i = 0; i < Folders.Count; i++)
                {
                    if (Folders[i].Selected)
                    {
                        index = i;
                    }
                }
                if (Folders[index].Tasks.Count == 0)
                {
                    return 0;
                }
                double percentOfDone = Convert.ToDouble(Folders[index].NumberOfDoneTasks) / Convert.ToDouble(Folders[index].Tasks.Count);
                _lineWidth = RightColumnWidth * percentOfDone;
                return _lineWidth;
            }
            set
            {
                if (value == _lineWidth)
                    return;
                _lineWidth = value;
                OnPropertyChanged(nameof(LineWidth));
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
                {
                    for (int i = 0; i < Folders.Count; i++)
                        if (Folders[i].Selected)
                            Folders[i].NumberOfDoneTasks++;
                }
                else
                {
                    for (int g = 0; g < Folders.Count; g++)
                        if (Folders[g].Selected)
                            Folders[g].NumberOfDoneTasks--;
                }
                OnPropertyChanged(nameof(LineWidth));
            }
        }
        private void AddFolder()
        {
            if (CanAddText(InputFolderText))
                        Folders.Add(new Folder(InputFolderText));
            InputFolderText = "";
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
        private void AddTask()
        {
            if (CanAddText(InputTaskText))
                for (int i = 0; i < Folders.Count; i++)
                    if (Folders[i].Selected)
                        Folders[i].Tasks.Add(new TaskModel(InputTaskText));
            InputTaskText = "";
            OnPropertyChanged(nameof(LineWidth));
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
            AddFolderCommand = new RelayCommand(p => AddFolder(), p => true);
            SelectFolderCommand = new RelayCommand(p => SelectFolder(p), p => true);
            AddTaskCommand = new RelayCommand(p => AddTask(), p => true);
            MakeTaskDoneCommand = new RelayCommand(p => MakeTaskDone(p), p => true);
            DeleteTaskCommand = new RelayCommand(p => DeleteTask(p), p => true);
            DeleteFolderCommand = new RelayCommand(p => DeleteFolder(p), p => true);
            ChangeRightColumnWidthCommand = new RelayCommand(p => ChangeRightColumnWidth(), p => true); 
            ChangeRightColumnWidthBackCommand = new RelayCommand(p => ChangeRightColumnWidthBack(), p => true);
        }
    }
}

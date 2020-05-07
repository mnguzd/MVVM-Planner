using Planner.Utilty;
using System.Collections.ObjectModel;

namespace Planner.Models
{
    public class Folder : ObservableObject
    {
        //private fields
        private bool _selected;
        private string _name;
        private double _numberOfDoneTasks;
        private double _numberOfTasksInProgress;
        private ObservableCollection<TaskModel> _tasks;
        public Folder(string folderName)
        {
            Name = folderName;
            Tasks = new ObservableCollection<TaskModel>();
            Selected = false;
        }
        public double NumberOfDoneTasks
        {
            get
            {
                _numberOfDoneTasks = 0;
                for (int i = 0; i < Tasks.Count; i++)
                    if (Tasks[i].Done)
                        _numberOfDoneTasks++;
                return _numberOfDoneTasks;
            }
            set => OnPropertyChanged(ref _numberOfDoneTasks, value);
        }
        public double NumberOfTasksInProgress
        {
            get
            {
                _numberOfTasksInProgress = 0;
                for (int i = 0; i < Tasks.Count; i++)
                    if (Tasks[i].InProgress)
                        _numberOfTasksInProgress++;
                return _numberOfTasksInProgress;
            }
            set => OnPropertyChanged(ref _numberOfTasksInProgress, value);
        }
        public ObservableCollection<TaskModel> Tasks
        {
            get => _tasks;
            set => OnPropertyChanged(ref _tasks, value);
        }
        public string Name
        {
            get => _name;
            set => OnPropertyChanged(ref _name, value);
        }
        public bool Selected
        {
            get => _selected;
            set => OnPropertyChanged(ref _selected, value);
        }
    }
}
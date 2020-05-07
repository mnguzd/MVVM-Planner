using Planner.Utilty;
using System;

namespace Planner.Models
{
    public class TaskModel : ObservableObject
    {
        private string _toDo;
        private bool _isDone;
        private bool _isInProgress;
        public TaskModel(string newTask)
        {
            ToDo = newTask;
        }
        public string ToDo
        {
            get => _toDo;
            set => OnPropertyChanged(ref _toDo, value);
        }
        public string CreationDate { get; set; } = DateTime.Now.ToShortTimeString();
        public bool Done
        {
            get => _isDone;
            set => OnPropertyChanged(ref _isDone, value);
        }
        public bool InProgress
        {
            get => _isInProgress;
            set => OnPropertyChanged(ref _isInProgress, value);
        }
    }
}

using Planner.Utilty;
using System;

namespace Planner.Models
{
    public class TaskModel:ObservableObject
    {
        private string _toDo;
        private bool _isDone;
        private bool _isInProcess;
        public TaskModel(string newtask)
        {
            ToDo = newtask;
        }
        public string ToDo 
        {
            get 
            {
                return _toDo; 
            } 
            set 
            { 
                OnPropertyChanged(ref _toDo, value);
            } 
        }
        public bool Done 
        {
            get 
            {
                return _isDone;
            }
            set 
            {
                OnPropertyChanged(ref _isDone, value);
            }
        }
        public bool InProcess
        {
            get
            {
                return _isInProcess;
            }
            set
            {
                OnPropertyChanged(ref _isInProcess, value);
            }
        }
    }
}

using Planner.Utilty;
using System;

namespace Planner.Models
{
    public class TaskModel:ObservableObject
    {
        private string _toDo;
        private bool _done;
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
                if (value == _toDo) 
                    return;
                _toDo = value; OnPropertyChanged(nameof(ToDo));
            } 
        }
        public bool Done 
        {
            get 
            {
                return _done;
            }
            set 
            {
                if (value == _done)
                    return;
                _done = value; OnPropertyChanged(nameof(Done));
            }
        }
        public string Date { get; private set; } = DateTime.Now.ToShortTimeString();
    }
}

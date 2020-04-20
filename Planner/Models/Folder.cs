using Planner.Utilty;
using System.Collections.ObjectModel;

namespace Planner.Models
{
    public class Folder : ObservableObject
    {
        //private fields
        private bool _selected;
        private string _name;
        private int _numberOfDoneTasks;
        private ObservableCollection<TaskModel> _tasks;
        public Folder(string folderName)
        {
            Name = folderName;
            Tasks = new ObservableCollection<TaskModel>();
            Selected = false;
        }

        public ObservableCollection<TaskModel> Tasks 
        { get 
            { return _tasks; 
            } 
            set 
            {
                if (value == _tasks)
                    return;
                _tasks = value;
                OnPropertyChanged(nameof(NumberOfDoneTasks));
                OnPropertyChanged(nameof(Tasks));
            } 
        }
        public int NumberOfDoneTasks 
        {
            get
            {
                _numberOfDoneTasks=0;
                for (int i = 0; i < Tasks.Count; i++)
                    if (Tasks[i].Done)
                        _numberOfDoneTasks++;
                return _numberOfDoneTasks;
            }
            set
            {
                OnPropertyChanged(ref _numberOfDoneTasks, value);
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                OnPropertyChanged(ref _name, value);
            }
        }
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                OnPropertyChanged(ref _selected, value);
            }
        }
    }
}

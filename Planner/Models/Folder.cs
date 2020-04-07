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
        private ObservableCollection<Task> _tasks;
        public Folder(string folderName)
        {
            Name = folderName;
            Tasks = new ObservableCollection<Task>();
            Selected = false;
        }

        public ObservableCollection<Task> Tasks 
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
                if (value == _numberOfDoneTasks)
                    return;
                _numberOfDoneTasks = value;
                OnPropertyChanged(nameof(NumberOfDoneTasks));
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
                if (value == _name)
                    return;
                _name = value; OnPropertyChanged(nameof(Name));
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
                if (value == _selected)
                    return;
                _selected = value; OnPropertyChanged(nameof(Selected));
            }
        }
    }
}

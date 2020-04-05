using Planner.Utilty;
using System.Collections.ObjectModel;

namespace Planner.Models
{
    public class Folder : ObservableObject
    {
        //private fields
        private bool _selected;
        private string _name;
        public Folder(string folderName)
        {
            Name = folderName;
            Tasks = new ObservableCollection<Task>();
            Selected = false;
        }

        public ObservableCollection<Task> Tasks { get; set; }
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

using Planner.Utilty;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Planner.Models
{
    public class Folder:INotifyPropertyChanged
    {
        public Folder(string str, string task)
        {
            Name = str;
            Tasks = new ObservableCollection<Task>();
            Selected = false;
            for(int i=0;i<Convert.ToInt32(task);i++)
                Tasks.Add(new Task(task));
        }
        public ObservableCollection<Task> Tasks { get; set; }
        private string _name;
        public string Name { get { return _name; } set { if (value == _name) return; else { _name = value; OnPropertyChanged(nameof(Name)); } } }
        private bool _isSelected { get; set; }
        public bool Selected { get { return _isSelected; } set { if (value == _isSelected) return; else { _isSelected = value; OnPropertyChanged(nameof(Selected)) ; } } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

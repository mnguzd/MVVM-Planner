using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Models
{
    public class Folder
    {
        public Folder(string str)
        {
            Name = str;
        }
        public Folder(string str, string task)
        {
            Name = str;
            Tasks = new ObservableCollection<Task>();
            Tasks.Add(new Task(task));
        }
        public ObservableCollection<Task> Tasks { get; set; }
        public string Name { get; set; }
    }
}

using System;
using System.Collections.ObjectModel;

namespace Planner.Models
{
    public class Folder
    {
        public Folder(string str, string task)
        {
            Name = str;
            Tasks = new ObservableCollection<Task>();
            for(int i=0;i<Convert.ToInt32(task);i++)
                Tasks.Add(new Task(task));
        }
        public ObservableCollection<Task> Tasks { get; set; }
        public string Name { get; set; }
    }
}

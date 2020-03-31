using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Models
{
    public class Task
    {
        public Task(string str)
        {
            ToDo = str;
        }
        public string ToDo { get; set; }
        public bool Done { get; set; }
        public string Date { get; set; } = DateTime.Now.ToShortTimeString();
    }
}

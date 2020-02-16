using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoWpf
{
    class Doings
    {
        public string date { get; set; } = DateTime.Now.ToShortTimeString();
        public string ToDo { get; set; }
        public bool Done { get; set; }
    }
}

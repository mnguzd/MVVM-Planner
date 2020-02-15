using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoWpf
{
    class ToDoInfo
    {
        public string date = DateTime.Now.ToString();
        public string ToDo { get; set; }
        public bool Done { get; set; }
    }
}

using System;
using System.ComponentModel;

namespace ToDoWpf
{
    class Doings
    {
        public string Time { get; set; } = DateTime.Now.ToShortTimeString();
        public string ToDo { get; set; }
        public bool Done { get; set; }
        public string Date { get;  set; } = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
    }
}

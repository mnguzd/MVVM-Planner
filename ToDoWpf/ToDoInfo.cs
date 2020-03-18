
using System;
namespace ToDoWpf
{
    class Doings
    {
        public string Time { get; set; } = DateTime.Now.ToShortTimeString();
        public string ToDo { get; set; }
        public bool Done { get; set; } = false;
        public string Date = DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString();
    }
}

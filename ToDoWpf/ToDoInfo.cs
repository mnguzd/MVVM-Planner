using System;

namespace ToDoWpf
{
    class Doings
    {
        public string Date { get; set; } = DateTime.Now.ToShortTimeString();
        public string ToDo { get; set; }
        public bool Done { get; set; }
    }
}

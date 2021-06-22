using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;

namespace bus.Model
{
    public class Stop : INotifyPropertyChanged
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Tag { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string RouteTag { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

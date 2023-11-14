using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Menu.MainWindow.Model
{
    public class MenuItem
    {
        public ObservableCollection<MenuItem>? SubItems { get; set; }

        public string? MenuTitle { get; set; }
        public string? PMenu { get; set; }
        public string? URI { get; set; }
    }
}

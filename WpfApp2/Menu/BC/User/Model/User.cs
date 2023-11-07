using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Common;

namespace WpfApp2.Menu.BC.User.Model
{
    public class User : BaseModel
    {
        private int? id;
        private string? name;
        private string? address;
        private int? age;

        public int? ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string? Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public int? Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged(nameof(Age));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Backend.Annotations;
using SharedLib.Models;

namespace Backend.Models
{
    public class BackendProductCategory : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Notify("Name");
            }
        }

        public List<Product> Products { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify([CallerMemberName]string propName = null)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}

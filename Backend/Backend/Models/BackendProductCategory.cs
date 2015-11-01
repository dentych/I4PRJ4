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
   
    public class BackendProductCategory : ProductCategory, INotifyPropertyChanged
    {

        public string BName
        {
            get { return Name; }
            set
            {
                Name = value;
                Notify("Name");
            }
        }


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

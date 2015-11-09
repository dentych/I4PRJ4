using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Backend.Annotations;
using SharedLib.Models;
using System.Threading;

namespace Backend.Models
{
   
    public class BackendProductCategory : ProductCategory, INotifyPropertyChanged
    {
        private Mutex _mutex = new Mutex();

        public string BName
        {
            get { return Name; }
            set
            {
                Name = value;
                Notify("Name");
            }
        }

        public void AddProduct(Product product)
        {
            _mutex.WaitOne();
            Products.Add(product);
            _mutex.ReleaseMutex();
        }

        public void RemoveProductAt(int index)
        {
            _mutex.WaitOne();
            Products.RemoveAt(index);
            _mutex.ReleaseMutex();
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

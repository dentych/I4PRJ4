using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using SharedLib.Models;
using System.Windows;

namespace Backend.Models.Datamodels
{
   
    public class BackendProductCategory : ProductCategory, INotifyPropertyChanged
    {
        private Mutex _mutex = new Mutex();

        public string BName
        {
            get { return Name; }
            set
            {
                _mutex.WaitOne();
                Name = value;
                Notify("Name");
                _mutex.ReleaseMutex();
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

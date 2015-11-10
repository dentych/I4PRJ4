using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using SharedLib.Models;

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

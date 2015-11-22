using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using SharedLib.Models;
using System.Windows;

namespace Backend.Models.Datamodels
{
    /// <summary>
    /// Specific category, containing a list of products.
    /// </summary>
    public class BackendProductCategory : ProductCategory, INotifyPropertyChanged
    {
        private Mutex _mutex = new Mutex();

        /// <summary>
        /// Name of the category. This gets/sets the ProductCategory "Name" property, but notifies when set, unlike the Name property which does not notify.
        /// </summary>
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

        /// <summary>
        /// Add Product to this category.
        /// </summary>
        /// <param name="product">The Product to add</param>
        public void AddProduct(Product product)
        {
            _mutex.WaitOne();
            Products.Add(product);
            _mutex.ReleaseMutex();
        }

        /// <summary>
        /// Remove Product from the category by index.
        /// </summary>
        /// <param name="index">The index of the Product to remove.</param>
        public void RemoveProductAt(int index)
        {
            _mutex.WaitOne();
            Products.RemoveAt(index);
            _mutex.ReleaseMutex();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Notifies of a changed property. Used for data binding purposes.
        /// </summary>
        /// <param name="propName">The name of the changed property.</param>
        protected void Notify([CallerMemberName]string propName = null)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}

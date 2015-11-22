using System.ComponentModel;
using SharedLib.Models;

namespace Backend.Models.Datamodels
{
    /// <summary>
    /// An override of the SharedLib Product, which implements data binding notifications when settings change.
    /// </summary>
    public class BackendProduct : Product, INotifyPropertyChanged
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string BName
        {
            get { return Name; }
            set
            {
                Name = value;
                OnPropertyChanged("BName");
            }
        }

        /// <summary>
        /// Barcode for the product.
        /// </summary>
        public string BProductNumber
        {
            get { return ProductNumber; }
            set
            {
                ProductNumber = value;
                OnPropertyChanged("BProductNumber");
            }
        }

        private string aPrice;
        /// <summary>
        /// The price in string format.
        /// </summary>
        public string StringBPrice
        {
            get { return BPrice.ToString(); }
            set
            {
                aPrice = value;
                if (string.IsNullOrWhiteSpace(aPrice))
                {
                    BPrice = 0;
                    return;
                }
                BPrice = decimal.Parse(aPrice);
            }
        }

        /// <summary>
        /// The price in decimal format.
        /// </summary>
        public decimal BPrice
        {
            get { return Price; }
            set
            {
                Price = value;
                OnPropertyChanged("BPrice");
            }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
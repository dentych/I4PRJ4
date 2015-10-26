using System.ComponentModel;
using SharedLib.Models;

namespace Backend.Models
{
    public class BackendProduct : Product, INotifyPropertyChanged
    {
        public string BName
        {
            get { return Name; }
            set
            {
                Name = value;
                OnPropertyChanged("BName");
            }
        }

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

        public decimal BPrice
        {
            get { return Price; }
            set
            {
                Price = value;
                OnPropertyChanged("BPrice");
            }
        }

        private BackendProductCategory _category;
        public BackendProductCategory Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged("Category");
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
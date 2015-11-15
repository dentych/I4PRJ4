using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Models
{
    public class PurchasedProduct : INotifyPropertyChanged
    {
        private int _quantity;
        private decimal _unitPrice;
        public int PurchaseId;
        public int PurchasedProductId
        {
            get; set;
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                Notify("Quantity");
                Notify("TotalPrice");
            }
        }

        public string Name
        {
            get; set;
        }

        public string ProductNumber
        {
            get; set;
        }

        public decimal UnitPrice
        {
            get
            {
                return _unitPrice;
            }
            set
            {
                _unitPrice = value;
                Notify("UnitPrice");
            }
        }

        public decimal TotalPrice
        {
            get { return Quantity*UnitPrice; }
        }

        public PurchasedProduct()
        {
        }

        public PurchasedProduct(PurchasedProduct pp)
        {
            PurchasedProductId = pp.PurchasedProductId;
            Quantity = pp.Quantity;
            Name = pp.Name;
            ProductNumber = pp.ProductNumber;
            UnitPrice = pp.UnitPrice;
            PurchaseId = pp.PurchaseId;
        }

        public PurchasedProduct(Product product, int quantity, int purchaseId)
        {
            Name = product.Name;
            ProductNumber = product.ProductNumber;
            UnitPrice = product.Price;
            Quantity = quantity;
            PurchasedProductId = product.ProductId;
            PurchaseId = purchaseId;
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

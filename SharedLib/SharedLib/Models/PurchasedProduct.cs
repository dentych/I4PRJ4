using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Models
{
    /// <summary>
    /// Datamodel for purchasedproducts.
    /// </summary>
    public class PurchasedProduct : INotifyPropertyChanged
    {
        private int _quantity;
        private decimal _unitPrice;
        public int PurchaseId;

        /// <summary>
        /// Id for the purchased product to be indentified by.
        /// </summary>
        public int PurchasedProductId
        {
            get; set;
        }

        /// <summary>
        /// Amount of products bought.
        /// </summary>
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

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Product number of the product.
        /// </summary>
        public string ProductNumber
        {
            get; set;
        }

        /// <summary>
        /// the price of one product.
        /// </summary>
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

        /// <summary>
        /// Total price of the amount of products.
        /// </summary>
        public decimal TotalPrice
        {
            get { return Quantity*UnitPrice; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PurchasedProduct()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pp">Purchased product which attributes is to be copied</param>
        public PurchasedProduct(PurchasedProduct pp)
        {
            PurchasedProductId = pp.PurchasedProductId;
            Quantity = pp.Quantity;
            Name = pp.Name;
            ProductNumber = pp.ProductNumber;
            UnitPrice = pp.UnitPrice;
            PurchaseId = pp.PurchaseId;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="product">Product which attributes is to be copied</param>
        /// <param name="quantity">Amount of products</param>
        /// <param name="purchaseId">Id of the purchase which to insert the purchsed products into</param>
        public PurchasedProduct(Product product, int quantity, int purchaseId)
        {
            Name = product.Name;
            ProductNumber = product.ProductNumber;
            UnitPrice = product.Price;
            Quantity = quantity;
            PurchasedProductId = product.ProductId;
            PurchaseId = purchaseId;
        }

        /// <summary>
        /// Event that a property has changed, used by the Kasseapparat
        /// </summary>
        public new event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Eventhandler
        /// </summary>
        /// <param name="propertyName">the property which is changed</param>
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

﻿using System;
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
        private uint _quantity;
        private decimal _unitPrice;

        public uint Quantity
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
            Quantity = pp.Quantity;
            Name = pp.Name;
            ProductNumber = pp.ProductNumber;
            UnitPrice = pp.UnitPrice;
        }

        public PurchasedProduct(Product product, uint quantity)
        {
            Name = product.Name;
            ProductNumber = product.ProductNumber;
            UnitPrice = product.Price;
            Quantity = quantity;
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
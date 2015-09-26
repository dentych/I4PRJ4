///////////////////////////////////////////////////////////
//  Product.cs
//  Implementation of the Class PrjProduct
//  Generated by Enterprise Architect
//  Created on:      26-sep-2015 19:47:55
//  Original author: benla
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Globalization;
using System.Windows.Markup;
using SharedLib.Models;

namespace Backend.OpretProdukt
{
    public class PrjProduct : Product, IProduct
    {
        public string name { set { Name = value; } get { return name; } }
        public string productnumber { set { ProductNumber = value; } get { return productnumber; } }
        public decimal price { set { Price = value; } get { return price; } }



        public Dictionary<string, string> GetData()
        {
            var dict = new Dictionary<string, string>();
            dict["ProductNumber"] = ProductNumber;
            dict["Name"] = Name;
            dict["Price"] = Price.ToString(CultureInfo.InvariantCulture);
            return dict;
        }

        public List<string> GetIndex()
        {
            return new List<string> {"ProductNumber", "Name", "Price"};
        }
    }
} //end PrjProduct
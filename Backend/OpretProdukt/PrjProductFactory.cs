///////////////////////////////////////////////////////////
//  PrjProductManager.cs
//  Implementation of the Class PrjProductManager
//  Generated by Enterprise Architect
//  Created on:      26-sep-2015 19:47:55
//  Original author: benla
///////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace Backend.OpretProdukt
{
    public class PrjProductFactory : ProductFactory
    {
        protected IProduct _myProduct;
        protected IProtocol _myProtocol;

        public PrjProductFactory()
        {
            _myProtocol = MakeProtocol();
        }

        public override IProduct myProduct
        {
            get { return _myProduct; }
            set { _myProduct = value; }
        }

        public override IProtocol myProtocol
        {
            get { return _myProtocol; }
            set { _myProtocol = value; }
        }

        public override bool AddProduct(Dictionary<string, string> propDictionary)
        {
            _myProduct = MakeProduct(propDictionary);
            return true;
        }

        protected override string EncodeProduct()
        {
            return _myProtocol.GetCmdString(_myProduct);
        }

        protected override sealed IProduct MakeProduct(Dictionary<string, string> propDictionary)
        {
            var myProduct = new PrjProduct();
            myProduct.name = propDictionary["Name"];
            myProduct.price = decimal.Parse(propDictionary["Price"]);
            myProduct.productnumber = propDictionary["ProductNumber"];
            return myProduct;
        }

        protected override sealed IProtocol MakeProtocol()
        {
            return new PrjProtocol();
        }
    }
} //end PrjProductManager
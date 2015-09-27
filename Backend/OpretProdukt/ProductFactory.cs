///////////////////////////////////////////////////////////
//  ProductManager.cs
//  Implementation of the Class ProductManager
//  Generated by Enterprise Architect
//  Created on:      26-sep-2015 19:47:55
//  Original author: benla
///////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace Backend.OpretProdukt
{
    public abstract class ProductFactory
    {
        public abstract IProduct myProduct { get; set; }
        public abstract IProtocol myProtocol { get; set; }

        public abstract bool AddProduct(Dictionary<string, string> propDictionary);
        protected abstract string EncodeProduct();
        protected abstract IProduct MakeProduct(Dictionary<string, string> propDictionary);
        protected abstract IProtocol MakeProtocol();
    }
} //end ProductManager
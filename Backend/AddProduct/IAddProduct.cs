///////////////////////////////////////////////////////////
//  IAddProduct.cs
//  Implementation of the Interface IAddProduct
//  Generated by Enterprise Architect
//  Created on:      28-sep-2015 17:43:51
//  Original author: benja
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Backend.AddProduct
{
    public interface IAddProduct
    {
        bool CreateProduct();

        string LastError { get; }
    }//end IAddProduct
}
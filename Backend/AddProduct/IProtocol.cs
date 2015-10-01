///////////////////////////////////////////////////////////
//  IProtocol.cs
//  Implementation of the Interface IProtocol
//  Generated by Enterprise Architect
//  Created on:      28-sep-2015 17:43:51
//  Original author: benja
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SharedLib.Models;


namespace Backend.AddProduct
{
    public interface IProtocol
    {
        string ProductXMLParser(Product toParse);
    }//end IProtocol
}
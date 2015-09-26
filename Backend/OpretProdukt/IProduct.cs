///////////////////////////////////////////////////////////
//  IProduct.cs
//  Implementation of the Interface IProduct
//  Generated by Enterprise Architect
//  Created on:      26-sep-2015 19:47:54
//  Original author: benla
///////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace Backend.OpretProdukt
{
    public interface IProduct
    {
        Dictionary<string, string> GetData();
        List<string> GetIndex();
    }
}//end IProduct
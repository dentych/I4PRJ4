///////////////////////////////////////////////////////////
//  PrjProtokol.cs
//  Implementation of the Class PrjProtokol
//  Generated by Enterprise Architect
//  Created on:      28-sep-2015 17:43:51
//  Original author: benja
///////////////////////////////////////////////////////////

using Backend.Models;
using SharedLib.Models;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;

namespace Backend.Brains
{
    public class PrjProtokol : IProtocol
    {
        public Protocol LocalProtocol { get; set; }

        public string ProductXMLParser(Product toParse)
        {
            return LocalProtocol.Encode(new CreateProductCmd(toParse));
        }

        public string CategoryXMLParser(BackendProductCategory toParse)
        {
            return LocalProtocol.Encode(new CreateProductCategoryCmd(toParse));
        }

        public string EditProductXMLParser(BackendProduct toParse)
        {
            return LocalProtocol.Encode(new EditProductCmd(toParse));
        }

        public string EditCategoryXMLParser(BackendProductCategory toParse)
        {
            return LocalProtocol.Encode(new EditProductCategoryCmd(toParse));
        }

        public string DeleteCategoryXMLParser(BackendProductCategory toParse)
        {
            return LocalProtocol.Encode(new DeleteProductCategoryCmd(toParse));
        }

        public string DeleteProductXMLParser(BackendProduct toParse)
        {
            return LocalProtocol.Encode(new DeleteProductCmd(toParse));
        }

        public string GetCatalougXMLParser()
        {
            return LocalProtocol.Encode(new GetCatalogueCmd());
        }
    } //end PrjProtokol
}

///////////////////////////////////////////////////////////
//  AddProductCB.cs
//  Implementation of the Class AddProductCB
//  Generated by Enterprise Architect
//  Created on:      28-sep-2015 17:43:46
//  Original author: benja
///////////////////////////////////////////////////////////

using Backend.Communication;
using Backend.Models;

namespace Backend.Brains
{
    public class AddProductCB : IAddProduct
    {
        private readonly IClient _client;
        private readonly IProtocol _protocol;
        public  IError Error;


        public AddProductCB(IProtocol protocol, IClient client)
        {
            _protocol = protocol;
            LastError = null;
            _client = client;
            Error = new Error();
        }

        public bool CreateProduct(BackendProduct Product)
        {
            // Create the product
            var product = Product;


            if (product.BName == "" || product.BPrice < 0 || product.BProductNumber == "")
            {
                LastError = "Enter correct product details.";
                Error.StdErr(LastError);
                return false;
            }


            // Generate XML from product
            var cmdtoSend = _protocol.ProductXMLParser(product);

            // Connect to server
            if (!_client.Connect())
            {
                LastError = "Conenction error";
                Error.StdErr(LastError);
                return false;
            }

            // Send the XML data
            if (!_client.Send(cmdtoSend))
            {
                LastError = "Sending error";
                Error.StdErr(LastError);
                return false;
            }

            _client.Disconnect();

            return true;
        }

        public string LastError { private set; get; }
    } //end AddProductCB
}
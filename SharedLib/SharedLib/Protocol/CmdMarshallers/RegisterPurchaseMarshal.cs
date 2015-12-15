using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SharedLib.Models;
using SharedLib.Protocol.Commands;

namespace SharedLib.Protocol.CmdMarshallers
{
    /// <summary>
    /// Marshaller for the RegisterPurchaseCmd, Implements the ICmdMarshal interface.
    /// </summary>
    public class RegisterPurchaseMarshal: ICmdMarshal
    {

        /// <summary>
        /// Casts RegisterPurchaseCmd to the parameter cmd, then creates an XML string with the list of PurchasedProducts.
        /// </summary>
        /// <param name="cmd">Command which is to be parse, in this instance a RegisterPurchaseCmd</param>
        /// <returns>XML string</returns>
        public string Encode(Command cmd)
        {
            // Cast to RegisterPurchaseCmd
            var rpcmd = (RegisterPurchaseCmd)cmd;

            // Create XML
            var sb = new StringBuilder(); // Create stringbuilder to collect xml data
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartElement("Command"); // Root
                writer.WriteAttributeString("Name", rpcmd.CmdName); // "Name" attribute to root
                writer.WriteStartElement("ProductList");// ProductList start

                foreach (var product in rpcmd.Products) // Write product details for each element in list
                {
                    writer.WriteStartElement("PurchasedProduct"); // Product start

                    writer.WriteAttributeString("Name", product.Name); // "Name" attribute for Product
                    writer.WriteAttributeString("ProductNumber", product.ProductNumber);// "ProductNumber" attribute for Product
                    writer.WriteAttributeString("UnitPrice", product.UnitPrice.ToString()); // "UnitPrice" attribute for Product
                    writer.WriteAttributeString("Quantity", product.Quantity.ToString()); // "Quantity" attribute for
                    writer.WriteAttributeString("TotalPrice", product.TotalPrice.ToString()); // "TotalPrice" attribute for Product

                    writer.WriteEndElement(); // PurchasedProduct ended
                }

                writer.WriteEndElement(); // ProductList ended

                writer.WriteEndElement(); // Root end
            }
            // Convert stringbuilder to string and return
            return sb.ToString();
        }

        /// <summary>
        /// Creates a purchase which has a list of PurchasedProducts and reads each XML node named "PurchasedProduct" and its attributes into the list, and then uses the purchase as a parameter to create a new RegisterPurchaseCmd.
        /// </summary>
        /// <param name="data">XML string to be parsed</param>
        /// <returns>CatalogueDetailsCmd object</returns>
        public Command Decode(string data)
        {
            // Create new Purchase
            var purchase = new Purchase();
            purchase.PurchasedProducts = new List<PurchasedProduct>();

            // Create XmlReader to read xml string into product
            using (XmlReader reader = XmlReader.Create(new StringReader(data)))
            {
                while (reader.Read()) // Makes the reader go through the whole string
                {
                    if (reader.Name == "PurchasedProduct") // if a node is named "PurchasedProduct" do the following:
                    {
                        var pproduct = new PurchasedProduct(); // Create new PurchasedProduct

                        pproduct.Name = reader["Name"]; // Inserts the value of the attribute name "Name" into the purchasedProduct object
                        pproduct.ProductNumber = reader["ProductNumber"]; // Inserts the value of the attribute name "ProductNumber" into the purchasedProduct object
                        pproduct.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]); // Inserts the value of the attribute name "UnitPrice" into the purchasedProduct object
                        pproduct.Quantity = Convert.ToInt32(reader["Quantity"]); // Inserts the value of the attribute name "Quantity" into the purchasedProduct object

                        purchase.PurchasedProducts.Add(pproduct); // Add the newly created purchasedProduct to the purchasedProductlist
                    } // end if
                } // end of read
            }

            // return new command with the translated xml product attributes
            return new RegisterPurchaseCmd(purchase);

        }
    }
}

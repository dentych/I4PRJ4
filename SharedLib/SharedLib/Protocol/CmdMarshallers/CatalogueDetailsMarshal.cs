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
    public class CatalogueDetailsMarshal: ICmdMarshal
    {
        public string Encode(Command cmd)
        {
            // Cast to CatalogueDetailsCmd
            var cdcmd = (CatalogueDetailsCmd)cmd;

            // Create XML
            var sb = new StringBuilder(); // Create stringbuilder to collect xml data
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartElement("Command"); // Root
                writer.WriteAttributeString("Name", cdcmd.CmdName); // "Name" attribute to root
                writer.WriteStartElement("ProductList");// ProductList start

                foreach (var product in cdcmd.Products) // Write product details for each element in list
                {
                    writer.WriteStartElement("Product"); // Product start

                    writer.WriteAttributeString("Name", product.Name); // "Name" attribute for Product
                    writer.WriteAttributeString("ProductNumber", product.ProductNumber);// "ProductNumber" attribute for Product
                    writer.WriteAttributeString("Price", product.Price.ToString()); // "Price" attribute for Product
                    writer.WriteAttributeString("ProductId", product.ProductId.ToString() ); // "ProductId" attribute for Product
                    
                    writer.WriteEndElement(); // Product ended
                }

                writer.WriteEndElement(); // ProductList ended

                writer.WriteEndElement(); // Root end
            }
            // Convert stringbuilder to string and return
            return sb.ToString();
        }

        public Command Decode(string data)
        {
            // Create new productList
            var productList = new List<Product>();

            // Create XmlReader to read xml string into product
            using (XmlReader reader = XmlReader.Create(new StringReader(data)))
            {
                while (reader.Read()) // Makes the reader go through the whole string
                {
                    if (reader.Name == "Product") // if a node is named "Product" do the following:
                    {
                        var product = new Product(); // Create new Product

                        product.Name = reader["Name"]; // Inserts the value of the attribute name "Name" into the product object
                        product.ProductNumber = reader["ProductNumber"]; // Inserts the value of the attribute name "ProductNumber" into the product object
                        product.Price = Convert.ToDecimal(reader["Price"]); // Inserts the value of the attribute name "Price" into the product object
                        product.ProductId = Convert.ToInt32(reader["ProductId"]); // Inserts the value of the attribute name "ProductId" into the product object
                        
                        productList.Add(product); // Add the newly created product to the productlist
                    } // end if
                } // end of read
            }

            // return new command with the translated xml product attributes
            return new CatalogueDetailsCmd(productList);
        }
    }
}

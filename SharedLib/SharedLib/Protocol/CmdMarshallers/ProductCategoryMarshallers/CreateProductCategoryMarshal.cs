using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SharedLib.Models;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;

namespace SharedLib.Protocol.CmdMarshallers.ProductCategoryMarshallers
{
    public class CreateProductCategoryMarshal: ICmdMarshal
    {
        public string Encode(Command cmd)
        {
            // Cast to CreateProductCategoryCmd
            var ccmd = (CreateProductCategoryCmd)cmd;

            // Create XML
            var sb = new StringBuilder(); // Create stringbuilder to collect xml data
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartElement("Command"); // Root
                writer.WriteAttributeString("Name", ccmd.CmdName); // "Name" attribute to root
                writer.WriteStartElement("ProductCategory");// ProductCategory start
                writer.WriteAttributeString("Name", ccmd.Name);// "Name" attribute for category

                foreach (var product in ccmd.Products) // Write product details for each element in list
                {
                    writer.WriteStartElement("Product"); // Product start

                    writer.WriteAttributeString("Name", product.Name); // "Name" attribute for Product
                    writer.WriteAttributeString("ProductNumber", product.ProductNumber);// "ProductNumber" attribute for Product
                    writer.WriteAttributeString("Price", product.Price.ToString()); // "Price" attribute for Product
                    writer.WriteAttributeString("ProductId", product.ProductId.ToString()); // "ProductId" attribute for Product

                    writer.WriteEndElement(); // Product ended
                }

                writer.WriteEndElement();// ProductCategory ended
                writer.WriteEndElement(); // Root end
            }
            // Convert stringbuilder to string and return
            return sb.ToString();
        }

        public Command Decode(string data)
        {
            // Create new productList
            var productList = new List<Product>();
            
            // Create string for category name
            string categoryName = null;

            int counter = 0; // Workaround to avoid categoryName get overwrited because reader.name is also true at </ProductCategory> which means categoryName becomes null

            // Create XmlReader to read xml string into product
            using (XmlReader reader = XmlReader.Create(new StringReader(data)))
            {
                while (reader.Read()) // Makes the reader go through the whole string
                {
                    if (reader.Name == "ProductCategory" && counter == 0) // if a node is named "ProductCategory" and it hasnt been visited before do the following:
                    {
                        categoryName = reader["Name"];
                        counter++;
                    } // end if

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
            return new CreateProductCategoryCmd(categoryName,productList);
        }
    }
}

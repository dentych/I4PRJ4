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
    /// Marshaller for the DeleteProductCmd, Implements the ICmdMarshal interface.
    /// </summary>
    public class DeleteProductMarshal: ICmdMarshal
    {
        /// <summary>
        /// Casts DeleteProductCmd to the parameter cmd, then creates an XML string with the Product attributes.
        /// </summary>
        /// <param name="cmd">Command which is to be parsed, in this instance a DeleteProductCmd</param>
        /// <returns>XML string</returns>
        public string Encode(Command cmd)
        {
            // Cast to DeleteProductCmd
            var dcmd = (DeleteProductCmd)cmd;

            // Create XML
            var sb = new StringBuilder(); // Create stringbuilder to collect xml data
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartElement("Command"); // Root
                writer.WriteAttributeString("Name", dcmd.CmdName); // "Name" attribute to root
                writer.WriteStartElement("Product");// Product start

                writer.WriteAttributeString("Name", dcmd.Name); // "Name" attribute for Product
                writer.WriteAttributeString("ProductNumber", dcmd.ProductNumber); // "ProductNumber" attribute for Product
                writer.WriteAttributeString("Price", dcmd.Price.ToString()); // "Price" attribute for Product
                writer.WriteAttributeString("ProductId", dcmd.ProductId.ToString()); // "ProductId" attribute for Product
                writer.WriteAttributeString("ProductCategoryId", dcmd.ProductCategoryId.ToString()); // "ProductId" attribute for Product

                writer.WriteEndElement(); // Product ended
                writer.WriteEndElement(); // Root end
            }
            // Convert stringbuilder to string and return
            return sb.ToString();
        }

        /// <summary>
        /// Creates a new Product and reads the XML node named "Product" and its attributes into the product, then uses the product object to create a new DeleteProductCmd.
        /// </summary>
        /// <param name="data">XML string to be parsed</param>
        /// <returns>DeleteProductCmd object</returns>
        public Command Decode(string data)
        {
            // Create new product
            var product = new Product();

            // Create XmlReader to read xml string into product
            using (XmlReader reader = XmlReader.Create(new StringReader(data)))
            {
                reader.ReadToFollowing("Product"); // Skips forward to the <Product> node
                product.Name = reader["Name"]; // Inserts the attribute name "Name" into the product object
                product.ProductNumber = reader["ProductNumber"]; // Inserts the attribute name "ProductNumber" into the product object
                product.Price = Convert.ToDecimal(reader["Price"]);// Inserts the attribute name "Price" into the product object
                product.ProductId = Convert.ToInt32(reader["ProductId"]);// Inserts the attribute name "ProductId" into the product object
                product.ProductCategoryId = Convert.ToInt32(reader["ProductCategoryId"]);// Inserts the attribute name "ProductId" into the product object
            }

            // return new command with the translated xml product attributes
            return new DeleteProductCmd(product);
        }
    
    }
}

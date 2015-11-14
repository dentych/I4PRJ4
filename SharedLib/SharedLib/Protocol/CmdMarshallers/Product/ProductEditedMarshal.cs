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
    public class ProductEditedMarshal: ICmdMarshal
    {

        public string Encode(Command cmd)
        {
            // Cast to ProductEditedCmd
            var pecmd = (ProductEditedCmd)cmd;

            // Create XML
            var sb = new StringBuilder(); // Create stringbuilder to collect xml data
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartElement("Command"); // Root
                writer.WriteAttributeString("Name", pecmd.CmdName); // "Name" attribute to root
                writer.WriteStartElement("Product");// Product start

                writer.WriteAttributeString("Name", pecmd.Name); // "Name" attribute for Product
                writer.WriteAttributeString("ProductNumber", pecmd.ProductNumber); // "ProductNumber" attribute for Product
                writer.WriteAttributeString("Price", pecmd.Price.ToString()); // "Price" attribute for Product
                writer.WriteAttributeString("ProductId", pecmd.ProductId.ToString()); // "ProductId" attribute for Product
                writer.WriteAttributeString("ProductCategoryId", pecmd.ProductCategoryId.ToString()); // "ProductCategoryId" attribute for Product
                writer.WriteAttributeString("OldProductCategoryId", pecmd.OldProductCategoryId.ToString()); // "ProductCategoryId" attribute for Product

                writer.WriteEndElement(); // Product ended
                writer.WriteEndElement(); // Root end
            }
            // Convert stringbuilder to string and return
            return sb.ToString();
        }

        public Command Decode(string data)
        {
            // Create new product
            var product = new Product();
            int oldProductCategoryId;

            // Create XmlReader to read xml string into product
            using (XmlReader reader = XmlReader.Create(new StringReader(data)))
            {
                reader.ReadToFollowing("Product"); // Skips forward to the <Product> node
                product.Name = reader["Name"]; // Inserts the attribute name "Name" into the product object
                product.ProductNumber = reader["ProductNumber"]; // Inserts the attribute name "ProductNumber" into the product object
                product.Price = Convert.ToDecimal(reader["Price"]);// Inserts the attribute name "Price" into the product object
                product.ProductId = Convert.ToInt32(reader["ProductId"]);
                product.ProductCategoryId = Convert.ToInt32(reader["ProductCategoryId"]);
                oldProductCategoryId = Convert.ToInt32(reader["OldProductCategoryId"]);
            }

            // return new command with the translated xml product attributes
            return new ProductEditedCmd(product, oldProductCategoryId);
        }
    }
}

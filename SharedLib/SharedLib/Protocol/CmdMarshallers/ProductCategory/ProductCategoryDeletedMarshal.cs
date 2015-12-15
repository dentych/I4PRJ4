using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SharedLib.Models;
using SharedLib.Protocol.Commands.ProductCategoryCommands;

namespace SharedLib.Protocol.CmdMarshallers
{
    /// <summary>
    /// Marshaller for the ProductCategoryDeletedCmd, Implements the ICmdMarshal interface.
    /// </summary>
    public class ProductCategoryDeletedMarshal: ICmdMarshal
    {
        /// <summary>
        /// Casts ProductCategoryDeletedCmd to the parameter cmd, then creates an XML string with the ProductCategory attributes and inserts every "Product" node into the list of products in ProductCategory.
        /// </summary>
        /// <param name="cmd">Command which is to be parsed, in this instance a ProductCategoryDeletedCmd</param>
        /// <returns>XML string</returns>
        public string Encode(Command cmd)
        {
            // Cast to ProductCategoryDeletedCmd
            var ccmd = (ProductCategoryDeletedCmd)cmd;

            // Create XML
            var sb = new StringBuilder(); // Create stringbuilder to collect xml data
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartElement("Command"); // Root
                writer.WriteAttributeString("Name", ccmd.CmdName); // "Name" attribute to root

                writer.WriteStartElement("ProductCategory");// ProductCategory start
                writer.WriteAttributeString("Name", ccmd.Name);// "Name" attribute for category
                writer.WriteAttributeString("ProductCategoryId", ccmd.ProductCategoryId.ToString());// "ProductCategorId" attribute for category

                writer.WriteEndElement();// ProductCategory ended
                writer.WriteEndElement(); // Root end
            }
            // Convert stringbuilder to string and return
            return sb.ToString();
        }

        /// <summary>
        /// Creates a ProductCategory object and reads the attributes from the XML string into it. Then the ProductCategory is used as a parameter to create a new ProductCategoryDeletedCmd.
        /// </summary>
        /// <param name="data">XML string to be parsed</param>
        /// <returns>ProductCategoryDeletedCmd object</returns>
        public Command Decode(string data)
        {
            // Create string for category name
            var productCategory = new ProductCategory();

            // Create XmlReader to read xml string into product
            using (XmlReader reader = XmlReader.Create(new StringReader(data)))
            {
                reader.ReadToFollowing("ProductCategory"); // Skips forward to the <Product> node
                productCategory.Name = reader["Name"]; // Inserts the attribute name "Name" into the product object
                productCategory.ProductCategoryId = Convert.ToInt32(reader["ProductCategoryId"]);

            }
            // return new command with the translated xml product attributes
            return new ProductCategoryDeletedCmd(productCategory);
        }
    }
}

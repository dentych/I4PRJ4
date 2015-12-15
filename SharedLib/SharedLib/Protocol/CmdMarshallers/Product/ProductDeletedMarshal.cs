﻿using System;
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
    /// Marshaller for the ProductDeletedCmd, Implements the ICmdMarshal interface.
    /// </summary>
    public class ProductDeletedMarshal: ICmdMarshal
    {
        /// <summary>
        /// Casts ProductDeletedCmd to the parameter cmd, then creates an XML string with the Product attributes.
        /// </summary>
        /// <param name="cmd">Command which is to be parsed, in this instance a ProductDeletedCmd</param>
        /// <returns>XML string</returns>
        public string Encode(Command cmd)
        {
            // Cast to ProductDeletedCmd
            var pdcmd = (ProductDeletedCmd)cmd;

            // Create XML
            var sb = new StringBuilder(); // Create stringbuilder to collect xml data
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartElement("Command"); // Root
                writer.WriteAttributeString("Name", pdcmd.CmdName); // "Name" attribute to root
                writer.WriteStartElement("Product");// Product start

                writer.WriteAttributeString("Name", pdcmd.Name); // "Name" attribute for Product
                writer.WriteAttributeString("ProductNumber", pdcmd.ProductNumber); // "ProductNumber" attribute for Product
                writer.WriteAttributeString("Price", pdcmd.Price.ToString()); // "Price" attribute for Product
                writer.WriteAttributeString("ProductId", pdcmd.ProductId.ToString()); // "ProductId" attribute for Product
                writer.WriteAttributeString("ProductCategoryId", pdcmd.ProductCategoryId.ToString()); // "ProductId" attribute for Product

                writer.WriteEndElement(); // Product ended
                writer.WriteEndElement(); // Root end
            }
            // Convert stringbuilder to string and return
            return sb.ToString();
        }

        /// <summary>
        /// Creates a new Product and reads the XML node named "Product" and its attributes into the product, then uses the product object to create a new ProductDeletedCmd.
        /// </summary>
        /// <param name="data">XML string to be parsed</param>
        /// <returns>ProductDeletedCmd object</returns>
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
                product.ProductId = Convert.ToInt32(reader["ProductId"]);
                product.ProductCategoryId = Convert.ToInt32(reader["ProductCategoryId"]);
            }

            // return new command with the translated xml product attributes
            return new ProductDeletedCmd(product);
        }
    }
}

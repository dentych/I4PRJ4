using Backend.Models.Datamodels;
using SharedLib.Models;

namespace Backend.Models.Brains
{
    /// <summary>
    /// Interface for the SharedLib protocol, since it was missing at the time of creating this.
    /// </summary>
    public interface IProtocol
    {
        /// <summary>
        /// Generates the XML for creating a new product.
        /// </summary>
        /// <param name="toParse">The product to generate the XML from.</param>
        /// <returns>Generated XML string.</returns>
        string ProductXMLParser(Product toParse);
        /// <summary>
        /// Generates the XML for creating a new category.
        /// </summary>
        /// <param name="toParse">The category to generate the XML from.</param>
        /// <returns>Generated XML string.</returns>
        string CategoryXMLParser(BackendProductCategory toParse);
        /// <summary>
        /// Generates the XML for editing a product.
        /// </summary>
        /// <param name="toParse">Product that the XML is generated from.</param>
        /// <returns>Generated XML string.</returns>
        string EditProductXMLParser(BackendProduct toParse);
        /// <summary>
        /// Generates the XML for editing a category.
        /// </summary>
        /// <param name="toParse">Category to generate XML from.</param>
        /// <returns>Generated XML string.</returns>
        string EditCategoryXMLParser(BackendProductCategory toParse);
        /// <summary>
        /// Generates the XML for deleting a category.
        /// </summary>
        /// <param name="toParse">Category to be deleted.</param>
        /// <returns>Generated XML string.</returns>
        string DeleteCategoryXMLParser(BackendProductCategory toParse);
        /// <summary>
        /// Generates the XML for deleting a product.
        /// </summary>
        /// <param name="toParse">Product to be deleted.</param>
        /// <returns>Generated XML string.</returns>
        string DeleteProductXMLParser(Product toParse);
        /// <summary>
        /// Generates the XML for getting the entire product catalogue.
        /// </summary>
        /// <returns>Generated XML string.</returns>
        string GetCatalougXMLParser();
    } //end IProtocol
}
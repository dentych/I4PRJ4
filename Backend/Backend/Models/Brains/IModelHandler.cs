using Backend.Models.Datamodels;
using SharedLib.Models;

namespace Backend.Models.Brains
{
    /// <summary>
    /// Model handler interface.
    /// </summary>
    public interface IModelHandler
    {
        /// <summary>
        /// The last error that has been generated.
        /// </summary>
        string LastError { get; }

        #region Products
        /// <summary>
        /// Send a create new product command to CentralServer.
        /// </summary>
        /// <param name="product">Product to create</param>
        /// <returns>True if command is sent, otherwise false.</returns>
        bool CreateProduct(BackendProduct product);
        /// <summary>
        /// Send an edit product command to CentralServer.
        /// </summary>
        /// <param name="product">Product with the updated information.</param>
        /// <returns>True if command is sent, otherwise false.</returns>
        bool EditProduct(BackendProduct product);
        /// <summary>
        /// Send a delete product command to CentralServer.
        /// </summary>
        /// <param name="product">Product to be deleted.</param>
        /// <returns>True if command is sent, otherwise false.</returns>
        bool DeleteProduct(Product product);
        #endregion

        #region Categories
        /// <summary>
        /// Send an edit category command to CentralServer.
        /// </summary>
        /// <param name="category">Category with new information.</param>
        /// <returns>True if command is sent, otherwise false.</returns>
        bool EditCategory(BackendProductCategory category);
        /// <summary>
        /// Send a create category command to CentralServer.
        /// </summary>
        /// <param name="category">Category to create.</param>
        /// <returns>True if command is sent, otherwise false.</returns>
        bool AddCategory(BackendProductCategory category);
        /// <summary>
        /// Send a delete category command to CentralServer.
        /// </summary>
        /// <param name="category">Category to be deleted.</param>
        /// <returns>True if command is sent, otherwise false.</returns>
        bool DeleteCategory(BackendProductCategory category);
        /// <summary>
        /// Send multiple edit product commands to CentralServer. One for each product in categoryToEmpty.
        /// </summary>
        /// <param name="categoryToEmpty">The category to empty for products.</param>
        /// <param name="catId">The category ID the products should be moved to.</param>
        /// <returns>True if commands are sent, otherwise false.</returns>
        bool MoveProductsInCategory(BackendProductCategory categoryToEmpty, int catId);
        #endregion

        /// <summary>
        /// Send a get catalogue command to CentralServer.
        /// </summary>
        /// <returns>True if command is sent, otherwise false.</returns>
        bool CatalogueDetails();

    } //end IAddProduct
}
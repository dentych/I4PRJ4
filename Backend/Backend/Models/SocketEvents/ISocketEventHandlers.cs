using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;

namespace Backend.Models.SocketEvents
{
    /// <summary>
    /// Interface for socket event handlers.
    /// </summary>
    public interface ISocketEventHandlers
    {
        /// <summary>
        /// Handles a new product creation.
        /// </summary>
        /// <param name="product">Information about the product that has been created.</param>
        void ProductCreatedHandler(ProductCreatedCmd product);
        /// <summary>
        /// Handles deletion of an existing product.
        /// </summary>
        /// <param name="product">Information about the deleted product.</param>
        void ProductDeletedHandler(ProductDeletedCmd product);
        /// <summary>
        /// Handles editing an existing product.
        /// </summary>
        /// <param name="product">Information about the edited product.</param>
        void ProductEditedHandler(ProductEditedCmd product);
        /// <summary>
        /// Handles a catalogue command, which contains information about all categories
        /// and their products.
        /// </summary>
        /// <param name="cmd">Catalogue details</param>
        void CatalogueDetailsHandler(CatalogueDetailsCmd cmd);
        /// <summary>
        /// Handles creation of a new category.
        /// </summary>
        /// <param name="category">Information about the created category.</param>
        void ProductCategoryCreatedHandler(ProductCategoryCreatedCmd category);
        /// <summary>
        /// Handles deletion of an existing category.
        /// </summary>
        /// <param name="category">Information about the deleted category.</param>
        void ProductCategoryDeletedHandler(ProductCategoryDeletedCmd category);
        /// <summary>
        /// Handles editing an existing category.
        /// </summary>
        /// <param name="product">Information about the edited category.</param>
        void ProductCategoryEditedHandler(ProductCategoryEditedCmd product);

        /// <summary>
        /// Subscribe to the product created command.
        /// </summary>
        void SubscribeProductCreated();
        /// <summary>
        /// Subscribe to the product deleted command.
        /// </summary>
        void SubscribeProductDeleted();
        /// <summary>
        /// Subscribe to the product edited command.
        /// </summary>
        void SubscribeProductEdited();
        /// <summary>
        /// Subscribe to the catalogue details command.
        /// </summary>
        void SubscribeCatalogueDetails();
        /// <summary>
        /// Subscribe to the category created command.
        /// </summary>
        void SubscribeProductCategoryCreated();
        /// <summary>
        /// Subscribe to the category deleted command.
        /// </summary>
        void SubscribeProductCategoryDeleted();
        /// <summary>
        /// Subscribe to the category edited command.
        /// </summary>
        void SubscribeProductCategoryEdited();
    }
}

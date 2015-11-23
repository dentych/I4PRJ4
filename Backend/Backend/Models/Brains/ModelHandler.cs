using System.Linq;
using Backend.Models.Communication;
using Backend.Models.Datamodels;
using Backend.Models.SocketEvents;
using SharedLib.Models;

namespace Backend.Models.Brains
{
    /// <summary>
    /// Handlers for all the actions performed in the GUI. asd asdasd asd a dqrlk jlkjawkeljf 
    /// </summary>
    /// The ModelHandler will generate an XML string, using IProtocol and then send the data
    /// to central server using IClient.
    public class ModelHandler : IModelHandler
    {
        private readonly IClient _client;
        private readonly IProtocol _protocol;
        public IError Error;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="protocol">The protocol to use.</param>
        /// <param name="client">The client to use.</param>
        public ModelHandler(IProtocol protocol, IClient client)
        {
            _protocol = protocol;
            LastError = null;
            _client = client;
            Error = new Error();
        }

        /// <summary>
        /// Send a create new product command to CentralServer.
        /// </summary>
        /// <param name="product">Product to create</param>
        /// <returns>True if command is sent, otherwise false.</returns>
        public bool CreateProduct(BackendProduct product)
        {
            if (string.IsNullOrWhiteSpace(product.BName) || product.BPrice < 0 ||
                string.IsNullOrWhiteSpace(product.BProductNumber))
            {
                LastError = "Enter correct product details.";
                Error.StdErr(LastError);
                return false;
            }

            // Generate XML from product
            var cmdtoSend = _protocol.ProductXMLParser(product);
            _client.Send(cmdtoSend);

            return true;
        }

        /// <summary>
        /// Send an edit product command to CentralServer.
        /// </summary>
        /// <param name="product">Product with the updated information.</param>
        /// <returns>True if command is sent, otherwise false.</returns>
        public bool EditProduct(BackendProduct product)
        {
            // Generate XML from Category
            var cmdtoSend = _protocol.EditProductXMLParser(product);
            _client.Send(cmdtoSend);
            return true;
        }

        /// <summary>
        /// Send a delete product command to CentralServer.
        /// </summary>
        /// <param name="product">Product to be deleted.</param>
        /// <returns>True if command is sent, otherwise false.</returns>
        public bool DeleteProduct(Product product)
        {
            _client.Send(_protocol.DeleteProductXMLParser(product));
            return true;
        }

        /// <summary>
        /// Send an edit category command to CentralServer.
        /// </summary>
        /// <param name="category">Category with new information.</param>
        /// <returns>True if command is sent, otherwise false.</returns>
        public bool EditCategory(BackendProductCategory category)
        {
            // Generate XML from Category
            var cmdtoSend = _protocol.EditCategoryXMLParser(category);
            _client.Send(cmdtoSend);
            return true;
        }

        /// <summary>
        /// Send a create category command to CentralServer.
        /// </summary>
        /// <param name="category">Category to create.</param>
        /// <returns>True if command is sent, otherwise false.</returns>
        public bool AddCategory(BackendProductCategory category)
        {
            // Generate XML from Category
            var cmdtoSend = _protocol.CategoryXMLParser(category);
            _client.Send(cmdtoSend);
            return true;
        }

        /// <summary>
        /// Send a delete category command to CentralServer.
        /// </summary>
        /// <param name="category">Category to be deleted.</param>
        /// <returns>True if command is sent, otherwise false.</returns>
        public bool DeleteCategory(BackendProductCategory category)
        {
            var cmdtosend = _protocol.DeleteCategoryXMLParser(category);
            _client.Send(cmdtosend);
            return true;
        }

        /// <summary>
        /// Send multiple edit product commands to CentralServer. One for each product in categoryToEmpty.
        /// </summary>
        /// <param name="categoryToEmpty">The category to empty for products.</param>
        /// <param name="catId">The category ID the products should be moved to.</param>
        /// <returns>True if commands are sent, otherwise false.</returns>
        public bool MoveProductsInCategory(BackendProductCategory categoryToEmpty, int catId)
        {
            if (categoryToEmpty.ProductCategoryId == catId)
            {
                return false;
            }


            // Thread shit, ellers crasher lortet hvis der går for lang tid mellem serversvar
            var clonedata = categoryToEmpty.Products.ToList();


            SocketEventHandlers.InitializeTransfer(clonedata.Count);
            foreach (var product in clonedata)
            {
                var newProduct = new BackendProduct
                {
                    Name = product.Name,
                    Price = product.Price,
                    ProductCategoryId = catId,
                    ProductId = product.ProductId,
                    ProductNumber = product.ProductNumber
                };
                var cmdtosend = _protocol.EditProductXMLParser(newProduct);
                _client.Send(cmdtosend);
            }


            return true;
        }

        /// <summary>
        /// Send a get catalogue command to CentralServer.
        /// </summary>
        /// <returns>True if command is sent, otherwise false.</returns>
        public bool CatalogueDetails()
        {
            var cmdtosend = _protocol.GetCatalougXMLParser();
            _client.Send(cmdtosend);
            return true;
        }

        /// <summary>
        /// The last error that has been generated.
        /// </summary>
        public string LastError { private set; get; }
    }
}
using System;
using System.Linq;
using CentralServer.Messaging;
using CentralServer.Logging;
using CentralServer.Sessions;
using CentralServer.Messaging.Messages;
using CentralServer.Database;
using CentralServer.RequisitionReceipt;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol;
using SharedLib.Models;
using SharedLib.Protocol.Commands.ProductCategoryCommands;

namespace CentralServer.Handlers
{
    /// <summary>
    /// Contains eventhandlers to handle commands received by clients.
    /// </summary>
    public class MainCommandHandler : ICommandHandler
    {
        private readonly ILog _log;
        private readonly ISessionControl _sessions;
        private readonly IRequisitionReceipt _receipt;


        public MainCommandHandler(ILog log, ISessionControl sessions, IRequisitionReceipt receipt)
        {
            _log = log;
            _sessions = sessions;
            _receipt = receipt;
        }

        /// <summary>
        /// A client requests to recieve the entire product catalogue. 
        /// Respond with a CatalogueDetails command.
        /// </summary>
        /// <param name="client">The client who send the command</param>
        /// <param name="cmd">The command</param>
        public void HandleGetCatalogue(IMessageReceiver client, GetCatalogueCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client recieving catalogue details");

            var responseCmd = new CatalogueDetailsCmd();

            // Retrieve products from database

            using (var db = new DatabaseContext())
            {
                var query = from pc in db.ProductCategories select pc;

                foreach (var cat in query.ToList())
                {
                    var copy = new ProductCategory() {
                        ProductCategoryId = cat.ProductCategoryId,
                        Name = cat.Name
                    };

                    var prdQuery = from p in db.Products
                                   where p.ProductCategoryId == cat.ProductCategoryId
                                   select p;

                    foreach (var product in prdQuery)
                        copy.Products.Add(product);

                    responseCmd.ProductCategories.Add(copy);
                }
            }

            // Send response command
            var response = new SendCommandMsg(responseCmd);
            client.Send(ClientControl.E_SEND_COMMAND, response);
        }

        /// <summary>
        /// A client requests to create a new product. 
        /// Create the product in database and broadcast a ProductCreatedCmd command.
        /// </summary>
        /// <param name="client">The client who send the command</param>
        /// <param name="cmd">The command</param>
        public void HandleCreateProduct(IMessageReceiver client, CreateProductCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client creating a new product");


            var product = new Product()
            {
                Name = cmd.Name,
                ProductNumber = cmd.ProductNumber,
                Price = cmd.Price,
                ProductCategoryId = cmd.ProductCategoryId,
            };

            using (var ctx = new DatabaseContext())
            {
                ctx.Products.Add(product);
                ctx.SaveChanges();
            }

            // Broadcast changes to all connected clients
            Broadcast(new ProductCreatedCmd(product));
        }

        /// <summary>
        /// A client requests to edit product details. 
        /// Save the details and broadcast a ProductEditedCmd command.
        /// </summary>
        /// <param name="client">The client who send the command</param>
        /// <param name="cmd">The command</param>
        public void HandleEditProduct(IMessageReceiver client, EditProductCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client modifying an existing product");

            using (var ctx = new DatabaseContext())
            {
                var product = ctx.Products.Find(cmd.ProductId);

                if (product != null)
                {
                    var oldProductCategoryId = product.ProductCategoryId;

                    product.Name = cmd.Name;
                    product.ProductNumber = cmd.ProductNumber;
                    product.Price = cmd.Price;
                    product.ProductCategoryId = cmd.ProductCategoryId;

                    ctx.SaveChanges();

                    Broadcast(new ProductEditedCmd(product, oldProductCategoryId));
                }
            }
        }

        /// <summary>
        /// A client requests to delete a product. 
        /// Delete the product and broadcast a ProductDeletedCmd command.
        /// </summary>
        /// <param name="client">The client who send the command</param>
        /// <param name="cmd">The command</param>
        public void HandleDeleteProduct(IMessageReceiver client, DeleteProductCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client modifying an existing product");

            using (var ctx = new DatabaseContext())
            {
                var product = ctx.Products.Find(cmd.ProductId);

                if (product != null)
                {
                    ctx.Products.Remove(product);
                    ctx.SaveChanges();

                    Broadcast(new ProductDeletedCmd(product));
                }
            }
        }

        /// <summary>
        /// A client requests to create a new product category. 
        /// Create the product category in database and broadcast a ProductCategoryCreatedCmd command.
        /// </summary>
        /// <param name="client">The client who send the command</param>
        /// <param name="cmd">The command</param>
        public void HandleCreateProductCategory(IMessageReceiver client, CreateProductCategoryCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client creating a new productCategory");

            // Create product
            var cat = new ProductCategory() { Name = cmd.Name };

            // Write to database
            using (var db = new DatabaseContext())
            {
                db.ProductCategories.Add(cat);
                db.SaveChanges();
            }

            // Broadcast changes to all connected clients
            Broadcast(new ProductCategoryCreatedCmd(cat));
        }

        /// <summary>
        /// A client requests to edit product category details. 
        /// Save the details and broadcast a ProductCategoryEditedCmd command.
        /// </summary>
        /// <param name="client">The client who send the command</param>
        /// <param name="cmd">The command</param>
        public void HandleEditProductCategory(IMessageReceiver client, EditProductCategoryCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client modifying an existing productcategory");

            using (var db = new DatabaseContext())
            {
                var cat = db.ProductCategories.Find(cmd.ProductCategoryId);

                if (cat != null)
                {
                    cat.Name = cmd.Name;
                    db.SaveChanges();

                    Broadcast(new ProductCategoryEditedCmd(cat));
                }
            }
        }

        /// <summary>
        /// A client requests to delete a product category. 
        /// Delete the product and broadcast a ProductCategoryDeletedCmd command.
        /// </summary>
        /// <param name="client">The client who send the command</param>
        /// <param name="cmd">The command</param>
        public void HandleDeleteProductCategory(IMessageReceiver client, DeleteProductCategoryCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client deleting a productcategory");

            using (var db = new DatabaseContext())
            {
                var cat = db.ProductCategories.Find(cmd.ProductCategoryId);

                if (cat != null)
                {
                    db.ProductCategories.Remove(cat);
                    db.SaveChanges();

                    Broadcast(new ProductCategoryDeletedCmd(cat));
                }
            }
        }

        /// <summary>
        /// A client registers a new purchase. 
        /// </summary>
        /// <param name="client">The client who send the command</param>
        /// <param name="cmd">The command</param>
        public void HandleRegisterPurchase(IMessageReceiver client, RegisterPurchaseCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client registering a new purchase");

            using (var ctx = new DatabaseContext())
            {
                var purchase = new Purchase();

                foreach (var purchasedProduct in cmd.Products)
                    purchase.PurchasedProducts.Add(purchasedProduct);

                ctx.Purchases.Add(purchase);
                ctx.SaveChanges();

                _receipt.Write(purchase);
            }
        }

        /// <summary>
        /// Broadcast a command to all connected clients
        /// </summary>
        /// <param name="cmd">The command to broadcast</param>
        private void Broadcast(Command cmd)
        {
            foreach (var c in _sessions.GetClients())
            {
                var msg = new SendCommandMsg(cmd);
                c.Send(ClientControl.E_SEND_COMMAND, msg);
            }
        }
    }
}

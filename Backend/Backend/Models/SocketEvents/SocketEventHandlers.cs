using Backend.Models.Communication;
using Backend.Models.Datamodels;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;

namespace Backend.Models.SocketEvents
{
    /// <summary>
    /// Event handlers for all the socket events.
    /// </summary>
    public class SocketEventHandlers : ISocketEventHandlers
    {
        #region Properties and variables
        private readonly BackendProductCategoryList _categories;
        private static int _pToTranfer;
        private int _pTranfered;
        #endregion

        #region Constructor
        /// <summary>
        /// Sets a reference to a category list.
        /// </summary>
        /// <param name="cat">Category list</param>
        public SocketEventHandlers(BackendProductCategoryList cat)
        {
            _categories = cat;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Sets how many events are expected before updating the current product list in
        /// the MainWindow. Used in ProductEditedHandler.
        /// </summary>
        /// <param name="c">Amount of edits expected.</param>
        public static void InitializeTransfer(int c)
        {
            _pToTranfer = c;
        }
        #endregion

        #region Event handlers
        public void ProductCreatedHandler(ProductCreatedCmd cmd)
        {
            _categories.GetListByCategory(cmd.ProductCategoryId).AddProduct(cmd.GetProduct());
            _categories.UpdateCurrentProducts();
        }

        public void ProductDeletedHandler(ProductDeletedCmd cmd)
        {
            var category = _categories.GetListByCategory(cmd.ProductCategoryId);

            for (var i = 0; i < category.Products.Count; i++)
            {
                if (category.Products[i].ProductId == cmd.ProductId)
                {
                    category.RemoveProductAt(i);
                    _categories.UpdateCurrentProducts();
                    break;
                }
            }
        }

        public void ProductEditedHandler(ProductEditedCmd cmd)
        {
            var category = _categories.GetListByCategory(cmd.OldProductCategoryId);

            for (var i = 0; i < category.Products.Count; i++)
            {
                var product = category.Products[i];

                if (product.ProductId != cmd.ProductId) continue;

                product.Name = cmd.Name;
                product.Price = cmd.Price;
                product.ProductNumber = cmd.ProductNumber;

                if (product.ProductCategoryId != cmd.ProductCategoryId)
                {
                    product.ProductCategoryId = cmd.ProductCategoryId;
                    category.Products.Remove(product);
                    _categories.GetListByCategory(cmd.ProductCategoryId).Products.Add(product);
                    i--;
                }
            }
            _pTranfered++;

            if (_pTranfered >= _pToTranfer)
            {
                _categories.UpdateCurrentProducts();
                _pTranfered = 0;
                _pToTranfer = 0;
            }
        }

        public void CatalogueDetailsHandler(CatalogueDetailsCmd cmd)
        {
            if (cmd.ProductCategories.Count > 0)
            {
                foreach (var category in cmd.ProductCategories)
                {
                    var Category = new BackendProductCategory
                    {
                        BName = category.Name,
                        ProductCategoryId = category.ProductCategoryId,
                        Products = category.Products
                    };
                    _categories.Add(Category);
                }
                _categories.Bootstrapper();
            }
            else
            {
                _categories.CurrentProductList = null;
            }
        }

        public void ProductCategoryCreatedHandler(ProductCategoryCreatedCmd category)
        {
            var cat = new BackendProductCategory
            {
                BName = category.Name,
                ProductCategoryId = category.ProductCategoryId,
                Products = category.Products
            };

            _categories.Add(cat);
        }

        public void ProductCategoryDeletedHandler(ProductCategoryDeletedCmd category)
        {
            for (var i = 0; i < _categories.Count; i++)
            {
                if (_categories[i].ProductCategoryId == category.ProductCategoryId)
                {
                    _categories.RemoveAt(i);
                    break;
                }
            }
        }

        public void ProductCategoryEditedHandler(ProductCategoryEditedCmd category)
        {
            for (var i = 0; i < _categories.Count; i++)
            {
                if (_categories[i].ProductCategoryId == category.ProductCategoryId)
                {
                    _categories[i].BName = category.Name;
                }
            }
        }
        #endregion

        #region Subscribe methods
        public void SubscribeProductCreated()
        {
            LSC.Listener.OnProductCreated += ProductCreatedHandler;
        }

        public void SubscribeProductDeleted()
        {
            LSC.Listener.OnProductDeleted += ProductDeletedHandler;
        }

        public void SubscribeProductEdited()
        {
            LSC.Listener.OnProductEdited += ProductEditedHandler;
        }

        public void SubscribeCatalogueDetails()
        {
            LSC.Listener.OnCatalogueDetails += CatalogueDetailsHandler;
        }

        public void SubscribeProductCategoryCreated()
        {
            LSC.Listener.OnProductCategoryCreated += ProductCategoryCreatedHandler;
        }

        public void SubscribeProductCategoryDeleted()
        {
            LSC.Listener.OnProductCategoryDeleted += ProductCategoryDeletedHandler;
        }

        public void SubscribeProductCategoryEdited()
        {
            LSC.Listener.OnProductCategoryEdited += ProductCategoryEditedHandler;
        }
        #endregion
    }
}
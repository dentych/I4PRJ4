using System;
using Backend.Communication;
using SharedLib.Models;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;
using System.Collections.Generic;

namespace Backend.Models.SocketEvents
{
    public class SocketEventHandlers : ISocketEventHandlers
    {
        #region Properties and variables
        private readonly BackendProductCategoryList _categories;
        #endregion

        #region Constructor
        public SocketEventHandlers(BackendProductCategoryList cat)
        {
            _categories = cat;
        }
        #endregion

        #region Event handlers



        public void ProductCreatedHandler(ProductCreatedCmd cmd)
        {
            _categories.GetListByCateogry(cmd.ProductId).AddProduct(cmd.GetProduct());
            _categories.UpdateCurrentProducts();
        }

        public void ProductDeletedHandler(ProductDeletedCmd cmd)
        {
            BackendProductCategory category = _categories.GetListByCateogry(0);

            for (int i = 0; i < category.Products.Count; i++)
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
            BackendProductCategory category = _categories.GetListByCateogry(0);

            foreach (var product in category.Products)
            {
                if (product.ProductId == cmd.ProductId)
                {
                    product.Name = cmd.Name;
                    product.Price = cmd.Price;
                    product.ProductCategoryId = cmd.ProductId;
                    product.ProductNumber = cmd.ProductNumber;
                    _categories.UpdateCurrentProducts();
                    break;
                }
            }
        }

        public void CatalogueDetailsHandler(CatalogueDetailsCmd cmd)
        {
            foreach (var category in cmd.ProductCategories) // NO IT IS PRODUCTCATEGORY)
            {
                BackendProductCategory Category = new BackendProductCategory()
                {
                    BName = category.Name,
                    ProductCategoryId = category.ProductCategoryId,
                    Products = category.Products
                };
                _categories.Add(Category);
            }
        }

        public void ProductCategoryCreatedHandler(ProductCategoryCreatedCmd category)
        {
            var cat = new BackendProductCategory()
            {
                BName = category.Name,
                ProductCategoryId = category.ProductCategoryId,
                Products = category.Products
            };

            _categories.Add(cat);
        }

        public void ProductCategoryDeletedHandler(ProductCategoryDeletedCmd category)
        {
            for (int i = 0; i < _categories.Count; i++)
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
            for (int i = 0; i < _categories.Count; i++)
            {
                if (_categories[i].ProductCategoryId == category.ProductCategoryId)
                {
                    _categories[i].BName = category.Name;
                   // _categories[i].Products = category.Products; // This shouldn't be nødvendigt nissemand :-D
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

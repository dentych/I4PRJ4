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

        #region DataReaders
        public void ProductCreatedHandler(ProductCreatedCmd product)
        {
            // TODO: Insert relevant ID from product, when implemented in SharedLib.
            //_categories.GetListByCateogry()
            //_categories.GetListByCateogry(1)
        }

        public void ProductDeletedHandler(ProductDeletedCmd product)
        {
            BackendProductCategory category = _categories.GetListByCateogry(1);

            for (int i = 0; i < category.Products.Count; i++)
            {
                if (category.Products[i].ProductId == product.ProductId)
                {
                    category.RemoveProductAt(i);
                    break;
                }
            }
        }

        public void ProductEditedHandler(ProductEditedCmd product)
        {
            throw new NotImplementedException();
        }

        public void CatalogueDetailsHandler(CatalogueDetailsCmd cmd)
        {
            foreach (var category in cmd.Products) // NO IT IS PRODUCTCATEGORY)
            {
                //  this.Categories.Add(category);
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
                   // _categories[i].Products = category.Products; 
                }
            }
        }

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

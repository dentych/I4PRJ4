using System;
using Backend.Communication;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;
using SharedLib.Models;
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
            throw new NotImplementedException();
        }

        public void ProductCategoryDeletedHandler(ProductCategoryDeletedCmd category)
        {
            throw new NotImplementedException();
        }

        public void ProductCategoryEditedHandler(ProductCategoryEditedCmd product)
        {
            throw new NotImplementedException();
        }

        public void SubscribeProductCreated()
        {
            LSC.Listener.OnProductCreated += ProductCreatedHandler;
        }

        public void SubscribeProductDeleted()
        {
            throw new NotImplementedException();
        }

        public void SubscribeProductEdited()
        {
            throw new NotImplementedException();
        }

        public void SubscribeCatalogueDetails()
        {
            LSC.Listener.OnCatalogueDetails += CatalogueDetailsHandler;
        }

        public void SubscribeProductCategoryCreated()
        {
            throw new NotImplementedException();
        }

        public void SubscribeProductCategoryDeleted()
        {
            throw new NotImplementedException();
        }

        public void SubscribeProductCategoryEdited()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
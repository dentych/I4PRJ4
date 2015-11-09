using System;
using Backend.Communication;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;

namespace Backend.Models.SocketEvents
{
    public class SocketEventHandlers : ISocketEventHandlers
    {
        #region DataReaders

        public SocketEventHandlers(BackendProductCategoryList cat)
        {
            this._categories = cat;

        }

        private readonly BackendProductCategoryList _categories;

        public void ProductCreatedHandler(ProductCreatedCmd product)
        {
            this._categories.GetListByCateogry("//product.Category").Add(product.GetProduct());
        }

        public void ProductDeletedHandler(ProductDeletedCmd product)
        {
            throw new NotImplementedException();
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
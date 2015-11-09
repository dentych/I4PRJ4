using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Communication;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;

namespace Backend.Models.SocketEvents
{
    public interface ISocketEventHandlers
    {


        void ProductCreatedHandler(ProductCreatedCmd product);
        void ProductDeletedHandler(ProductDeletedCmd product);
        void ProductEditedHandler(ProductEditedCmd product);

        void CatalogueDetailsHandler(CatalogueDetailsCmd cmd);

        void ProductCategoryCreatedHandler(ProductCategoryCreatedCmd category);
        void ProductCategoryDeletedHandler(ProductCategoryDeletedCmd category);
        void ProductCategoryEditedHandler(ProductCategoryEditedCmd product);

        void SubscribeProductCreated();
        void SubscribeProductDeleted();
        void SubscribeProductEdited();

        void SubscribeCatalogueDetails();

        void SubscribeProductCategoryCreated();
        void SubscribeProductCategoryDeleted();
        void SubscribeProductCategoryEdited();



    }

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

        public void CatalogueDetailsHandler(CatalogueDetailsCmd cmd)
        {
            foreach (var category in cmd.Products) // NO IT IS PRODUCTCATEGORY)
            {
                //  this.Categories.Add(category);
            }
        }

        public void SubscribeProductCreated()
        {
            LSC.Listener.OnProductCreated += ProductCreatedHandler;
        }

        public void SubscribeCatalogueDetails()
        {
            LSC.Listener.OnCatalogueDetails += CatalogueDetailsHandler;
        }

        #endregion
    }
}

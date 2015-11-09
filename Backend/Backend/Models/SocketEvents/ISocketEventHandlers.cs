using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}

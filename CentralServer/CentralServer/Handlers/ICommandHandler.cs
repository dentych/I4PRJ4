using CentralServer.Messaging;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;
using System;

namespace CentralServer.Handlers
{
    public interface ICommandHandler
    {
        void HandleGetCatalogue(IMessageReceiver client, GetCatalogueCmd cmd);

        void HandleCreateProduct(IMessageReceiver client, CreateProductCmd cmd);
        void HandleEditProduct(IMessageReceiver client, EditProductCmd cmd);
        void HandleDeleteProduct(IMessageReceiver client, DeleteProductCmd cmd);

        void HandleCreateProductCategory(IMessageReceiver client, CreateProductCategoryCmd cmd);
        void HandleEditProductCategory(IMessageReceiver client, EditProductCategoryCmd cmd);
        void HandleDeleteProductCategory(IMessageReceiver client, DeleteProductCategoryCmd cmd);

        void HandleRegisterPurchase(IMessageReceiver client, RegisterPurchaseCmd cmd);
    }
}

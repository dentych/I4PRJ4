using Backend.Models.Datamodels;
using SharedLib.Models;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;

namespace Backend.Models.Brains
{
    /// <summary>
    /// Generates XML strings to be sent to Central Server. The generated strings will contain an encoded command, appropriate for the action the user wish to perform.
    /// </summary>
    public class PrjProtokol : IBProtocol
    {
        public SharedLib.Protocol.IProtocol LocalProtocol { get; set; }


        public PrjProtokol()
        {
            LocalProtocol = new Protocol();
        }

        public string ProductXMLParser(Product toParse)
        {
            return LocalProtocol.Encode(new CreateProductCmd(toParse));
        }

        public string CategoryXMLParser(BackendProductCategory toParse)
        {
            return LocalProtocol.Encode(new CreateProductCategoryCmd(toParse));
        }

        public string EditProductXMLParser(BackendProduct toParse)
        {
            return LocalProtocol.Encode(new EditProductCmd(toParse));
        }

        public string EditCategoryXMLParser(BackendProductCategory toParse)
        {
            return LocalProtocol.Encode(new EditProductCategoryCmd(toParse));
        }

        public string DeleteCategoryXMLParser(BackendProductCategory toParse)
        {
            return LocalProtocol.Encode(new DeleteProductCategoryCmd(toParse));
        }

        public string DeleteProductXMLParser(Product toParse)
        {
            return LocalProtocol.Encode(new DeleteProductCmd(toParse));
        }

        public string GetCatalougXMLParser()
        {
            return LocalProtocol.Encode(new GetCatalogueCmd());
        }
    } //end PrjProtokol
}

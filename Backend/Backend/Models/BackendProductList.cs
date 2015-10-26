using Backend.Brains;
using Backend.Communication;
using SharedLib.Models;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class BackendProductList : ObservableCollection<Product>
    {
        #region Properties
        private int _curIndex = -1;

        public int CurrentIndex
        {
            get { return _curIndex; }
            set { _curIndex = value; }
        }
        #endregion

        // For debug onlyyyy, making fake products and categories
        public BackendProductList()
        {
            // For hver kategori i backendproductcategorilist
                // Smid produkter ind i den sliste vha getProductsByCateogires

            for (int i = 0; i < 10; i++)
            {
                BackendProduct tmpProduct = new BackendProduct()
                {
                    BName = "Name " + i,
                    BPrice = i*7/2,
                    BProductNumber = "ABC" + i,
                };
                Add(tmpProduct);
            }
        }

        public void GetCatalogue()
        {
            
            // Get necessary objects
            var getCatCmd = new GetCatalogueCmd();
            var protocol = new Protocol();

            // Convert get catalogue command to XML
            string toSend = protocol.Encode(getCatCmd);

            // Connect
            var client = new Client();
            if (!client.Connect())
            {
                return;
            }

            // Send WE WANT CATALOGUE
            client.Send(toSend);

            // Receive the catalogue
            string receive = client.Receive();

            // Close connection, because we no longer need it
            client.Disconnect();

            // Create a catalogue details command with the product list
            var catalogue = new CatalogueDetailsCmd();
            catalogue = protocol.Decode(receive) as CatalogueDetailsCmd;

            // Add the products from catalogue details command to the BackendProductList.
            catalogue.Products.ForEach(Add);
        }
    }
}

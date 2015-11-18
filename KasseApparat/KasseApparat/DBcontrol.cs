using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;

namespace KasseApparat
{
    public class DBcontrol : IDBcontrol
    {
        public IConnection Connection = null;
        public IProtocol protocol = new Protocol();

        public DBcontrol(IConnection conn)
        {
            Connection = conn;
        }

        public List<ProductCategory> GetProducts()
        {
            CatalogueDetailsCmd cmd = null;
            Connection.Connect();
            Connection.Send(protocol.Encode(new GetCatalogueCmd()));

            while (true)
            {
                protocol.AddData(Connection.Receive());

                foreach (var c in protocol.GetCommands())
                    cmd = (CatalogueDetailsCmd)c;
                if (cmd != null)
                    break;
            }
            Connection.Disconnect();
            return cmd.ProductCategories;
        }

        public void PurchaseDone(IList<PurchasedProduct> ShopList)
        {
            Connection.Connect();

            Purchase pc = new Purchase();
            pc.PurchasedProducts = ShopList.ToList();

            Connection.Send(protocol.Encode(new RegisterPurchaseCmd(pc)));

            Connection.Disconnect();
        }
    }


}

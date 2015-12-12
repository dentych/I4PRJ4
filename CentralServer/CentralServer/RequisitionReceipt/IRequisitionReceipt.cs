using SharedLib.Models;

namespace CentralServer.RequisitionReceipt
{
    public interface IRequisitionReceipt
    {
        void Write(Purchase purchase);
    }
}

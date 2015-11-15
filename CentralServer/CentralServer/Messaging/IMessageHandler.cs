
namespace CentralServer.Messaging
{
    public interface IMessageHandler
    {
        void Dispatch(long id, Message msg = null);
    }
}

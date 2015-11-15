
namespace CentralServer.Messaging
{
    public interface IMessageReceiver
    {
        void Send(long id, Message msg = null);
    }
}

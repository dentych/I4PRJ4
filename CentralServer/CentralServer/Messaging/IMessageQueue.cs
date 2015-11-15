
namespace CentralServer.Messaging
{
    public struct MessageQueueItem
    {
        public long Id;
        public Message Message;
    }


    public interface IMessageQueue
    {
        void Send(long id, Message msg = null);
        MessageQueueItem Recieve();
    }
}

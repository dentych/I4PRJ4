
namespace CentralServer.Messaging
{
    public abstract class MessageThread : Threading.ThreadBase
    {
        private readonly MessageQueue _queue;


        protected MessageThread(int maxSize = 10)
        {
            _queue = new MessageQueue(maxSize);
        }

        /*
         * Invoked whenever a message is recieved.
         */
        protected abstract void Dispatch(long id, Message msg);

        /*
         * Run the thread
         */
        protected override void Run()
        {
            while (!IsAborted())
            {
                var item = _queue.Recieve();
                Dispatch(item.Id, item.Message);
            }
        }

        /*
         * Send a message to the thread
         */
        public void Send(long id, Message msg = null)
        {
            _queue.Send(id, msg);
        }
    }
}

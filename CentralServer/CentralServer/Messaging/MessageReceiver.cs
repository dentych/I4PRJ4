using System;
using CentralServer.Threading;

namespace CentralServer.Messaging
{
    /// <summary>
    /// Implements a thread that can receive messages.
    /// </summary>
    public class MessageReceiver : IThreadRunner, IMessageReceiver
    {
        private readonly IMessageHandler _handler;
        private readonly IMessageQueue _queue;


        public MessageReceiver(IMessageHandler handler, IMessageQueue queue)
        {
            _handler = handler;
            _queue = queue;
        }

        /// <summary>
        /// Invoke to run the thread
        /// </summary>
        public void RunThread()
        {
            while (true)
            {
                var item = _queue.Recieve();

                try {
                    _handler.Dispatch(item.Id, item.Message);
                } catch (StopHandling)  {
                    break;
                }
            }
        }

        /// <summary>
        /// Send a message to this thread
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <param name="msg">Message to put in queue (optional)</param>
        public void Send(long id, Message msg = null)
        {
            _queue.Send(id, msg);
        }
    }
}

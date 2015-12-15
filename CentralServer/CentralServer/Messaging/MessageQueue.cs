using System;
using System.Collections.Concurrent;

namespace CentralServer.Messaging
{
    /// <summary>
    /// A thread-safe FIFO queue
    /// </summary>
    public class MessageQueue : IMessageQueue
    {
        private readonly BlockingCollection<MessageQueueItem> _items;


        public MessageQueue(int maxSize = 10)
        {
            if (maxSize < 1)
                throw new Exception("Queue size must be greater than zero");

            _items = new BlockingCollection<MessageQueueItem>(maxSize);
        }

        /// <summary>
        /// Put a Message in the queue. Block if full.
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <param name="msg">Message to put in queue (optional)</param>
        public void Send(long id, Message msg = null)
        {
            _items.Add(new MessageQueueItem {
                Id = id,
                Message = msg,
            });
        }

        /// <summary>
        /// Recieve an item from the queue. Block if empty.
        /// </summary>
        /// <returns>The next item from the queue</returns>
        public MessageQueueItem Recieve()
        {
            return _items.Take();
        }
    }
}

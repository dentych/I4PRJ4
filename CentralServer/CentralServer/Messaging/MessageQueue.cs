using System;
using System.Collections.Concurrent;

namespace CentralServer.Messaging
{
    public class MessageQueue : IMessageQueue
    {
        private readonly BlockingCollection<MessageQueueItem> _items;


        public MessageQueue(int maxSize = 10)
        {
            if (maxSize < 1)
                throw new Exception("Queue size must be greater than zero");

            _items = new BlockingCollection<MessageQueueItem>(maxSize);
        }

        /*
         * Put a Message in the queue.
         * Block if full.
         */
        public void Send(long id, Message msg = null)
        {
            _items.Add(new MessageQueueItem {
                Id = id,
                Message = msg,
            });
        }

        /*
         * Recieve an item from the queue.
         * Block if empty.
         */
        public MessageQueueItem Recieve()
        {
            return _items.Take();
        }
    }
}

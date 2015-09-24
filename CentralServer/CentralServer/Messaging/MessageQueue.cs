using System;
using System.Collections.Concurrent;

namespace CentralServer.Messaging
{
    public struct QueueItem
    {
        public long Id;
        public Message Message;
    }


    public class MessageQueue
    {
        private readonly BlockingCollection<QueueItem> _items;


        public MessageQueue(int maxSize)
        {
            if (maxSize < 1)
                throw new Exception("Queue size must be greater than zero");

            _items = new BlockingCollection<QueueItem>(maxSize);
        }

        /*
         * Put a Message in the queue.
         * Block if full.
         */
        public void Send(long id, Message msg = null)
        {
            _items.Add(new QueueItem {
                Id = id,
                Message = msg,
            });
        }

        /*
         * Recieve an item from the queue.
         * Block if empty.
         */
        public QueueItem Recieve()
        {
            return _items.Take();
        }
    }
}

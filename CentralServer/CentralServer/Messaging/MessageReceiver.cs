using System;
using CentralServer.Threading;

namespace CentralServer.Messaging
{
    class MessageReceiver : IThreadRunner, IMessageReceiver
    {
        private readonly IMessageHandler _handler;
        private readonly IMessageQueue _queue;


        public MessageReceiver(IMessageHandler handler, IMessageQueue queue)
        {
            _handler = handler;
            _queue = queue;
        }

        public void RunThread()
        {
            while (true)
            {
                var item = _queue.Recieve();

                try {
                    _handler.Dispatch(item.Id, item.Message);
                } catch (StopThread)  {
                    break;
                }
            }
        }

        public void Send(long id, Message msg = null)
        {
            _queue.Send(id, msg);
        }
    }
}

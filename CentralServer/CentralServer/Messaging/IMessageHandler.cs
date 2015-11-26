
using System;

namespace CentralServer.Messaging
{
    public class StopHandling : Exception { }


    public interface IMessageHandler
    {
        void Dispatch(long id, Message msg = null);
    }
}

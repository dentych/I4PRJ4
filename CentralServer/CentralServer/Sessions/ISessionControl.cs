using System.Collections.Generic;
using CentralServer.Messaging;

namespace CentralServer.Sessions
{
    public interface ISessionControl
    {
        long Register(IMessageReceiver client);
        void Unregister(long sessionId);
        IMessageReceiver GetClient(long sessionId);
        IList<IMessageReceiver> GetClients();
    }
}

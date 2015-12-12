using System;
using System.Collections.Generic;
using CentralServer.Messaging;

namespace CentralServer.Sessions
{
    public class SessionControl : ISessionControl
    {
        private readonly Dictionary<long, IMessageReceiver> _sessions =
            new Dictionary<long, IMessageReceiver>();

        private long _lastSessionId = 0;


        /*
         * Registers a client. Clients must be unique.
         */
        public long Register(IMessageReceiver client)
        {
            if (_sessions.ContainsValue(client))
                throw new Exception("Dublicate client");

            // Get next available session ID
            while (_sessions.ContainsKey(++_lastSessionId));

            _sessions[_lastSessionId] = client;
            return _lastSessionId;
        }

        /*
         * Unregisters a client
         */
        public void Unregister(long sessionId)
        {
            if(!_sessions.ContainsKey(sessionId))
                throw new ArgumentException("Unknown session ID given: " + sessionId);

            _sessions.Remove(sessionId);
        }

        /*
         * Retrieves a client by Session ID
         */
        public IMessageReceiver GetClient(long sessionId)
        {
            if (!_sessions.ContainsKey(sessionId))
                throw new Exception("Unknown session ID given: " + sessionId);

            return _sessions[sessionId];
        }
        
        /*
         * Iterate over all known clients
         */
        public IList<IMessageReceiver> GetClients()
        {
            var clients = new List<IMessageReceiver>();

            foreach (var client in _sessions.Values)
                clients.Add(client);

            return clients;
        }
    }
}

using System;
using System.Collections.Generic;

namespace CentralServer
{

    /*
     * 
     */

    class SessionControl
    {
        private readonly Dictionary<long, ClientControl> _sessions =
            new Dictionary<long, ClientControl>();

        private long _lastSessionId = 0;


        /*
         * Retrieves next available (unique) Session ID
         */
        private long GetNextSessionId()
        {
            while (_sessions.ContainsKey(++_lastSessionId));

            return _lastSessionId;
        }

        /*
         * Registers a client. Clients must be unique.
         */
        public long Register(ClientControl client)
        {
            if (_sessions.ContainsValue(client))
                throw new Exception("Dublicate client");

            var sessionId = GetNextSessionId();
            _sessions[sessionId] = client;
            return sessionId;
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
        public ClientControl GetClient(long sessionId)
        {
            if (!_sessions.ContainsKey(sessionId))
                throw new Exception("Unknown session ID given: " + sessionId);

            return _sessions[sessionId];
        }

        /*
         * Iterate over all known clients
         */
        public IEnumerable<ClientControl> GetClients()
        {
            foreach (var client in _sessions.Values)
                yield return client;
        }
    }
}

using System;
using System.Collections.Generic;

namespace CentralServer
{
    class SessionControl
    {
        private readonly Dictionary<long, ClientControl> _sessions =
            new Dictionary<long, ClientControl>();

        private long _lastSessionId = 0;


        private long GetNextSessionId()
        {
            while (_sessions.ContainsKey(++_lastSessionId));

            return _lastSessionId;
        }

        public long Register(ClientControl client)
        {
            if (_sessions.ContainsValue(client))
                throw new Exception("Dublicate client");

            var sessionId = GetNextSessionId();
            _sessions[sessionId] = client;
            return sessionId;
        }

        public void Unregister(long sessionId)
        {
            if(!_sessions.ContainsKey(sessionId))
                throw new ArgumentException("Unknown session ID given: " + sessionId);

            _sessions.Remove(sessionId);
        }

        public ClientControl GetClient(long sessionId)
        {
            if (!_sessions.ContainsKey(sessionId))
                throw new Exception("Unknown session ID given: " + sessionId);

            return _sessions[sessionId];
        }
    }
}

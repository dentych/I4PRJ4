using System;
using System.Collections.Generic;
using CentralServer.Messaging;

namespace CentralServer.Sessions
{
    /// <summary>
    /// Implements functionality to register clients and get unique session IDs.
    /// </summary>
    public class SessionControl : ISessionControl
    {
        private readonly Dictionary<long, IMessageReceiver> _sessions =
            new Dictionary<long, IMessageReceiver>();

        private long _lastSessionId = 0;


        /// <summary>
        /// Registers a client. Clients must be unique.
        /// </summary>
        /// <param name="client">The client to register</param>
        /// <returns>A unique session ID</returns>
        public long Register(IMessageReceiver client)
        {
            if (_sessions.ContainsValue(client))
                throw new Exception("Dublicate client");

            // Get next available session ID
            while (_sessions.ContainsKey(++_lastSessionId));

            _sessions[_lastSessionId] = client;
            return _lastSessionId;
        }

        /// <summary>
        /// Unregisters a client
        /// </summary>
        /// <param name="sessionId">The session ID to unregister</param>
        public void Unregister(long sessionId)
        {
            if(!_sessions.ContainsKey(sessionId))
                throw new ArgumentException("Unknown session ID given: " + sessionId);

            _sessions.Remove(sessionId);
        }

        /// <summary>
        /// Retrieves a client by Session ID
        /// </summary>
        /// <param name="sessionId">Session ID to retrieve client from</param>
        /// <returns>Client object</returns>
        public IMessageReceiver GetClient(long sessionId)
        {
            if (!_sessions.ContainsKey(sessionId))
                throw new Exception("Unknown session ID given: " + sessionId);

            return _sessions[sessionId];
        }

        /// <summary>
        /// Iterate over all known clients
        /// </summary>
        /// <returns>A list of all clients</returns>
        public IList<IMessageReceiver> GetClients()
        {
            var clients = new List<IMessageReceiver>();

            foreach (var client in _sessions.Values)
                clients.Add(client);

            return clients;
        }
    }
}

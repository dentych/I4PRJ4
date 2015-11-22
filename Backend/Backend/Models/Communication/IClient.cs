namespace Backend.Communication
{
    /// <summary>
    /// Client interface.
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Connect to the socket server (central server).
        /// </summary>
        /// <returns>True if connection was successful, otherwise false.</returns>
        bool Connect();

        /// <summary>
        /// Send data to the central server.
        /// </summary>
        /// <param name="data">Data to send.</param>
        /// <returns>True if data was sent, otherwise false.</returns>
        bool Send(string data);
    }
}
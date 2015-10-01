namespace Backend.Communication
{
    public interface IClient
    {
        string Ip { get; }
        int Port { get; }

        bool Send(string data);
    }
}
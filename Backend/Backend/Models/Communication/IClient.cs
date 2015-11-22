namespace Backend.Models.Communication
{
    public interface IClient
    {
        bool Connect();
   //     bool Disconnect();
        bool Send(string data);
      //  string Receive();
    }
}
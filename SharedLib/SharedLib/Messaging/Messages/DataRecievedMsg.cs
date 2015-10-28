
namespace SharedLib.Messaging.Messages
{
    public class DataRecievedMsg : Message
    {
        private string _data;

        public string Data { get { return _data; } }

        public DataRecievedMsg(string data)
        {
            _data = data;
        }
    }
}

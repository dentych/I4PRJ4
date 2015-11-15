
namespace CentralServer.Logging
{
    public interface ILog
    {
        void Write(string sender, int category, string text);
    }
}

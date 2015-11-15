using System;

namespace CentralServer.Threading
{
    public class StopThread : Exception {}


    public interface IThreadRunner
    {
        void RunThread();
    }
}

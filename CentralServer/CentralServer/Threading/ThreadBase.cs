using System;
using System.Threading;

namespace CentralServer.Threading
{
    public abstract class ThreadBase
    {
        private readonly Thread _thread;
        private bool _isAborted = false;

        protected abstract void Run();


        protected ThreadBase()
        {
            _thread = new Thread(RunThread);
        }

        protected void RunThread(object arg)
        {
            var thread = (ThreadBase)arg;
            thread.Run();
        }

        public void Start()
        {
            _thread.Start(this);
        }

        public void Abort()
        {
            _isAborted = true;
            _thread.Abort();
        }

        public void Join()
        {
            _thread.Join();
        }

        public bool IsAborted()
        {
            return _isAborted;
        }
    }
}

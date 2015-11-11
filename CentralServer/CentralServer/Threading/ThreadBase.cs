using System;
using System.Threading;

namespace CentralServer.Threading
{
    public static class ThreadStarter
    {
        public static Thread Start(IThreadRunner runner)
        {
            var thread = new Thread(RunThread);

            thread.Start(runner);

            return thread;
        }

        private static void RunThread(object arg)
        {
            var runner = (IThreadRunner)arg;
            runner.RunThread();
        }
    }

    /*
    public abstract class ThreadBase
    {
        private readonly Thread _thread;
        private readonly IThreadRunner _runner;
        private bool _isAborted = false;


        protected ThreadBase(IThreadRunner runner)
        {
            _runner = runner;
            _thread = new Thread(RunThread);
        }

        protected void RunThread(object arg)
        {
            var runner = (IThreadRunner)arg;
            runner.Run();
        }

        public void Start()
        {
            _thread.Start(_runner);
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
    */
}

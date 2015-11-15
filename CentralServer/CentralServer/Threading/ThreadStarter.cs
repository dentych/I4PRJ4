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
}

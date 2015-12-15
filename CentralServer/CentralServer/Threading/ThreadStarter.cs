using System;
using System.Threading;

namespace CentralServer.Threading
{
    /// <summary>
    /// A helper-class for starting threads
    /// </summary>
    public static class ThreadStarter
    {
        /// <summary>
        /// Start a new thread
        /// </summary>
        /// <param name="runner">The object which runs the thread</param>
        /// <returns>The Thread object</returns>
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

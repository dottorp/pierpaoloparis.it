using System;
using System.Threading;
using Utilities;

namespace Chapter1
{
    public class ThreadDalThreadPool : IEsempio
    {
        public void Run()
        {
            ThreadPool.QueueUserWorkItem(Worker1);
            ThreadPool.QueueUserWorkItem(Worker2);
            Utility.PremiUnTastoPerContnuare("Esecuzione terminata (E INVECE NO!), premere un tasto per terminare.");
        }

        private void Worker1(object state)
        {
            Console.WriteLine("Sono un threaad del thread pool: {0}",Thread.CurrentThread.ManagedThreadId);
            for(int i=0;i<5;i++)
            {
                Thread.Sleep(i * 1000);
                Console.WriteLine("Faccio cose, vedo gente...");
            }
        }

        private void Worker2(object state)
        {
            Console.WriteLine("Anche io sono un threaad del thread pool: {0}", Thread.CurrentThread.ManagedThreadId);
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(i * 1000);
                Console.WriteLine("Faccio cose, vedo gente...");
            }
        }
    }
}

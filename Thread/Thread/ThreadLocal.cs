using System;
using System.Threading;

namespace Chapter1
{
    public class ThreadLocal : IEsempio
    {
        private ThreadLocal<int> interoRandom;
        public ThreadLocal()
        {
            Random randomGenerator = new Random(DateTime.Now.Second);
            interoRandom = new ThreadLocal<int>(() => randomGenerator.Next(15));
        }
        public void Run()
        {
            Thread t1 = new Thread(() =>
            {
                for (int x = 0; x < interoRandom.Value; x++)
                {
                    Console.WriteLine("Thread A: {0}", x);
                }
            });
            Thread t2 = new Thread(() =>
            {
                for (int x = 0; x < interoRandom.Value; x++)
                {
                    Console.WriteLine("Thread B {0}", x);
                }
            });
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();            
            Console.WriteLine("Esecuzione terminata, premere un tasto per terminare.");
            Console.ReadKey();
        }
    }
}

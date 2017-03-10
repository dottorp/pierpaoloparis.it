using System;
using System.Threading;
namespace EsempiThread
{
    public class ThreadStatic : IEsempio
    {
        public static int contatore;
        public void Run()
        {
            new Thread(() =>
            {
                for (int x = 0; x < 10; x++)
                {
                    contatore++;
                    Console.WriteLine("Thread A: {0}", contatore);
                }
            }).Start();
            new Thread(() =>
        {
            for (int x = 0; x < 10; x++)
            {
                contatore++;
                Console.WriteLine("Thread B: {0}", contatore);
            }
        }).Start();
            Console.ReadKey();
        }
    }
}
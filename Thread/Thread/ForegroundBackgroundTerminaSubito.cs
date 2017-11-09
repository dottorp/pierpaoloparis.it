using System;
using System.Threading;

namespace Chapter1
{
    public class ForegroundBackgroundTerminaSubito : IEsempio
    {
        public void Run()
        {
            Thread bgThread = new Thread(BgWorker),fgThread = new Thread(FgWorker);
            bgThread.IsBackground = true;
            bgThread.Start();
            fgThread.Start();
            fgThread.Join();           
        }    

        private void BgWorker()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Nessuno vedrà questa scritta a video troppe volte, parola di BackgroundThread ({0})", i);
                Thread.Sleep(1000);
            }
        }
        private void FgWorker()
        {
            Console.WriteLine("Io termino subito e faccio uno scherzone al background thread");
        }
    }
}

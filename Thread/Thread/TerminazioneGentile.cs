using System;
using System.Threading;

namespace Chapter1
{
    class TerminazioneGentile:IEsempio
    {
        public void Run()
        {
            bool deviFermarti = false;
            Thread t = new Thread(new ThreadStart(() =>
            {
                Worker(ref deviFermarti);
            }));
            t.Start();
            Console.WriteLine("Premi un tasto qualunque per terminare il worker thread.");
            Console.ReadKey();
            deviFermarti = true;
            Console.WriteLine(deviFermarti);
            t.Join();
        }

        private static void Worker(ref bool deviFermarti)
        {
            while (!deviFermarti)
            {
                Console.WriteLine("Faccio cose, vedo gente...{0}",deviFermarti);
                Thread.Sleep(1000);
            }
            Console.WriteLine("Mi hanno detto che devo fermarmi e io obbedisco perché sono un bravo thread.");
        }
        
    }
}

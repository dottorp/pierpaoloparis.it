using System;
using System.Threading;
using Utilities;

namespace Chapter1
{
    public class TerminazioneBrutale : IEsempio
    {
        public void Run()
        {
            Thread t = new Thread(Worker);
            t.Start();
            Console.WriteLine("Sono il thread principale. Dormo un po' e poi commetto un threadicidio");
            Thread.Sleep(5000);
            t.Abort();
            t.Join();
            Utility.PremiUnTastoPerContnuare();
        }

        private void Worker()
        {
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine("Sono un thread qualunque. Faccio cose, vedo gente...");
                    Thread.Sleep(1000);
                }
            }
            catch (ThreadAbortException tae)
            {
                Console.WriteLine("Sono un thread qualunque. Qualcuno mi ha ucciso ({0}). Ciò è indisponente",tae.Message);
            }
            finally
            {
                Console.WriteLine("Sono un thread qualunque. Se avessi bisogno di fare testamento postumo potrei farlo qui.");
            }
        }
    }
}

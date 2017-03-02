﻿using System;
using System.Threading;

namespace EsempiThread
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
            Console.WriteLine("Esecuzione terminata. Premere un tasto per continuare.");
            Console.ReadKey();
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
            catch(ThreadAbortException tae)
            {
                Console.WriteLine("Sono un thread qualunque. Qualcuno mi ha ucciso. Ciò è indisponente");
            }
            finally
            {
                Console.WriteLine("Sono un thread qualunque. Se avessi bisogno di fare testamento potrei farlo qui.");
            }
        }
    }
}

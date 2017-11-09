﻿using System;
using System.Threading;

namespace Chapter1
{
    public class Introduzione:IEsempio
    {
        public void Run()
        {
            Thread t = new Thread(new ThreadStart(Worker));
            t.Start();
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Thread principale. Faccio cose, vedo gente...");
                Thread.Sleep(0);
            }
            t.Join();
            Console.WriteLine("Esecuzione terminata. Premere un tasto per continuare.\n");
            Console.ReadKey();
        }

        private static void Worker()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Thread worker numero {0}\n",i);
                Thread.Sleep(0);
            }
        }      
    }
}

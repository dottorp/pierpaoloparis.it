using EsempiThread;
using System.Reflection;
using System.Linq;
using System;
using System.Collections.Generic;

namespace EsempiThread
{
    public class EsempiThread
    {
        public static void Main(string[] args)
        {
            IEsempio esempio;
            Dictionary<int, Type> examples = BuildMenu();
            StampaMenu(examples);
            int numeroEsempio = 0;
            do { numeroEsempio = SelezionaEsempio(); } while (numeroEsempio > examples.Keys.Count);
            Type exType = examples[numeroEsempio];
            ConstructorInfo ctor = exType.GetConstructor(Type.EmptyTypes);
            esempio = (IEsempio)ctor.Invoke(null);
            esempio.Run();
        }



        private static int SelezionaEsempio()
        {
            int numeroEsempio = 0;
            Console.WriteLine("*********** ESEMPI ***********");
            Console.WriteLine("Inserire il numero dell'esempio da eseguire e premere invio\n");
            do
            {
                try
                {
                    numeroEsempio = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Inserire il numero dell'esempio da eseguire e premere invio\n");
                }
            } while (numeroEsempio < 0);
            return numeroEsempio;
        }

        private static Dictionary<int, Type> BuildMenu()
        {
            Dictionary<int, Type> examples = new Dictionary<int, Type>();
            int key = 1;
            foreach (Type esempio in System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(mytype => mytype.GetInterfaces().Contains(typeof(IEsempio))))
            {
                examples.Add(key, esempio);
            }
            return examples;
        }
        private static void StampaMenu(Dictionary<int, Type> examples)
        {
            foreach (int numeroEsempio in examples.Keys)
                Console.WriteLine("{0} - {1}", numeroEsempio, examples[numeroEsempio].Name);
        }
    }
}

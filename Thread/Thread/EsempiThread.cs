using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chapter1
{
    public class EsempiThread
    {
        public static void Main(string[] args)
        {
            IEsempio esempio;
            Dictionary<int, Type> esempi = BuildMenu();
            int numeroEsempio = 0;
            do { numeroEsempio = SelezionaEsempio(esempi); } while (numeroEsempio > esempi.Keys.Count);
            Type exType = esempi[numeroEsempio];
            ConstructorInfo ctor = exType.GetConstructor(Type.EmptyTypes);
            esempio = (IEsempio)ctor.Invoke(null);
            esempio.Run();           
        }



        private static int SelezionaEsempio(Dictionary<int, Type> esempi)
        {
            int numeroEsempio = 0;
            do
            {
                try
                {
                    StampaMenu(esempi);
                    numeroEsempio = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    StampaMenu(esempi);
                }
            } while (numeroEsempio <= 0);
            return numeroEsempio;
        }

        private static Dictionary<int, Type> BuildMenu()
        {
            Dictionary<int, Type> examples = new Dictionary<int, Type>();
            int key = 1;
            foreach (Type esempio in System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(mytype => mytype.GetInterfaces().Contains(typeof(IEsempio))))
            {
                examples.Add(key++, esempio);
            }
            return examples;
        }
        private static void StampaMenu(Dictionary<int, Type> examples)
        {
            Console.WriteLine("*********** ESEMPI ***********");
            Console.WriteLine("Inserire il numero dell'esempio da eseguire e premere invio\n");
            foreach (int numeroEsempio in examples.Keys)
                Console.WriteLine("{0} - {1}", numeroEsempio, examples[numeroEsempio].Name);
        }
    }
}

using System;
namespace Utilities
{
    public class Utility
    {
        public static void PremiUnTastoPerContnuare()
        {
            PremiUnTastoPerContnuare("Esecuzione terminata.\nPremi un tasto per terminare.");
        }

        public static void PremiUnTastoPerContnuare(string messaggio)
        {
            Console.WriteLine(messaggio);
            Console.ReadKey();
        }
    }
}

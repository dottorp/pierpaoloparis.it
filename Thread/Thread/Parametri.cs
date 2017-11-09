using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities;

namespace Chapter1
{
    public class Parametri : IEsempio
    {
        public void Run()
        {
            Thread t1 = new Thread(new ParameterizedThreadStart(WebDownload)),
                t2 = new Thread(new ParameterizedThreadStart(WebDownload)),
                t3 = new Thread(WebDownload);
            t1.Start("http://www.pierpaoloparis.it");
            t2.Start("https://www.google.it");
            t3.Start("https://msdn.microsoft.com/it-it/library/system.threading.parameterizedthreadstart(v=vs.110).aspx");
            t3.Join();
            Utility.PremiUnTastoPerContnuare();
        }

        private void WebDownload(object parameter)
        {
            string url = (string)parameter;
            using (var w = new WebClient())
            {
                Console.WriteLine("Sto scaricando... " + url);
                string page = w.DownloadString(url);
                Console.WriteLine("Scaricato {0}, lunghezza {1}", url, page.Length);
            }
        }
    }
}

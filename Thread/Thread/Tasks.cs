using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities;

namespace Chapter1
{
    public class Tasks : IEsempio
    {
        public void Run()
        {
            Console.WriteLine("*****TASK SEMPLICE*****");
            TaskSemplice();
            Console.WriteLine("*****TASK CHE BLOCCA*****");
            TaskCheBlocca();
            Console.WriteLine("*****TASK CHE CONTINUA CON...*****");
            ContinuaCon();
            Console.WriteLine("*****TASK CON DIVERSE OPZIONI DI TERMINAZIONE*****");
            ContinuaInDiversiModi();
            Console.WriteLine("*****TASK CON FIGLI*****");
            Figli();
            Console.WriteLine("*****TASK CON TASK FACTORY*****");
            Factory();
            Console.WriteLine("*****TASK CHE ASPETTA TUTTI*****");
            AspettaTutti();
            Console.WriteLine("*****TASK CHE ASPETTA UN TASK QUALUNQUE*****");
            AspettaneUno();
            Utility.PremiUnTastoPerContnuare();
        }

        private void TaskSemplice()
        {
            Task t = Task.Run(() => StampaUnaFraccaDiAsterischi());
            t.Wait();//come Thread.Join
        }

        private void StampaUnaFraccaDiAsterischi()
        {
            for (int i = 0; i < 100; i++)
                Console.Write("*");
            Console.WriteLine();
        }

        private void TaskCheBlocca()
        {
            Task<int> t = Task.Run(() => LaRispostaAllaDomanda());
            Console.WriteLine(t.Result);
            Console.WriteLine("Vi ho fatto attendere molto?");
        }

        private int LaRispostaAllaDomanda()
        {
            Console.WriteLine("Sto calcolando la risposta alla domanda fondamentale sulla vita, l'universo e tutto quanto");
            Thread.Sleep(10000);
            return 42;
        }

        private void ContinuaCon()
        {
            Task<int> t = Task.Run(() => LaRispostaAllaDomandaSenzaAttendereTroppo())
                .ContinueWith((risposta) => { 
                return risposta.Result * 2; });
        }

        private int LaRispostaAllaDomandaSenzaAttendereTroppo()
        {
            return 42;
        }

        private void ContinuaInDiversiModi()
        {
            Task<int> t = Task.Run(() => { return 42; });
            t.ContinueWith((i) => { Console.WriteLine("Qualcuno ha annullato."); }, TaskContinuationOptions.OnlyOnCanceled);
            t.ContinueWith((i) => { Console.WriteLine("Qualcsa è andato storto da qualche parte."); }, TaskContinuationOptions.OnlyOnFaulted);
            Task taskCheTermina = t.ContinueWith((i) => { Console.WriteLine("'ttapposto!"); }, TaskContinuationOptions.OnlyOnRanToCompletion);
            t.Wait();
        }

        private void Figli()
        {
            Task<int[]> t = Task.Run(() =>
            {
                var result = new int[3];
                new Task(() => { Task.Delay(1000); result[0] = 0; },TaskCreationOptions.AttachedToParent).Start();
                new Task(() => { Task.Delay(1000); result[1] = 1; }, TaskCreationOptions.AttachedToParent).Start();
                new Task(() => { Task.Delay(1000); result[2] = 2; }, TaskCreationOptions.AttachedToParent).Start();
                return result;
            });
            Task finale = t.ContinueWith((taskPrincipale) => {
                foreach (int result in t.Result)
                    Console.WriteLine(result);
                Console.WriteLine("Solo adesso che gli altri task sono terminati termino anche io");
            });
            finale.Wait();
        }

        private void Factory()
        {
            Task<int[]> t = Task.Run(() =>
            {
                var result = new int[3];
                TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent,TaskContinuationOptions.ExecuteSynchronously);
                tf.StartNew(() => { result[0] = 0; });
                tf.StartNew(() => { result[1] = 1; });
                tf.StartNew(() => { result[2] = 2; });
                return result;
            });
            Task finale = t.ContinueWith((taskPrincipale) => {
                foreach (int result in t.Result)
                    Console.WriteLine(result);
                Console.WriteLine("Solo adesso che gli altri task sono terminati termino anche io");
            });
            finale.Wait();
        }

        private void AspettaTutti()
        {
            Console.WriteLine("Stanno partendo 3 task, e io li aspetto tutti");
            Task[] tasks = new Task[3];
            tasks[0] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine(1);
                return 1;
            });
            tasks[1] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine(2);
                return 2;
            });
            tasks[2] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine(3);
                return 3;
            });
            Task.WaitAll(tasks);
            Console.WriteLine("Tutti i task sono terminati");
        }

        private void AspettaneUno()
        {
            Console.WriteLine("Stanno partendo 3 task, e quando termina uno termino anche io.");
            Task<int>[] tasks = new Task<int>[3];
            tasks[0] = Task.Run(() =>
            {
                Thread.Sleep(2000);
                return 1;
            });
            tasks[1] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                return 2;
            });
            tasks[2] = Task.Run(() =>
            {
                Thread.Sleep(3000);
                return 3;
            });
            Task.WaitAll(tasks);
            Console.WriteLine("Tutti i task sono terminati");
            while (tasks.Length > 0)
            {
                int i = Task.WaitAny(tasks);
                Task<int> completedTask = tasks[i];
                Console.WriteLine(completedTask.Result);
                var temp = tasks.ToList();
                temp.RemoveAt(i);
                tasks = temp.ToArray();
            }            
        }
    }
}

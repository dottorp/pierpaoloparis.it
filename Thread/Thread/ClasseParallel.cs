using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1
{
    public class ClasseParallel : IEsempio
    {
        public void Run()
        {
            ParallelFor();
            ParallelConvolution(new float[] { 2, 3, 4, 5, 9 }, new float[] { 355, 58, 4, 54, 5, 5 });
            ParallelLinq();
        }
        public void ParallelFor()
        {
            Parallel.For(0, 10, i =>
            {
                Console.WriteLine("ParallelFor:{0}", i);
                Thread.Sleep(1000);
            });
            var numbers = Enumerable.Range(0, 10);
            Parallel.ForEach(numbers, i =>
            {
                Console.WriteLine("ParallelForEach:{0}", i);
                Thread.Sleep(1000);
            });
        }
        public float[] ParallelConvolution(float[] input, float[] kernel)
        {
            float[] output = new float[input.Length];
            Parallel.For(0, input.Length, i =>
            {
                float total = 0;
                for (int k = 0; k < Math.Min(kernel.Length, i + 1); k++)
                {
                    total += input[i - k] * kernel[k];
                }
                output[i] = total;
            });
            return output;
        }
        public void ParallelLinq()
        {
            var numbers = Enumerable.Range(0, 100000000);
            var parallelResult = numbers.AsParallel().Where(i => i % 2 == 0)
                .ToArray();
        }

    }
}


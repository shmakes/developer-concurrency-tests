using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace csAwait
{
    class Program
    {
        private static async Task<int> DoWork(string name, int x, int sec) 
        {
            Console.WriteLine("Start: " + name);
            await Task.Delay(sec * 1000);
            Console.WriteLine("End: " + name);
            return x;
        }

        private static async Task<int> Flow5_6()
        {
            return await DoWork("5", 55, 4) + await DoWork("6", 60, 2);
        }

        private static async Task ComplexFlow()
        {
            try
            {
                var sw = new Stopwatch();
                    sw.Start();

                var result1 = await DoWork("1", 10, 2);
                var result2 = await DoWork("2", 25, 1);
                var finalResult = result1 + result2;


                var result = await Task.WhenAll(DoWork("3", 30, 7), DoWork("4", 45, 2), Flow5_6());
                finalResult += result.Sum();
                sw.Stop();

                Console.WriteLine("Total: " + finalResult);
                Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds / 1000} seconds");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async Task Main()
        {
            await ComplexFlow();
        }
    }
}

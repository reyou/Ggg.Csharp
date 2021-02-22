using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace intro1
{
    class Program
    {


        static async Task Main(string[] args)
        {
            IEnumerable yieldCall = YieldCall();
            foreach (object? o in yieldCall)
            {
                Console.WriteLine($"Yield result: {o}");
                Console.WriteLine("Do something in main function.");
            }
        }

        private static IEnumerable YieldCall()
        {
            int total = 0;
            while (total < 5)
            {
                total = total + 1;

                // doing some calculations or web request etc.
                Thread.Sleep(TimeSpan.FromSeconds(2));
                yield return "Still calculating.";
            }

            Console.WriteLine("Reach to target.");

            yield return total;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace intro1
{
    class Program
    {
        static readonly CancellationTokenSource CancelToken = new CancellationTokenSource();
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Start any key to start processing");
                Console.ReadKey();
                Console.WriteLine("Processing");
                Task.Factory.StartNew(() => ProcessIntData());
                Console.Write("Enter Q to quit: ");
                string answer = Console.ReadLine();
                // Does user want to quit?
                if (answer != null && answer.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    CancelToken.Cancel();
                    break;
                }
            } while (true);
            Console.ReadLine();

        }
        static void ProcessIntData()
        {
            // Get a very large array of integers.
            int[] source = Enumerable.Range(1, 10_000_000).ToArray();
            // Find the numbers where num % 3 == 0 is true, returned
            // in descending order.
            try
            {
                int[] modThreeIsZero = (from num in source.AsParallel().WithCancellation(CancelToken.Token)
                                        where num % 3 == 0
                                        orderby num descending
                                        select num).ToArray();
                Console.WriteLine();
                Console.WriteLine($"Found {modThreeIsZero.Count()} numbers that match query!");
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }

}

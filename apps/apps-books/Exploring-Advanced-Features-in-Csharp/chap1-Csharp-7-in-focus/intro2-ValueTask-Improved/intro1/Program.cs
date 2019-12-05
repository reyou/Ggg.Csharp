using System;
using System.Collections.Generic;

namespace intro1
{
    class Program
    {
        static void Main(string[] args)
        {
            ShareService shareListing = new ShareService();
            for (int i = 0; i < 100_000_000; i++)
            {
                IEnumerable<StockListing> result = shareListing.GetStockDetails().Result;
            }
            Console.WriteLine($"Garbage collection occurred {GC.CollectionCount(0)} times");
            Console.ReadLine();
        }
    }
}

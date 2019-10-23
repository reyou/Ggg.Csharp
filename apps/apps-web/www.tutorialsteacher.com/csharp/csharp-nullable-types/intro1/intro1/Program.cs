using System;

namespace intro1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GetValueOrDefault();
            Console.WriteLine("Bye World!");
            Console.ReadLine();
        }

        private static void GetValueOrDefault()
        {
            int? i = null;
            double? D = null;
            if (i.HasValue)
            {
                Console.WriteLine(i.Value);
            }
            Console.WriteLine(i.GetValueOrDefault());
        }
    }
}

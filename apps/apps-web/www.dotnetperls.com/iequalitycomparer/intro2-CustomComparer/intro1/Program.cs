using System;
using System.Collections.Generic;

namespace intro1
{
    /// <summary>
    /// https://www.dotnetperls.com/iequalitycomparer
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine();
            // ... Add data to dictionary.
            Dictionary<string, int> dictionary = new Dictionary<string, int>(new CustomComparer());
            Console.WriteLine("dictionary[\"cat-1\"] = 1;");
            dictionary["cat-1"] = 1;
            Console.WriteLine();

            Console.WriteLine("dictionary[\"cat-2\"] = 2;");
            dictionary["cat-2"] = 2;
            Console.WriteLine();

            Console.WriteLine("dictionary[\"dog-bark\"] = 10;");
            dictionary["dog-bark"] = 10;
            Console.WriteLine();

            Console.WriteLine("dictionary[\"dog-woof\"] = 20;");
            dictionary["dog-woof"] = 20;
            Console.WriteLine();

            Console.WriteLine("Lookup values, ignoring hyphens");
            Console.WriteLine();
            Console.WriteLine(dictionary["cat1"]);
            Console.WriteLine();
            Console.WriteLine(dictionary["cat-1"]);
            Console.WriteLine();
            Console.WriteLine(dictionary["dog--bark"]);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Bye World!");
            Console.ReadLine();
        }
    }
}

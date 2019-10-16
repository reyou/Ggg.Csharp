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
            Dictionary<string, bool> custom = new Dictionary<string, bool>(new StringIndexKeyComparer());
            custom.Add("22D-Array-IEnumerable", true);
            custom.Add("22D-Array-Use", true);
            custom.Add("27-Zip", true);
            custom.Add("27-Zip-DEFLATE-Ratio", true);
            custom.Add("27-Zip-Examples", true);
            custom.Add("2About", true);
            custom.Add("2Action-Dictionary", true);
            custom.Add("2Adobe-CS3-Rounded", true);
            custom.Add("2Adobe-Fireworks-CS3-Resampling", true);
            Console.WriteLine("Bye World!");
            Console.ReadLine();
        }
    }
}

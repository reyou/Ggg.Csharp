using NodaTime;
using System;

namespace intro1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DateTimeZone dateTimeZone = DateTimeZoneProviders.Tzdb["Europe/London"];
            Console.WriteLine("Bye World!");
            Console.ReadLine();
        }
    }
}

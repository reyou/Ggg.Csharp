using System;

namespace intro1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DateTime dt = DateTime.UtcNow;
            Console.WriteLine("dt.ToLocalTime():");
            Console.WriteLine(dt.ToLocalTime());

            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            // TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Pacific/Honolulu");
            DateTimeOffset utcOffset = new DateTimeOffset(dt, TimeSpan.Zero);
            TimeSpan timeSpan = tz.GetUtcOffset(utcOffset);
            Console.WriteLine("utcOffset.ToOffset(timeSpan):");
            Console.WriteLine(utcOffset.ToOffset(timeSpan));
            Console.WriteLine("Bye World!");
            Console.ReadLine();
        }
    }
}

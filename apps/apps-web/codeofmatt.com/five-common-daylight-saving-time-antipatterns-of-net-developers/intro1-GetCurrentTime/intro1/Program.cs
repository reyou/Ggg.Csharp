using System;

namespace intro1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.Local;
            Console.WriteLine("timeZoneInfo:");
            Console.WriteLine(timeZoneInfo);
            DateTimeOffset dateTimeOffset = GetCurrentTime(timeZoneInfo.Id);
            Console.WriteLine("dateTimeOffset:");
            Console.WriteLine(dateTimeOffset);
            Console.WriteLine("Bye World!");
            Console.ReadLine();
        }

        private static DateTimeOffset GetCurrentTime(string timeZoneId)
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            DateTimeOffset now = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tzi);
            return now;
        }
    }
}

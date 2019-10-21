using System;
using System.Linq;

namespace intro1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int localHour = 13;
            int localMinute = 40;
            TimeZoneInfo tzi = TimeZoneInfo.Local;
            DateTime computeNextUtcTimeToRun = ComputeNextUtcTimeToRun(localHour, localMinute, tzi);
            Console.WriteLine("computeNextUtcTimeToRun:");
            Console.WriteLine(computeNextUtcTimeToRun);
            Console.WriteLine("Bye World!");
            Console.ReadLine();
        }

        public static DateTime ComputeNextUtcTimeToRun(int localHour, int localMinute, TimeZoneInfo tzi)
        {
            // Get the current time in the target time zone
            DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);

            // Compute the next local time
            DateTime next = new DateTime(now.Year, now.Month, now.Day, localHour, localMinute, 0);
            if (next < now) next = next.AddDays(1);

            // If it's invalid, advance.  The clocks shifted forward, so might you!
            if (tzi.IsInvalidTime(next))
                return TimeZoneInfo.ConvertTimeToUtc(next.AddHours(1), tzi);

            // If it's ambiguous, pick the first instance.  Why not - You have to pick something!
            if (tzi.IsAmbiguousTime(next))
            {
                TimeSpan offset = tzi.GetAmbiguousTimeOffsets(next).Max();
                DateTimeOffset dto = new DateTimeOffset(next, offset);
                return dto.UtcDateTime;
            }

            // It's safe to use as-is.
            return TimeZoneInfo.ConvertTimeToUtc(next, tzi);
        }

    }
}

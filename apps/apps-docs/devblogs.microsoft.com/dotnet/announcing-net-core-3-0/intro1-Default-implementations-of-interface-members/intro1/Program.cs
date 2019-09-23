using System;

namespace intro1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    interface ILogger
    {
        void Log(LogLevel level, string message);
        void Log(Exception ex) => Log(LogLevel.Error, ex.ToString()); // New overload
    }

    internal enum LogLevel
    {
        Error
    }

    class ConsoleLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {

        }
        // Log(Exception) gets default implementation
    }

}

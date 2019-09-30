using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTaskRunner.ConsoleApp
{
    class Program
    {
        private static ApplicationEnvironment applicationEnvironment = ApplicationEnvironment.Local;
        // ReSharper disable once FunctionRecursiveOnAllPaths
        public static void Main(string[] args)
        {
            Guid guid = Guid.NewGuid();
            Console.WriteLine(@"Choose an option below:");
            Console.WriteLine();
            Console.WriteLine($"SessionId: {guid}");
            Console.WriteLine($"Application Environment: {applicationEnvironment}");
            Console.WriteLine();
            // list options
            List<TestSuiteMethod> testRunOptions = TestRunManager.GetTestRunOptions(applicationEnvironment);
            foreach (TestSuiteMethod testRunOption in testRunOptions)
            {
                Console.WriteLine($"{testRunOption.Order}- {testRunOption.Title} ({testRunOption.Order})");
            }
            // pick an option
            string option = Console.ReadLine();
            int optionInt;
            int.TryParse(option, out optionInt);
            TestSuiteMethod actionToRun = testRunOptions.FirstOrDefault(q => q.Order.Equals(optionInt));
            if (actionToRun == null)
            {
                Console.WriteLine();
                Console.WriteLine($"Invalid option: {option}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"===================================================");
                Console.WriteLine($"Executing: {actionToRun.Order}- {actionToRun.Title}");
                Console.WriteLine($"SessionId: {guid}");
                Console.WriteLine($"===================================================");
                Console.WriteLine();
                try
                {
                    actionToRun.TaskToRun.Invoke();
                    Console.WriteLine();
                    Console.WriteLine($"===================================================");
                    Console.WriteLine($"Executed: {actionToRun.Order}- {actionToRun.Title}");
                    Console.WriteLine($"SessionId: {guid}");
                    Console.WriteLine($"===================================================");
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Main(null);
            Console.ReadLine();
        }
    }
}

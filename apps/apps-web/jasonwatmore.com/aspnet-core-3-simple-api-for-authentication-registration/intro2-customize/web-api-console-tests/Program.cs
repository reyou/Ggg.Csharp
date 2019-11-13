using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_Console_Tests
{
    [SuppressMessage("ReSharper", "FunctionRecursiveOnAllPaths")]
    static class Program
    {
        private static readonly ApplicationEnvironment applicationEnvironment = ApplicationEnvironment.Local;

        public static async Task Main()
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
            int.TryParse(option, out var optionInt);
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
                Console.WriteLine("===================================================");
                Console.WriteLine($"Executing: {actionToRun.Order}- {actionToRun.Title}");
                Console.WriteLine($"SessionId: {guid}");
                Console.WriteLine("vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv");
                Console.WriteLine();
                try
                {
                    if (actionToRun.IsAsync)
                    {
                        await actionToRun.FunctionToRun.Invoke();
                    }
                    else
                    {
                        actionToRun.ActionToRun.Invoke();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine();
                Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                Console.WriteLine($"Executed: {actionToRun.Order}- {actionToRun.Title}");
                Console.WriteLine($"SessionId: {guid}");
                Console.WriteLine("===================================================");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
            await Main();
            Console.ReadLine();
        }
    }
}

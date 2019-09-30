using System;

namespace ConsoleTaskRunner.ConsoleApp.TestSuites
{
    public class ExceptionTests : ITestSuite
    {
        public void InvalidOperationExceptionTest(ApplicationEnvironment applicationEnvironment)
        {
            throw new InvalidOperationException("This operation is invalid");
        }
    }
}

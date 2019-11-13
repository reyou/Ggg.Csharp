using System;
using System.Threading.Tasks;

namespace WebApi_Console_Tests
{
    internal class TestSuiteMethod
    {
        public int Order { get; set; }
        public string Title { get; set; }
        public Action ActionToRun { get; set; }
        public Func<Task> FunctionToRun { get; set; }
        public bool IsAsync { get; set; }
    }
}
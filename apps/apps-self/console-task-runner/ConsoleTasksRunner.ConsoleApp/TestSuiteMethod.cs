using System;

namespace ConsoleTaskRunner.ConsoleApp
{
    internal class TestSuiteMethod
    {
        public int Order { get; set; }
        public string Title { get; set; }
        public Action TaskToRun { get; set; }
    }
}
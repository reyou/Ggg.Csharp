using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace intro1
{
    public class MySecondCustomTelemetryProcessor : ITelemetryProcessor
    {
        public void Process(ITelemetry item)
        {
            Console.WriteLine("MySecondCustomTelemetryProcessor.Process has been called.");
        }
    }
}
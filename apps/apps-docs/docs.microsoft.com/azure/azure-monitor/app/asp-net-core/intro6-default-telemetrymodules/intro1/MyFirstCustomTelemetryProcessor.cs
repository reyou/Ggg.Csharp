using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace intro1
{
    public class MyFirstCustomTelemetryProcessor : ITelemetryProcessor
    {
        public void Process(ITelemetry item)
        {
            Console.WriteLine("MyFirstCustomTelemetryProcessor.Process has been called.");
        }
    }
}
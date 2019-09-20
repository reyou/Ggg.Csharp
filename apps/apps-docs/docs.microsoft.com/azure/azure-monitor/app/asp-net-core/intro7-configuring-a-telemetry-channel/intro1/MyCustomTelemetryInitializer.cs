using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace intro1
{
    public class MyCustomTelemetryInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
          Console.WriteLine("MyCustomTelemetryInitializer.Initialize has been called.");
        }
    }
}
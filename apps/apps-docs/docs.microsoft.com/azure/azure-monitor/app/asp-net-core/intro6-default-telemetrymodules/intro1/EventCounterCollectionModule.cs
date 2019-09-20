using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Extensibility;

namespace intro1
{
    public class EventCounterCollectionModule : ITelemetryModule
    {
        public void Initialize(TelemetryConfiguration configuration)
        {
            Console.WriteLine("EventCounterCollectionModule.Initialize has been called.");
        }

        public List<EventCounterCollectionRequest> Counters { get; set; }
    }
}
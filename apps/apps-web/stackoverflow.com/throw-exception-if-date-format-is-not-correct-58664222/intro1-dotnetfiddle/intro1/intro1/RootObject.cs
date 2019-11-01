using Newtonsoft.Json;
using System;

namespace intro1
{
    [JsonConverter(typeof(DateParseHandlingConverter), DateParseHandling.None)]
    public class RootObject
    {
        [JsonConverter(typeof(DateFormatConverter), "MM-dd-yyyy")]
        public DateTime? StartDate { get; set; }
    }
}
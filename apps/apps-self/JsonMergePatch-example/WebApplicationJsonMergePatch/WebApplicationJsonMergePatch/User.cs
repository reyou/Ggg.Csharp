using System;
using Newtonsoft.Json;

namespace WebApplicationJsonMergePatch
{
    public class User
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("birthDate")]
        public DateTimeOffset BirthDate { get; set; }
    }
}

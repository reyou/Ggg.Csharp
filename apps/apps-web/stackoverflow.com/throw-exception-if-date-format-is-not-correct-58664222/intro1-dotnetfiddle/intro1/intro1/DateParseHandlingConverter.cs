using Newtonsoft.Json;
using System;

namespace intro1
{
    public class DateParseHandlingConverter : JsonConverter
    {
        readonly DateParseHandling _dateParseHandling;

        public DateParseHandlingConverter(DateParseHandling dateParseHandling)
        {
            _dateParseHandling = dateParseHandling;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            DateParseHandling dateParseHandling = reader.DateParseHandling;
            try
            {
                reader.DateParseHandling = _dateParseHandling;
                existingValue ??= serializer.ContractResolver.ResolveContract(objectType).DefaultCreator();
                serializer.Populate(reader, existingValue);
                return existingValue;
            }
            finally
            {
                reader.DateParseHandling = dateParseHandling;
            }
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
using Newtonsoft.Json.Converters;

namespace intro1
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Format of the date
        /// </summary>
        /// <param name="format"></param>
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;

        }

    }
}
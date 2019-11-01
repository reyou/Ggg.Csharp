using Newtonsoft.Json;
using System;
using System.Globalization;

namespace intro1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Test1();
            //Test2();
            //Test3();
            Test4_DateTimeOffset();
            //Test5_Parse_Exact();
            //Test6_Parse_Exact();
            Console.WriteLine("Bye World!");
            Console.ReadLine();
        }

        private static void Test6_Parse_Exact()
        {
            try
            {
                DateTime dateTime = DateTime.ParseExact("2019-10-17T00:14:35.8384165-04:00", "mm-dd-yyyy", CultureInfo.InvariantCulture);
                Console.WriteLine(dateTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Test5_Parse_Exact()
        {
            DateTime dateTime = DateTime.ParseExact("10-20-2019", "mm-dd-yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine(dateTime);
        }

        private static void Test4_DateTimeOffset()
        {
            string stringDate = "\"2019-10-17T00:14:35.8384165-04:00\"";
            DateTimeOffset deserializeObject = JsonConvert.DeserializeObject<DateTimeOffset>(stringDate, new DateFormatConverter("mm-dd-yyyy"));
            Console.WriteLine(deserializeObject);
        }

        private static void Test1()
        {
            string stringDate = "\"10-20-2019\"";
            DateTime deserializeObject = JsonConvert.DeserializeObject<DateTime>(stringDate, new DateFormatConverter("mm-dd-yyyy"));
            Console.WriteLine(deserializeObject);
        }

        private static void Test2()
        {
            string stringDate = "\"10/20/2019\"";
            DateTime deserializeObject = JsonConvert.DeserializeObject<DateTime>(stringDate, new DateFormatConverter("mm/dd/yyyy"));
            Console.WriteLine(deserializeObject);
        }

        private static void Test3()
        {
            string serializeObject = JsonConvert.SerializeObject(DateTime.Now.ToString("mm-dd-yyyy"));
            string json = "\"7 December, 2009\"";
            DateTime dateTime = JsonConvert.DeserializeObject<DateTime>(json, new JsonSerializerSettings
            {
                DateFormatString = "d MMMM, yyyy"
            });
            Console.WriteLine(dateTime);
        }




    }
}

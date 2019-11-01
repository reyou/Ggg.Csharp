using Newtonsoft.Json;
using System;

namespace intro1
{
    /// <summary>
    /// https://dotnetfiddle.net/Y18NPk
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Test1_Exception();
            // Test2_Parse();
            // Test3_Serialize();
            Console.WriteLine("Bye World!");
            Console.ReadLine();
        }

        private static void Test3_Serialize()
        {
            RootObject obj = new RootObject()
            {
                StartDate = new DateTime(2019, 10, 20)
            };
            string serializeObject = JsonConvert.SerializeObject(obj, Formatting.Indented);
            Console.WriteLine(serializeObject);
        }

        private static void Test2_Parse()
        {
            string json = @"{'StartDate': '10-20-2019'}";
            RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
            Console.WriteLine(root.StartDate.ToString());
        }

        private static void Test1_Exception()
        {
            try
            {
                string json = @"{'StartDate': '2019-10-17T00:14:35.8384165-04:00'}";
                RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
                Console.WriteLine(root.StartDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}

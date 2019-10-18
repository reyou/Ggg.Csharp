using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace intro1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            HttpClient client = new HttpClient();
            int count = 10000;
            List<Task<HttpResponseMessage>> tasks = new List<Task<HttpResponseMessage>>();
            for (int i = 0; i < count; i++)
            {
                Task<HttpResponseMessage> task = client.GetAsync("http://www.example.com");
                tasks.Add(task);
            }

            HttpResponseMessage[] httpResponseMessages = await Task.WhenAll(tasks);
            int counter = 0;
            foreach (HttpResponseMessage httpResponseMessage in httpResponseMessages)
            {
                string readAsStringAsync = await httpResponseMessage.Content.ReadAsStringAsync();
                Console.WriteLine(readAsStringAsync);
                Console.WriteLine($"Fetched: {++counter}");
            }

            Console.WriteLine("Bye World!");
            Console.ReadLine();
        }
    }
}

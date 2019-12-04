using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace intro1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!\n\n");
            // await Example1();
            await Example2();
            Console.WriteLine("\n\nEnd World!");
            Console.ReadLine();
        }

        private static async Task Example2()
        {
            string sampleString = GetSampleString(5_000);
            byte[] bytes = Encoding.UTF8.GetBytes(sampleString);
            MemoryStream2 memoryStream2 = new MemoryStream2(sampleString);
            ValueTask<int> readAsync = memoryStream2.ReadAsync(bytes, 0, bytes.Length);
            int readAsyncResult;
            if (readAsync.IsCompletedSuccessfully)
            {
                readAsyncResult = readAsync.Result;
                Console.WriteLine($"readAsyncResult: {readAsyncResult}");
            }
            else
            {
                readAsyncResult = await readAsync;
                Console.WriteLine($"await readAsyncResult: {readAsyncResult}");
            }
        }

        private static string GetSampleString(int length)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append("this is a sample string. ");
            }
            return builder.ToString();
        }

        private static async Task Example1()
        {
            string sampleString = "this is a sample string.";
            byte[] bytes = Encoding.UTF8.GetBytes(sampleString);
            MemoryStream2 memoryStream2 = new MemoryStream2(sampleString);
            int readAsync = await memoryStream2.ReadAsync(bytes, 0, bytes.Length);
            Console.WriteLine($"Result: {readAsync}");
        }
    }

    class MemoryStream2 : MemoryStream
    {
        public MemoryStream2(string sampleString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(sampleString);
            base.Write(bytes, 0, bytes.Length);
            base.Seek(0, SeekOrigin.Begin);
        }

        public new ValueTask<int> ReadAsync(byte[] buffer, int offset, int count)
        {
            try
            {
                int bytesRead = Read(buffer, offset, count);
                return new ValueTask<int>(bytesRead);
            }
            catch (Exception e)
            {
                return new ValueTask<int>(Task.FromException<int>(e));
            }
        }
    }


}

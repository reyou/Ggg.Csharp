﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.DataflowBlockReceivesData
{
    /// <summary>
    /// Demonstrates how to provide delegates to exectution dataflow blocks
    /// https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-perform-action-when-a-dataflow-block-receives-data
    /// </summary>
    class DataflowExecutionBlocks
    {
        // Computes the number of zero bytes that the provided file
        // contains.
        static int CountBytes(string path)
        {
            byte[] buffer = new byte[1024];
            int totalZeroBytesRead = 0;
            using (FileStream fileStream = File.OpenRead(path))
            {
                int bytesRead;
                do
                {
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                    totalZeroBytesRead += buffer.Count(b => b == 0);
                } while (bytesRead > 0);
            }
            return totalZeroBytesRead;
        }

        public static void Run()
        {
            // Create a temporary file on disk.
            string tempFile = Path.GetTempFileName();
            // Write random data to the temporary file.
            using (FileStream fileStream = File.OpenWrite(tempFile))
            {
                Random rand = new Random();
                byte[] buffer = new byte[1024];
                for (int i = 0; i < 512; i++)
                {
                    rand.NextBytes(buffer);
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }

            // Create an ActionBlock<int> object that prints to the console 
            // the number of bytes read.
            // Provides a dataflow block that invokes a provided Action<T> delegate 
            // for every data element received
            ActionBlock<int> printResult = new ActionBlock<int>(zeroBytesRead =>
            {
                Console.WriteLine("{0} contains {1} zero bytes.", Path.GetFileName(tempFile), zeroBytesRead);
            });

            // Create a TransformBlock<string, int> object that calls the 
            // CountBytes function and returns its result.
            // TransformBlock<TInput,TOutput> Class
            TransformBlock<string, int> countBytes = new TransformBlock<string, int>(new Func<string, int>(CountBytes));
            // Link the TransformBlock<string, int> object to the 
            // ActionBlock<int> object.
            // Links the ISourceBlock<TOutput> to the specified ITargetBlock<TInput>.
            countBytes.LinkTo(printResult);
            // Create a continuation task that completes the ActionBlock<int>
            // object when the TransformBlock<string, int> finishes.
            // Completion: Gets a Task that represents the asynchronous operation and 
            // completion of the dataflow block
            countBytes.Completion.ContinueWith(delegate { printResult.Complete(); });

            // Post the path to the temporary file to the 
            // TransformBlock<string, int> object.
            countBytes.Post(tempFile);

            // Requests completion of the TransformBlock<string, int> object.
            countBytes.Complete();

            // Wait for the ActionBlock<int> object to print the message.
            printResult.Completion.Wait();

            // Delete the temporary file.
            File.Delete(tempFile);
        }

    }
}

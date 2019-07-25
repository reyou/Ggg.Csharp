using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Newtonsoft.Json;

namespace ConsoleTaksRunner.ConsoleApp
{
    public class TestUtilities
    {
        public static void ConsoleWriteJson(object item)
        {
            Console.WriteLine();
            Console.WriteLine(JsonConvert.SerializeObject(new
            {
                Thread.CurrentThread.ManagedThreadId,
                ThreadName = Thread.CurrentThread.Name,
                item
            }, Formatting.Indented));
            Console.WriteLine();
        }
        public static void ThreadSleepSeconds(int seconds, string message = "")
        {
            Console.WriteLine($"{message} - ManagedThreadId:{Thread.CurrentThread.ManagedThreadId} " +
                              $"(Name:{Thread.CurrentThread.Name}) sleeps {seconds} sec...");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }
        public static void MethodEnds()
        {
            Console.WriteLine();
            Console.WriteLine("=========== Method Ends =================");
            Console.WriteLine();
        }

        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        public static void RunPowershell(string filePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.RedirectStandardError = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.FileName = "powershell.exe";
            startInfo.Arguments = $"& '{filePath}'";
            Process process = Process.Start(startInfo);
            if (process != null)
            {
                string message = $"{process.Id} started.";
                TestUtilities.ConsoleWriteJson(new
                {
                    message,
                    command = $"{startInfo.FileName} {startInfo.Arguments}",
                });
            }
        }

        public static void RunBash(string filePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.RedirectStandardError = false;
            startInfo.FileName = "/bin/bash";
            startInfo.Arguments = $"\"{filePath}\"";
            Process process = Process.Start(startInfo);
            if (process != null)
            {
                string message = $"{process.Id} started.";
                TestUtilities.ConsoleWriteJson(new
                {
                    message,
                    command = $"{startInfo.FileName} {startInfo.Arguments}",
                });
            }
        }
    }
}
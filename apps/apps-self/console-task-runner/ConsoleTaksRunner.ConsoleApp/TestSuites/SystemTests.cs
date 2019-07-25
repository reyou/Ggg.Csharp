using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleTaksRunner.ConsoleApp.TestSuites
{
    public class SystemTests : ITestSuite
    {

        public void NetStatBash(ApplicationEnvironment applicationEnvironment)
        {
            if (TestUtilities.IsLinux())
            {
                string filePathExecute = "./Assets/SystemTests/NetStatBash.sh";
                FileInfo fileInfo = new FileInfo(filePathExecute);
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.FileName = "/bin/bash";
                startInfo.Arguments = $"\"{fileInfo.FullName}\"";
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
            else
            {
                TestUtilities.ConsoleWriteJson(new
                {
                    Message = "This method only works in Linux or OSX"
                });
            }
        }
        public void NetTcpConnectionPowershell(ApplicationEnvironment applicationEnvironment)
        {
            if (TestUtilities.IsWindows())
            {
                string filePathExecute = "./Assets/SystemTests/NetTcpConnectionPowershell.ps1";
                FileInfo fileInfo = new FileInfo(filePathExecute);
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = true;
                startInfo.RedirectStandardError = false;
                startInfo.RedirectStandardOutput = false;
                startInfo.FileName = "powershell.exe";
                startInfo.Arguments = $"& '{fileInfo.FullName}'";
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
            else
            {
                TestUtilities.ConsoleWriteJson(new
                {
                    Message = "This method only works in Windows"
                });
            }

        }

        public void GetRuntimeInformation(ApplicationEnvironment applicationEnvironment)
        {
            TestUtilities.ConsoleWriteJson(new
            {
                RuntimeInformation.OSDescription,
                RuntimeInformation.OSArchitecture,
                RuntimeInformation.FrameworkDescription,
                RuntimeInformation.ProcessArchitecture,
                IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows),
                IsLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux),
                IsOSX = RuntimeInformation.IsOSPlatform(OSPlatform.OSX),
            });
        }

        public void GetEnvironmentProperties(ApplicationEnvironment applicationEnvironment)
        {
            TestUtilities.ConsoleWriteJson(new
            {
                Environment.CommandLine,
                Environment.CurrentDirectory,
                Environment.CurrentManagedThreadId,
                Environment.ExitCode,
                Environment.HasShutdownStarted,
                Environment.Is64BitOperatingSystem,
                Environment.Is64BitProcess,
                Environment.MachineName,
                Environment.NewLine,
                Environment.OSVersion,
                Environment.ProcessorCount,
                Environment.SystemDirectory,
                Environment.TickCount,
                Environment.SystemPageSize,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.Version,
                Environment.UserInteractive,
                Environment.WorkingSet,
                Environment.SpecialFolder.Desktop,
                Environment.SpecialFolder.UserProfile,
                LogicalDrives = Environment.GetLogicalDrives(),
                EnvironmentVariables = Environment.GetEnvironmentVariables()

            });
        }
    }
}

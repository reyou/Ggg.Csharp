using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTaksRunner.ConsoleApp.TestSuites
{
    public class SystemTests : ITestSuite
    {
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
                LogicalDrives = Environment.GetLogicalDrives(),
                Environment.SpecialFolder.Desktop,
                Environment.SpecialFolder.UserProfile,
            });
        }
    }
}

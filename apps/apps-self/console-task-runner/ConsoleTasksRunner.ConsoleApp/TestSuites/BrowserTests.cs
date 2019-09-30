using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleTaskRunner.ConsoleApp.TestSuites
{
    public class BrowserTests : ITestSuite
    {
        public void OpenBookmarks(ApplicationEnvironment applicationEnvironment)
        {
            List<string> urls = new List<string>();
            urls.Add("http://github.com");
            foreach (string url in urls)
            {
                Process process = Process.Start("cmd", $"/C start {url}");
                if (process != null)
                {
                    TestUtilities.ConsoleWriteJson(new
                    {
                        process.Id,
                        process.MachineName,
                        process.ProcessName
                    });
                }
            }
        }
    }
}

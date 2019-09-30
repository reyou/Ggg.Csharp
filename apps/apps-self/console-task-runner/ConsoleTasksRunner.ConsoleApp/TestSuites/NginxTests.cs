using System.IO;

namespace ConsoleTaskRunner.ConsoleApp.TestSuites
{
    public class NginxTests : ITestSuite
    {
        public void AllRunningNginxProcessesBash(ApplicationEnvironment applicationEnvironment)
        {
            if (TestUtilities.IsLinux())
            {
                string filePathExecute = "./Assets/SystemTests/AllRunningNginxProcessesBash.sh";
                FileInfo fileInfo = new FileInfo(filePathExecute);
                TestUtilities.RunBash(fileInfo.FullName);
            }
            else
            {
                TestUtilities.ConsoleWriteJson(new
                {
                    Message = "This method only works in Linux or OSX"
                });
            }
        }
    }
}

using System.IO;

namespace ConsoleTaskRunner.ConsoleApp.TestSuites
{
    class FileTests : ITestSuite
    {
        public void ListFolders(ApplicationEnvironment applicationEnvironment)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\");
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            foreach (DirectoryInfo info in directoryInfos)
            {
                TestUtilities.ConsoleWriteJson(new
                {
                    info.Name,
                    info.FullName,
                });
                TestUtilities.ThreadSleepSeconds(1, "ListFolders");
            }
        }
    }
}

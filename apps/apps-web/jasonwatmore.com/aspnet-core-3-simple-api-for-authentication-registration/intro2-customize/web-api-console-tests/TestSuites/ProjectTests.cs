using System.Diagnostics;
using System.IO;
using WebApi;

namespace WebApi_Console_Tests.TestSuites
{
    /// <summary>
    /// <see cref="Startup"/>
    /// </summary>
    public class ProjectTests : ITestSuite
    {
        public void StartProject(ApplicationEnvironment applicationEnvironment)
        {
            string filePathExecute = "./Assets/ProjectTests/StartProject.ps1";
            FileInfo fileInfo = new FileInfo(filePathExecute);
            TestUtilities.RunPowershell(fileInfo.FullName);
        }

        public void OpenLocahost4000(ApplicationEnvironment applicationEnvironment)
        {
            string url = "http://localhost:4000";
            Process process = Process.Start("cmd", $"/C start {url}");
        }
    }
}

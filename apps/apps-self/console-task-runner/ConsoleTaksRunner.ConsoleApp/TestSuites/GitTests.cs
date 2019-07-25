﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleTaksRunner.ConsoleApp.TestSuites
{
    class GitTests : ITestSuite
    {
        public void GitCommitPushPowershell(ApplicationEnvironment applicationEnvironment)
        {
            if (TestUtilities.IsWindows())
            {
                string filePathExecute = "./Assets/SystemTests/GitCommitPushPowershell.ps1";
                FileInfo fileInfo = new FileInfo(filePathExecute);
                string currentDirectory = Environment.CurrentDirectory;
                TestUtilities.RunPowershell(fileInfo.FullName);
            }
            else
            {
                TestUtilities.ConsoleWriteJson(new
                {
                    Message = "This method only works in Windows"
                });
            }

        }
    }
}

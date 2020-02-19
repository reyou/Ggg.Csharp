using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CompanyManagement.Test.Testdata
{
    public class ExternalEmployeeTestData
    {
        public static IEnumerable<object[]> TestDataFromFile
        {
            get
            {
                var lines = File.ReadAllLines("TestDataCSV.csv");

                return lines.Select(line => line.Split(',').Cast<object>().ToArray()).ToList();
            }
        }
    }
}
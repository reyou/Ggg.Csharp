using System.Collections.Generic;

namespace CompanyManagement.Test.Testdata
{
    public class EmployeeAgeTestData
    {
        // testdata can be used in several classes
        public static IEnumerable<object[]> TestData
        {
            get
            {
                yield return new object[] { -100, false };
                yield return new object[] { 17, false };
                yield return new object[] { 18, true };
                yield return new object[] { 65, true };
                yield return new object[] { 66, false };
            }
        }
    }
}
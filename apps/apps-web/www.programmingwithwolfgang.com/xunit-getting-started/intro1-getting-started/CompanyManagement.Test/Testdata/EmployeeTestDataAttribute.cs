using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace CompanyManagement.Test.Testdata
{
    public class EmployeeTestDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { -100, false };
            yield return new object[] { 17, false };
            yield return new object[] { 18, true };
            yield return new object[] { 65, true };
            yield return new object[] { 66, false };
        }
    }
}
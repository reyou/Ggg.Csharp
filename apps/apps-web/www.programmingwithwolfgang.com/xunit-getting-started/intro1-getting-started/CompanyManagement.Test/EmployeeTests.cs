using CompanyManagement.Test.Testdata;
using FluentAssertions;
using System;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace CompanyManagement.Test
{
    [Collection("My Collection Name")]
    public class EmployeeTests : IDisposable
    {
        private readonly Employee _testee;
        private readonly ITestOutputHelper _outputHelper;
        private readonly TimeFixture _timeFixture;

        public EmployeeTests(ITestOutputHelper outputHelper, TimeFixture timeFixture)
        {
            // constructor is called for every test
            _testee = new Employee();
            _outputHelper = outputHelper;
            _timeFixture = timeFixture;
        }

        public void Dispose()
        {
            // do some cleanup for every test
        }

        [Theory]
        [InlineData(-100, false)]
        [InlineData(17, false)]
        [InlineData(18, true)]
        [InlineData(65, true)]
        [InlineData(66, false)]
        public void TheoryTest_WithInlineData(int age, bool expectedResult)
        {
            var result = age >= 18 && age <= 65;

            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(EmployeeAgeTestData.TestData), MemberType = typeof(EmployeeAgeTestData))]
        public void TheoryTest_WithMemberData(int age, bool expectedResult)
        {
            var result = age >= 18 && age <= 65;

            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(ExternalEmployeeTestData.TestDataFromFile), MemberType = typeof(ExternalEmployeeTestData))]
        public void TheoryTest_WithExternalData(int age, bool expectedResult)
        {
            var result = age >= 18 && age <= 65;

            result.Should().Be(expectedResult);
        }

        [Theory]
        [EmployeeTestData]
        public void TheoryTest_WithCustomDataAttribute(int age, bool expectedResult)
        {
            var result = age >= 18 && age <= 65;

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void WhenCreatingEmployee_ShouldHaveSalaryOf1000()
        {
            var testee = new Employee();

            testee.Salary.Should().Be(1000);
        }

        [Fact]
        public void ShouldHaveWorkingHours()
        {
            _testee.WorkingHours = 40;
            // wirite message to the result window
            _outputHelper.WriteLine("Creating Emplyoee with 40 hours working");

            _testee.WorkingHours.Should().Be(40);
        }

        [Fact(Skip = "This test is skipped")]
        public void SkippedTest()
        {
            // test something
        }

        [Fact]
        public void TestWithSameTimeFixture()
        {
            _outputHelper.WriteLine($"{_timeFixture.DateTime:O}");

            Thread.Sleep(1500);
        }

        [Fact]
        public void AnotherTestWithTimeFixture()
        {
            _outputHelper.WriteLine($"{_timeFixture.DateTime:O}");

            Thread.Sleep(1500);
        }

        [Fact]
        [Trait("Name", "Category")]
        public void TestWithTrait()
        {
            // test something
        }
    }
}
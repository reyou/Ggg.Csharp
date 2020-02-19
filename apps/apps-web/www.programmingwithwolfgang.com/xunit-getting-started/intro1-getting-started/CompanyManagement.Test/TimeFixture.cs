using System;

namespace CompanyManagement.Test
{
    public class TimeFixture
    {
        public TimeFixture()
        {
            DateTime = DateTime.Now;
        }

        public DateTime DateTime { get; set; }
    }
}
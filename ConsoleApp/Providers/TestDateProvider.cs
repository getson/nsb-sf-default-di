using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Providers
{
    public interface ITestDateProvider
    {
        DateTime GetDateNow();
    }
    public class TestDateProvider : ITestDateProvider
    {
        public DateTime GetDateNow()
        {
            return DateTime.UtcNow;
        }
    }
}

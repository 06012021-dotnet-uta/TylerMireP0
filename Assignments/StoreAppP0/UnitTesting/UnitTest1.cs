using System;
using Application;
using Persistence;
using Xunit;

namespace UnitTesting
{
    public class UnitTest1
    {

        private readonly BusinessApplicaiton _businessApplication;
        private readonly DataContext _dataContext;

        public UnitTest1()
        {
            _dataContext = new DataContext();
        }

        [Fact]
        public void RegisterCustomerTest()
        {

        }
    }
}

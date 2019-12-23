using ConsoleApp.Providers;
using Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Messaging.Handlers
{
    public class TestHandler2 : IHandleMessages<TestMessage>
    {
        private readonly ITestDateProvider _testDateProvider;

        public TestHandler2(ITestDateProvider testDateProvider)
        {
            _testDateProvider = testDateProvider;
        }

        public Task Handle(TestMessage message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Handled: {message.Id} - {message.Name} utc: {_testDateProvider.GetDateNow()}");
            return Task.CompletedTask;
        }
    }
}

using NServiceBus;
using System;

namespace Messages
{
    public class TestMessage:IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

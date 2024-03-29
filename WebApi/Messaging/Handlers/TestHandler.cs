﻿using Messages;
using NServiceBus;
using System;
using System.Threading.Tasks;
using WebApi.Providers;

namespace WebApi.Messaging.Handlers
{
    public class TestHandler : IHandleMessages<TestMessage>
    {
        private readonly IGenerateIdentityProvider _identityProvider;

        public TestHandler(IGenerateIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public Task Handle(TestMessage message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Handled: {message.Id} - {message.Name} identity: {_identityProvider.Generate()}");
            return Task.CompletedTask;
        }
    }
}

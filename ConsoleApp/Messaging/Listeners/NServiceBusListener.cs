using Microsoft.ServiceFabric.Services.Communication.Runtime;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Messaging.Listeners
{
    public class NServiceBusListener : ICommunicationListener
    {
        private readonly string _endpointName;
        private IEndpointInstance _endpointInstance;
        public NServiceBusListener(StatelessServiceContext context, string endpointName, IEndpointInstance endpointInstance)
        {
            _endpointName = endpointName;
            _endpointInstance = endpointInstance;
        }

        public void Abort()
        {
            CloseAsync(CancellationToken.None).Wait();
        }

        public async Task CloseAsync(CancellationToken cancellationToken)
        {
            await _endpointInstance.Stop();
        }

        public Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_endpointName);
            
        }
    }
}

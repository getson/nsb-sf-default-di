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
        private StatelessServiceContext _context;
        private readonly string _endpointName;
        private readonly IStartableEndpoint _startableEndpoint;
        private IEndpointInstance _endpointInstance;
        public NServiceBusListener(StatelessServiceContext context, string endpointName, IStartableEndpoint startableEndpoint)
        {
            _context = context;
            _endpointName = endpointName;
            _startableEndpoint = startableEndpoint;
        }

        public void Abort()
        {
            CloseAsync(CancellationToken.None).Wait();
        }

        public async Task CloseAsync(CancellationToken cancellationToken)
        {
            await _endpointInstance.Stop();
        }

        public async Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            _endpointInstance = await _startableEndpoint.Start();
            return _endpointName;
        }
    }
}

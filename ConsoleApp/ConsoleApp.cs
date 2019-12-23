using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp.Messaging.Listeners;
using ConsoleApp.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using NServiceBus;

namespace ConsoleApp
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class ConsoleApp : StatelessService
    {
        private const string _endpointName = "ConsoleApp-endpoint";
        public ConsoleApp(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            var services = new ServiceCollection();
            var serviceProvider = ConfigureServices(services);
            return new[]
            {
                new ServiceInstanceListener(context
                        => new NServiceBusListener(context,
                                    _endpointName,
                                    serviceProvider.GetRequiredService<IEndpointInstance>())
                        )
            };
        }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITestDateProvider, TestDateProvider>();


            var endpointConfiguration = new EndpointConfiguration(_endpointName);
            endpointConfiguration.UseTransport<LearningTransport>()
                                 .StorageDirectory($"{AppContext.BaseDirectory}\\.learningtransport");

            endpointConfiguration.UseContainer<ServicesBuilder>(c => c.ExistingServices(services));


            var endpointInstance = Endpoint.Start(endpointConfiguration)
                                               .GetAwaiter()
                                               .GetResult();
            services.AddSingleton(endpointInstance);

            return services.BuildServiceProvider();
        }
    }
}

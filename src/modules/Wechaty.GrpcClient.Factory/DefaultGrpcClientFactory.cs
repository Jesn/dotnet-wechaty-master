using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wechaty.Grpc.Client;

namespace Wechaty.GrpcClient.Factory
{
    internal class DefaultGrpcClientFactory : IGrpcClientFactory
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _services;
        private readonly IServiceScopeFactory _scopeFactory;

        private static readonly ConcurrentDictionary<string, WechatyPuppetClient> PuppetClientList = new ConcurrentDictionary<string, WechatyPuppetClient>();

        public DefaultGrpcClientFactory(
            IServiceProvider services,
            IServiceScopeFactory scopeFactory,
            ILoggerFactory loggerFactory
            )
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _services = services ?? throw new ArgumentNullException(nameof(services));
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
            _logger = loggerFactory.CreateLogger<DefaultGrpcClientFactory>();
        }


        public WechatyPuppetClient CreateClient(GrpcPuppetOption option)
        {
            if (option == null || string.IsNullOrWhiteSpace(option.ENDPOINT) || string.IsNullOrWhiteSpace(option.Token))
            {
                throw new ArgumentNullException(nameof(option));
            }
            if (string.IsNullOrWhiteSpace(option.Name))
            {
                option.Name = "Default";
            }

            var client = PuppetClientList.GetValueOrDefault(option.Name);
            if (client != null)
            {
                return client;
            }

            client = new WechatyPuppetClient(option);
            _ = client.StartAsync();

            PuppetClientList.GetOrAdd(option.Name, client);

            return client;
        }

        public WechatyPuppetClient GetClient(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            var client = PuppetClientList.GetValueOrDefault(name);
            return client;
        }




    }
}

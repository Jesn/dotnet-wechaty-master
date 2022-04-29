using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wechaty.Grpc.Client;
using Wechaty.GrpcClient.Factory;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WechatyGrpcFactoryServiceCollectionExtensions
    {
        public static IServiceCollection AddWechatyGrpc(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            //services.AddOptions();

            services.AddSingleton<DefaultGrpcClientFactory>();
            services.TryAddSingleton<IGrpcClientFactory>(serviceProvider => serviceProvider.GetRequiredService<DefaultGrpcClientFactory>());

            return services;

        }

        public static IGrpcClientBuilder AddWechatyGrpc(this IServiceCollection services, GrpcPuppetOption option)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (option == null)
            {
                throw new ArgumentNullException(nameof(option));
            }

            AddWechatyGrpc(services);

            return new DefaultGrpcClientBuilder(services, option);
        }
    }
}

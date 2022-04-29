using System;
using Wechaty.Grpc.Client;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WechatyGrpcBuilderExtensions
    {
        public static IGrpcClientBuilder ConfigureWechatyGrpcClient(this IGrpcClientBuilder builder, Action<WechatyPuppetClient> configureClient)
        {
            if (builder==null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (configureClient==null)
            {
                throw new ArgumentNullException(nameof(configureClient));
            }



            return builder;
        }
    }
}

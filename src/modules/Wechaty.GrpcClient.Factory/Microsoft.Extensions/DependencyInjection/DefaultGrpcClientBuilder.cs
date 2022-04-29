using Wechaty.Grpc.Client;

namespace Microsoft.Extensions.DependencyInjection
{
    internal class DefaultGrpcClientBuilder : IGrpcClientBuilder
    {
        public DefaultGrpcClientBuilder(IServiceCollection services, GrpcPuppetOption option)
        {
            Services = services;
            Option = option;
        }
        public GrpcPuppetOption Option { get; }

        public IServiceCollection Services { get; }
    }
}

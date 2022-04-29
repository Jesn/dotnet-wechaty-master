using Wechaty.Grpc.Client;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IGrpcClientBuilder
    {
        GrpcPuppetOption Option { get; }
        IServiceCollection Services { get; }
    }
}

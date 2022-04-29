using Wechaty.Grpc.Client;
using static Wechaty.Puppet;

namespace Wechaty.GrpcClient.Factory
{
    public interface IGrpcClientFactory
    {
        WechatyPuppetClient CreateClient(GrpcPuppetOption option);

        WechatyPuppetClient GetClient(string name);

    }
}

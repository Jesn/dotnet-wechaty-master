using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Grpc.Core;
using Wechaty.Module.Puppet.Schemas;
using static Wechaty.Puppet;

namespace Wechaty.Grpc.PuppetService
{
    public interface IWechatyPuppetService : IApplicationService
    {
        PuppetClient InitGrpcClient(PuppetOptions options);

        Task StartAsync();

        Task StopGrpcClient();

        AsyncServerStreamingCall<EventResponse> EventStream();
    }
}

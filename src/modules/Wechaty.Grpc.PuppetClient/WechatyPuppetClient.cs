using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Grpc.Core;
using static Wechaty.Puppet;

namespace Wechaty.Grpc.Client
{
    public partial class WechatyPuppetClient
    {
        private PuppetClient _grpcClient;

        internal GrpcPuppetOption _option;
        public WechatyPuppetClient(GrpcPuppetOption option)
        {
            _option = option;
        }


        public async Task StartAsync()
        {
            _grpcClient = DiscoverPupptClient.InitGrpcClient(_option);
            await _grpcClient.StartAsync(new StartRequest());

        }

        public async Task StopAsync()
        {
            await _grpcClient.StopAsync(new StopRequest());
        }




    }
}

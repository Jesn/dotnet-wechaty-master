using System;
using System.Net.Http;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Grpc.Core;
using Grpc.Net.Client;
using Newtonsoft.Json;
using Wechaty.GateWay;
using Wechaty.Module.Puppet.Schemas;
using static Wechaty.Puppet;
using GrpcClient = Grpc.Net.Client;

namespace Wechaty.Grpc.PuppetService
{
    public class WechatyPuppetService : IWechatyPuppetService
    {
        protected PuppetClient _grpcClient;

        public WechatyPuppetService()
        {
            var grpcFactory = WechatyGrpcFactory.GetGrpcClientInstace(Constant.GrpcInstaceName);
            _grpcClient = grpcFactory._grpcClient;
        }


        public AsyncServerStreamingCall<EventResponse> EventStream()
        {
            var eventStream = _grpcClient.Event(new EventRequest());
            return eventStream;
        }
    }
}

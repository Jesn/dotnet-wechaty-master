using System.Threading.Tasks;
using Wechaty.Grpc.PuppetService;
using Wechaty.Module.Puppet.Schemas;
using static Wechaty.Puppet;

namespace Wechaty.GateWay
{
    public class GateWayService : IGateWayService
    {

        protected PuppetClient _grpcClient;

        public GateWayService()
        {
         
        }


        public async Task<PuppetClient> StartAsync(PuppetOptions option)
        {
            var instace = WechatyGrpcFactory.GetGrpcClientInstace(option.Name);
            return await instace.StartAsync(option);
        }

        public async Task StopAsync()
        {
            var instace = WechatyGrpcFactory.GetGrpcClientInstace(Constant.GrpcInstaceName);
            await instace.StopAsync();
        }

    }
}

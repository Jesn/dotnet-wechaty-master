using System.Threading.Tasks;
using Wechaty.Grpc.PuppetService;
using Wechaty.Module.Puppet.Schemas;
using static Wechaty.Puppet;

namespace Wechaty.GateWay
{
    public interface IGateWayService: IApplicationService
    {
        Task<PuppetClient> StartAsync(PuppetOptions option);

        Task StopAsync();
    }
}

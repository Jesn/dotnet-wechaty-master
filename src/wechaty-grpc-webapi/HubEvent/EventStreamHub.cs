using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Wechaty.Grpc.PuppetService;

namespace wechaty_grpc_webapi
{
    public class EventStreamHub : Hub
    {
        private readonly IWechatyPuppetService _wechatyPuppetService;

        //public EventStreamHub(IWechatyPuppetService wechatyPuppetService)
        //{
        //    //_wechatyPuppetService = wechatyPuppetService;
        //}

        public override Task OnConnectedAsync() => EventStream();

        public async Task EventStream()
        {
            //var eventStream = _wechatyPuppetService.EventStream();

            await Clients.All.SendAsync("event", "eventStream");
        }
    }
}

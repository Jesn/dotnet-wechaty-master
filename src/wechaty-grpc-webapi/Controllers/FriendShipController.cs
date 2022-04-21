using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wechaty.Grpc.PuppetService.FriendShip;

namespace wechaty_grpc_webapi.Controllers
{
    public class FriendShipController : WechatyApiController
    {
        private readonly IFriendShipService _friendShipService;
        public FriendShipController(IFriendShipService friendShipService) => _friendShipService = friendShipService;

        [HttpPut]
        public async Task<ActionResult> FriendshipAccept(string friendshipId)
        {
            await _friendShipService.FriendshipAccept(friendshipId);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> FriendshipAdd(string contactId, string? hello)
        {
            await _friendShipService.FriendshipAdd(contactId, hello);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> FriendshipSearchPhone(string phone)
        {
            var response = await _friendShipService.FriendshipSearchPhone(phone);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> FriendshipSearchWeixin(string weixin)
        {
            var response = await _friendShipService.FriendshipSearchWeixin(weixin);
            return Ok(response);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wechaty.Grpc.PuppetService.Room;

namespace wechaty_grpc_webapi.Controllers
{

    public class RoomController : WechatyApiController
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService) => _roomService = roomService;


        [HttpPut]
        public async Task<ActionResult> RoomAdd(string roomId, string contactId)
        {
            await _roomService.RoomAdd(roomId, contactId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> RoomAnnounce(string roomId)
        {
            var response = await _roomService.RoomAnnounce(roomId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> RoomAnnounce(string roomId, string text)
        {
            await _roomService.RoomAnnounce(roomId, text);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> RoomAvatar(string roomId)
        {
            var response = await _roomService.RoomAvatar(roomId);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> RoomCreate(IEnumerable<string> contactIdList, string? topic)
        {
            var response = await _roomService.RoomCreate(contactIdList, topic);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> RoomDel(string roomId, string contactId)
        {
            await _roomService.RoomDel(roomId, contactId);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> RoomInvitationAccept(string roomInvitationId)
        {
            await _roomService.RoomInvitationAccept(roomInvitationId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> RoomList()
        {
            var roomList = await _roomService.RoomList();
            return Ok(roomList);
        }

        [HttpGet]
        public async Task<ActionResult> RoomMemberList(string roomId)
        {
            var response = await _roomService.RoomMemberList(roomId);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> RoomQRCode(string roomId)
        {
            var response=await _roomService.RoomQRCode(roomId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> RoomQuit(string roomId)
        {
            await _roomService.RoomQuit(roomId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> RoomTopic(string roomId)
        {
            var response=await _roomService.RoomTopic(roomId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> RoomTopic(string roomId, string topic)
        {
            await _roomService.RoomTopic(roomId, topic);
            return Ok();
        }
        
    }
}

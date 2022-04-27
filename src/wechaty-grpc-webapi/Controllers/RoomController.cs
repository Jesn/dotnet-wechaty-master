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

        [HttpGet]
        public async Task<ActionResult> GetRoomPayload(string roomId)
        {
            var response=await _roomService.RoomPayloadAsync(roomId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> RoomAdd(string roomId, string contactId)
        {
            await _roomService.RoomAddAsync(roomId, contactId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> RoomAnnounce(string roomId)
        {
            var response = await _roomService.RoomAnnounceAsync(roomId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> RoomAnnounce(string roomId, string text)
        {
            await _roomService.RoomAnnounceAsync(roomId, text);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> RoomAvatar(string roomId)
        {
            var response = await _roomService.RoomAvatarAsync(roomId);
            var jsonObj= response.ToJson();
            
            return Ok(jsonObj);
        }

        [HttpPost]
        public async Task<ActionResult> RoomCreate(IEnumerable<string> contactIdList, string? topic)
        {
            var response = await _roomService.RoomCreateAsync(contactIdList, topic);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> RoomDel(string roomId, string contactId)
        {
            await _roomService.RoomDelAsync(roomId, contactId);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> RoomInvitationAccept(string roomInvitationId)
        {
            await _roomService.RoomInvitationAcceptAsync(roomInvitationId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> RoomList()
        {
            var roomList = await _roomService.RoomListAsync();
            return Ok(roomList);
        }

        [HttpGet]
        public async Task<ActionResult> RoomMemberList(string roomId)
        {
            var response = await _roomService.RoomMemberListAsync(roomId);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> RoomQRCode(string roomId)
        {
            var response=await _roomService.RoomQRCodeAsync(roomId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> RoomQuit(string roomId)
        {
            await _roomService.RoomQuitAsync(roomId);
            return Ok();
        }

        // 该函数也是更新操作，所以注释了，如果想获取群消息可以通过GetRoomPayload这个接口
        //[HttpGet]
        //public async Task<ActionResult> RoomTopic(string roomId)
        //{
        //    var response=await _roomService.RoomTopicAsync(roomId);
        //    return Ok(response);
        //}

        [HttpPut]
        public async Task<ActionResult> RoomTopic(string roomId, string topic)
        {
            await _roomService.RoomTopicAsync(roomId, topic);
            return Ok();
        }
        
    }
}

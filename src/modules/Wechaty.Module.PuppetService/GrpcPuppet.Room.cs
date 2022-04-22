using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Wechaty.Module.Filebox;

namespace Wechaty.Module.PuppetService
{
    public partial class GrpcPuppet
    {
        #region Room

        public override async Task RoomAdd(string roomId, string contactId)
        {
            await _roomService.RoomAddAsync(roomId, contactId);
        }

        public override async Task<string> RoomAnnounce(string roomId)
        {

            var response = await _roomService.RoomAnnounceAsync(roomId);
            return response;
        }

        public override async Task RoomAnnounce(string roomId, string text)
        {
            await _roomService.RoomAnnounceAsync(roomId, text);
        }

        public override async Task<FileBox> RoomAvatar(string roomId)
        {
            var response = await _roomService.RoomAvatarAsync(roomId);
            return response;
        }

        // TODO 可以合并为一个接口
        public override async Task<string> RoomCreate(IEnumerable<string> contactIdList, string? topic)
        {
            var response = await _roomService.RoomCreateAsync(contactIdList, topic);
            return response;
        }

        public override async Task<string> RoomCreate(string[] contactIdList, string? topic)
        {
            var response = await _roomService.RoomCreateAsync(contactIdList, topic);
            return response;
        }

        public override async Task RoomDel(string roomId, string contactId)
        {
            await _roomService.RoomDelAsync(roomId, contactId);
        }

        public override async Task RoomInvitationAccept(string roomInvitationId)
        {
            await _roomService.RoomInvitationAcceptAsync(roomInvitationId);
        }

        public override async Task<IReadOnlyList<string>> RoomList()
        {
            var response = await _roomService.RoomListAsync();
            return response;
        }

        public override async Task<string[]> RoomMemberList(string roomId)
        {
            var response = await _roomService.RoomMemberListAsync(roomId);
            return response;
        }

        public override async Task<string> RoomQRCode(string roomId)
        {
            var response = await _roomService.RoomQRCodeAsync(roomId);
            return response;
        }

        public override async Task RoomQuit(string roomId)
        {
            await _roomService.RoomQuitAsync(roomId);
        }

        public override async Task<string> RoomTopic(string roomId)
        {
            var response = await _roomService.RoomTopicAsync(roomId);
            return response;
        }

        // TODO  待确定
        public override async Task RoomTopic(string roomId, string topic)
        {
            await _roomService.RoomTopicAsync(roomId, topic);
            //return response?.Topic;
        }
        #endregion
    }
}

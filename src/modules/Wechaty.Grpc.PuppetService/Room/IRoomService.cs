using System.Collections.Generic;
using System.Threading.Tasks;
using Wechaty.Module.Filebox;
using Wechaty.Module.Puppet.Schemas;

namespace Wechaty.Grpc.PuppetService.Room
{
    public interface IRoomService : IApplicationService
    {
        Task<RoomPayload> RoomPayloadAsync(string roomId);

        Task RoomAddAsync(string roomId, string contactId);
        Task<string> RoomAnnounceAsync(string roomId);
        Task RoomAnnounceAsync(string roomId, string text);
        Task<FileBox> RoomAvatarAsync(string roomId);
        Task<string> RoomCreateAsync(IEnumerable<string> contactIdList, string? topic);
        Task RoomDelAsync(string roomId, string contactId);
        Task RoomInvitationAcceptAsync(string roomInvitationId);
        Task<RoomInvitationPayload> RoomInvitationPayloadAsync(string roomInvitationId);
        Task<IReadOnlyList<string>> RoomListAsync();
        Task<string[]> RoomMemberListAsync(string roomId);
        Task<RoomMemberPayload> RoomMemberPayloadAsync(string roomId, string contactId);
        Task<string> RoomQRCodeAsync(string roomId);
        Task RoomQuitAsync(string roomId);
        Task<string> RoomTopicAsync(string roomId);
        Task RoomTopicAsync(string roomId, string topic);

    }
}

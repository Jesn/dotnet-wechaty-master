using System.Collections.Generic;
using System.Threading.Tasks;
using Wechaty.Module.Filebox;

namespace Wechaty.Grpc.PuppetService.Room
{
    public interface IRoomService : IApplicationService
    {
        Task RoomAdd(string roomId, string contactId);
        Task<string> RoomAnnounce(string roomId);
        Task RoomAnnounce(string roomId, string text);
        Task<FileBox> RoomAvatar(string roomId);
        Task<string> RoomCreate(IEnumerable<string> contactIdList, string? topic);
        Task RoomDel(string roomId, string contactId);
        Task RoomInvitationAccept(string roomInvitationId);
        Task<IReadOnlyList<string>> RoomList();
        Task<string[]> RoomMemberList(string roomId);
        Task<string> RoomQRCode(string roomId);
        Task RoomQuit(string roomId);
        Task<string> RoomTopic(string roomId);
        Task RoomTopic(string roomId, string topic);

    }
}

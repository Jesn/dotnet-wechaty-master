using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Wechaty.Module.Puppet.Schemas;

namespace Wechaty.Module.PuppetService
{
    public partial class GrpcPuppet
    {
        #region RawPayload
        protected override async Task<ContactPayload> ContactRawPayload(string contactId)
        {
            var payload = await _contactService.ContactPayloadAsync(contactId);
            return payload;
        }

        protected override async Task<ContactPayload> ContactRawPayloadParser(ContactPayload rawPayload)
        {
            return rawPayload;
        }

        protected override async Task<FriendshipPayload> FriendshipRawPayload(string friendshipId)
        {
            var payload = await _friendShipService.FriendshipPayloadAsync(friendshipId);
            return payload;
        }

        protected override async Task<FriendshipPayload> FriendshipRawPayloadParser(FriendshipPayload rawPayload)
        {
            return _ = rawPayload;
        }

        protected override async Task<MessagePayload> MessageRawPayload(string messageId)
        {
            var payload = await _messageService.MessagePayloadAsync(messageId);
            return payload;
        }

        protected override MessagePayload MessageRawPayloadParser(MessagePayload rawPayload)
        {
            return rawPayload;
        }

        protected override async Task<RoomInvitationPayload> RoomInvitationRawPayload(string roomInvitationId)
        {

            var payload = await _roomService.RoomInvitationPayloadAsync(roomInvitationId);
            return payload;
        }

        protected override async Task<RoomInvitationPayload> RoomInvitationRawPayloadParser(RoomInvitationPayload rawPayload)
        {
            return _ = rawPayload;
        }

        protected override async Task<RoomMemberPayload> RoomMemberRawPayload(string roomId, string contactId)
        {
            var payload = await _roomService.RoomMemberPayloadAsync(roomId, contactId);

            return payload;
        }

        protected override async Task<RoomMemberPayload> RoomMemberRawPayloadParser(RoomMemberPayload rawPayload)
        {
            return _ = rawPayload;
        }

        protected override async Task<RoomPayload> RoomRawPayload(string roomId)
        {
            var roomPayload = await _roomService.RoomPayloadAsync(roomId);
            return roomPayload;
        }

        protected override async Task<RoomPayload> RoomRawPayloadParser(RoomPayload rawPayload)
        {
            return _ = rawPayload;
        }


        #endregion
    }
}

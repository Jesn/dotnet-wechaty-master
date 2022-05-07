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
            var payload = await _grpcClient.ContactPayloadAsync(contactId);
            return payload;
        }

        protected override async Task<ContactPayload> ContactRawPayloadParser(ContactPayload rawPayload) => rawPayload;
      

        protected override async Task<FriendshipPayload> FriendshipRawPayload(string friendshipId)
        {
            var payload = await _grpcClient.FriendshipPayloadAsync(friendshipId);
            return payload;
        }

        protected override async Task<FriendshipPayload> FriendshipRawPayloadParser(FriendshipPayload rawPayload) => rawPayload;

        protected override async Task<MessagePayload> MessageRawPayload(string messageId)
        {
            var payload = await _grpcClient.MessagePayloadAsync(messageId);
            return payload;
        }

        protected override MessagePayload MessageRawPayloadParser(MessagePayload rawPayload) => rawPayload;


        protected override async Task<RoomInvitationPayload> RoomInvitationRawPayload(string roomInvitationId)
        {

            var payload = await _grpcClient.RoomInvitationPayloadAsync(roomInvitationId);
            return payload;
        }

        protected override async Task<RoomInvitationPayload> RoomInvitationRawPayloadParser(RoomInvitationPayload rawPayload) => rawPayload;


        protected override async Task<RoomMemberPayload> RoomMemberRawPayload(string roomId, string contactId)
        {
            var payload = await _grpcClient.RoomMemberPayloadAsync(roomId, contactId);

            return payload;
        }

        protected override async Task<RoomMemberPayload> RoomMemberRawPayloadParser(RoomMemberPayload rawPayload) => _ = rawPayload;

        protected override async Task<RoomPayload> RoomRawPayload(string roomId)
        {
            var roomPayload = await _grpcClient.RoomPayloadAsync(roomId);
            return roomPayload;
        }

        protected override async Task<RoomPayload> RoomRawPayloadParser(RoomPayload rawPayload) => rawPayload;
       


        #endregion
    }
}

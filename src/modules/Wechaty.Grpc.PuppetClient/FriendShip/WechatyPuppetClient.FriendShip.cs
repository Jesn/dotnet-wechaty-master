using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Wechaty.Module.Puppet.Schemas;

namespace Wechaty.Grpc.Client
{
    public partial class WechatyPuppetClient
    {
        #region Friendship

        public async Task<FriendshipPayload> FriendshipPayloadAsync(string friendshipId)
        {
            var payload = new FriendshipPayload();

            var request = new FriendshipPayloadRequest()
            {
                Id = friendshipId
            };

            var response = await _grpcClient.FriendshipPayloadAsync(request);
            if (response != null)
            {
                payload = new FriendshipPayload()
                {
                    ContactId = response.ContactId,
                    Hello = response.Hello,
                    Id = response.Id,
                    Scene = (int)response.Scene,
                    Stranger = response.Stranger,
                    Ticket = response.Ticket,
                    Type = (Module.Puppet.Schemas.FriendshipType)response.Type
                };
            }
            return payload;
        }

        public async Task FriendshipAcceptAsync(string friendshipId)
        {
            var request = new FriendshipAcceptRequest()
            { Id = friendshipId };

            await _grpcClient.FriendshipAcceptAsync(request);
        }

        public async Task FriendshipAddAsync(string contactId, string? hello)
        {
            var request = new FriendshipAddRequest()
            {
                ContactId = contactId,
                Hello = hello,
            };

            var response = await _grpcClient.FriendshipAddAsync(request);
        }

        public async Task<string?> FriendshipSearchPhoneAsync(string phone)
        {
            var request = new FriendshipSearchPhoneRequest()
            {
                Phone = phone
            };

            var response = await _grpcClient.FriendshipSearchPhoneAsync(request);
            return response?.ContactId;
        }

        public async Task<string?> FriendshipSearchWeixinAsync(string weixin)
        {
            var request = new FriendshipSearchHandleRequest()
            { Weixin = weixin };

            var respnse = await _grpcClient.FriendshipSearchWeixinAsync(request);
            return respnse?.ContactId;
        }
        #endregion
    }
}

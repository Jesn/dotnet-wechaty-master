using System.Threading.Tasks;
using github.wechaty.grpc.puppet;

namespace Wechaty.Grpc.PuppetService.FriendShip
{
    public class FriendShipService : WechatyPuppetService, IFriendShipService
    {
        #region Friendship

        public async Task FriendshipAccept(string friendshipId)
        {
            var request = new FriendshipAcceptRequest()
            { Id = friendshipId };

            await _grpcClient.FriendshipAcceptAsync(request);
        }

        public async Task FriendshipAdd(string contactId, string? hello)
        {
            var request = new FriendshipAddRequest()
            {
                ContactId = contactId,
                Hello = hello
            };
            var response = await _grpcClient.FriendshipAddAsync(request);
        }

        public async Task<string?> FriendshipSearchPhone(string phone)
        {
            var request = new FriendshipSearchPhoneRequest()
            {
                Phone = phone
            };

            var response = await _grpcClient.FriendshipSearchPhoneAsync(request);
            return response?.ContactId;
        }

        public async Task<string?> FriendshipSearchWeixin(string weixin)
        {
            var request = new FriendshipSearchHandleRequest()
            { Weixin = weixin };

            var respnse = await _grpcClient.FriendshipSearchWeixinAsync(request);
            return respnse?.ContactId;
        }
        #endregion
    }
}

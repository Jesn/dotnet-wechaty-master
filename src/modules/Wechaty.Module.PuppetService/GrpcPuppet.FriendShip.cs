using System.Threading.Tasks;
using github.wechaty.grpc.puppet;

namespace Wechaty.Module.PuppetService
{
    public partial class GrpcPuppet
    {
        #region Friendship

        public override async Task FriendshipAccept(string friendshipId)
        {
            await _friendShipService.FriendshipAcceptAsync(friendshipId);
        }

        public override async Task FriendshipAdd(string contactId, string? hello)
        {
            await _friendShipService.FriendshipAddAsync(contactId, hello);
        }

        public override async Task<string?> FriendshipSearchPhone(string phone)
        {
            var response = await _friendShipService.FriendshipSearchPhoneAsync(phone);
            return response;
        }

        public override async Task<string?> FriendshipSearchWeixin(string weixin)
        {
            var respnse = await _friendShipService.FriendshipSearchWeixinAsync(weixin);
            return respnse;
        }
        #endregion
    }
}

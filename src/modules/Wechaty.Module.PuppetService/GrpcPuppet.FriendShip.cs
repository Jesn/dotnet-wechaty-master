using System.Threading.Tasks;
using github.wechaty.grpc.puppet;

namespace Wechaty.Module.PuppetService
{
    public partial class GrpcPuppet
    {
        #region Friendship

        public override async Task FriendshipAccept(string friendshipId) => await _grpcClient.FriendshipAcceptAsync(friendshipId);

        public override async Task FriendshipAdd(string contactId, string? hello) => await _grpcClient.FriendshipAddAsync(contactId, hello);

        public override async Task<string?> FriendshipSearchPhone(string phone)
        {
            var response = await _grpcClient.FriendshipSearchPhoneAsync(phone);
            return response;
        }

        public override async Task<string?> FriendshipSearchWeixin(string weixin)
        {
            var respnse = await _grpcClient.FriendshipSearchWeixinAsync(weixin);
            return respnse;
        }
        #endregion
    }
}

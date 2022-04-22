using System.Threading.Tasks;
using Wechaty.Module.Puppet.Schemas;

namespace Wechaty.Grpc.PuppetService.FriendShip
{
    public interface IFriendShipService : IApplicationService
    {
        Task<FriendshipPayload> FriendshipPayloadAsync(string friendshipId);
        Task FriendshipAcceptAsync(string friendshipId);
        Task FriendshipAddAsync(string contactId, string? hello);
        Task<string?> FriendshipSearchPhoneAsync(string phone);
        Task<string?> FriendshipSearchWeixinAsync(string weixin);

    }
}

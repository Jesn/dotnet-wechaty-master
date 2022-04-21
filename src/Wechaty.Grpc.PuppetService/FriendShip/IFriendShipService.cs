using System.Threading.Tasks;

namespace Wechaty.Grpc.PuppetService.FriendShip
{
    public interface IFriendShipService : IApplicationService
    {
        Task FriendshipAccept(string friendshipId);
        Task FriendshipAdd(string contactId, string? hello);
        Task<string?> FriendshipSearchPhone(string phone);
        Task<string?> FriendshipSearchWeixin(string weixin);

    }
}

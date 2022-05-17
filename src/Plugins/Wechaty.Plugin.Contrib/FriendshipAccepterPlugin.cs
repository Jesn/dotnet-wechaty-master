using System.Threading.Tasks;
using Wechaty.Module.Schemas;
using Wechaty.User;

namespace Wechaty.Plugin
{
    public class FriendshipAccepterPlugin : IWechatPlugin
    {
        public string Name => "FriendshipAccepter Plugin";

        public string Description => "Accept friendship automatically, and say/do something for greeting.";

        public string Version => "V1.0.0";


        private FriendshipAccepterConfig config;

        public FriendshipAccepterPlugin()
        {

        }

        public FriendshipAccepterPlugin(FriendshipAccepterConfig _config)
        {
            config = _config;
        }

        public Task Install(Wechaty bot)
        {
            bot.OnFriendship(async (Friendship friendship) =>
            {
                var friendshipType = friendship.Type;
                switch (friendshipType)
                {
                    case FriendshipType.Confirm:
                        var contact = friendship.Contact;
                        await contact.Say(config.Greeting);
                        break;
                    case FriendshipType.Receive:
                        var hello = friendship.Hello;
                        if (hello.Contains(config.Greeting))
                        {
                            await friendship.Accept();
                        }
                        break;
                    case FriendshipType.Unknown:
                        break;
                    case FriendshipType.Verify:
                        break;
                    default:
                        break;
                }
            });

            return Task.CompletedTask;
        }
    }

    public class FriendshipAccepterConfig
    {
        public string Greeting { get; set; }
        public string KeyWord { get; set; }
    }
}

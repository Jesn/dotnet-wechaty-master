using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wechaty.Module.Filebox;
using Wechaty.Module.Puppet.Schemas;

namespace Wechaty.Grpc.PuppetService.Message
{
    public interface IMessageService : IApplicationService
    {
        Task<MessagePayload> MessagePayloadAsync(string messageId);
        Task<string> MessageContactAsync(string messageId);
        Task<FileBox> MessageFileAsync(string messageId);
        Task<FileBox> MessageImageAsync(string messageId,ImageType imageType);
        Task<byte[]> MessageImageStreamAsync(string messageId, ImageType imageType, CancellationToken cancellationToken = default);
        Task<MiniProgramPayload> MessageMiniProgramAsync(string messageId);
        Task<bool> MessageRecallAsync(string messageId);
        Task<string?> MessageSendContactAsync(string conversationId, string contactId);
        Task<string?> MessageSendFileAsync(string conversationId, FileBox file);
        Task<string?> MessageSendMiniProgramAsync(string conversationId, MiniProgramPayload miniProgramPayload);
        Task<string?> MessageSendTextAsync(string conversationId, string text, params string[]? mentionIdList);
        Task<string?> MessageSendTextAsync(string conversationId, string text, IEnumerable<string>? mentionIdList);
        Task<string?> MessageSendUrlAsync(string conversationId, UrlLinkPayload urlLinkPayload);
        Task<UrlLinkPayload> MessageUrlAsync(string messageId);

    }
}

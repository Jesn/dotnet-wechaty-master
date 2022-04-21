using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wechaty.Module.Filebox;
using Wechaty.Module.Puppet.Schemas;

namespace Wechaty.Grpc.PuppetService.Message
{
    public interface IMessageService : IApplicationService
    {
        Task<string> MessageContact(string messageId);
        Task<FileBox> MessageFile(string messageId);
        Task<FileBox> MessageImage(string messageId,ImageType imageType);
        Task<byte[]> MessageImageStream(string messageId, ImageType imageType, CancellationToken cancellationToken = default);
        Task<MiniProgramPayload> MessageMiniProgram(string messageId);
        Task<bool> MessageRecall(string messageId);
        Task<string?> MessageSendContact(string conversationId, string contactId);
        Task<string?> MessageSendFile(string conversationId, FileBox file);
        Task<string?> MessageSendMiniProgram(string conversationId, MiniProgramPayload miniProgramPayload);
        Task<string?> MessageSendText(string conversationId, string text, params string[]? mentionIdList);
        Task<string?> MessageSendText(string conversationId, string text, IEnumerable<string>? mentionIdList);
        Task<string?> MessageSendUrl(string conversationId, UrlLinkPayload urlLinkPayload);
        Task<UrlLinkPayload> MessageUrl(string messageId);

    }
}

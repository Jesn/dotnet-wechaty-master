using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Newtonsoft.Json;
using Wechaty.Module.Filebox;

namespace Wechaty.Module.PuppetService
{
    public partial class GrpcPuppet
    {
        #region Message
        public override async Task<string> MessageContact(string messageId)
        {
            var response = await _messageService.MessageContactAsync(messageId);
            return response;
        }

        public override async Task<FileBox> MessageFile(string messageId)
        {
            var response = await _messageService.MessageFileAsync(messageId);
            return response;

        }

        public override async Task<FileBox> MessageImage(string messageId, Puppet.Schemas.ImageType imageType)
        {
            var response = await _messageService.MessageImageAsync(messageId, imageType);
            return response;
        }



        public override async Task<byte[]> MessageImageStream(string messageId, Puppet.Schemas.ImageType imageType, CancellationToken cancellationToken = default)
        {
            var response = await _messageService.MessageImageStreamAsync(messageId, imageType, cancellationToken);
            return response;
        }

        //public override async Task<MiniProgramPayload> MessageMiniProgram(string messageId)
        //{
        //    var request = new MessageMiniProgramRequest
        //    {
        //        Id = messageId
        //    };

        //    var response = await _messageService.MessageMiniProgramAsync(request);
        //    var payload = response.MiniProgram;
        //    return payload;
        //}

        public override async Task<Puppet.Schemas.MiniProgramPayload> MessageMiniProgram(string messageId)
        {
            var response = await _messageService.MessageMiniProgramAsync(messageId);
            return response;
        }

        public override async Task<bool> MessageRecall(string messageId)
        {
            var response = await _messageService.MessageRecallAsync(messageId);
            return response;
        }

        public override async Task<string?> MessageSendContact(string conversationId, string contactId)
        {
            var response = await _messageService.MessageSendContactAsync(conversationId, contactId);
            return response;
        }

        public override async Task<string?> MessageSendFile(string conversationId, FileBox file)
        {
            var response = await _messageService.MessageSendFileAsync(conversationId, file);
            return response;
        }

        //public override async Task<string?> MessageSendMiniProgram(string conversationId, MiniProgramPayload miniProgramPayload)
        //{
        //    var request = new MessageSendMiniProgramRequest
        //    {
        //        ConversationId = conversationId,
        //        MiniProgram = miniProgramPayload
        //    };

        //    var response = await _messageService.MessageSendMiniProgramAsync(request);
        //    return response?.Id;
        //}

        public override async Task<string?> MessageSendMiniProgram(string conversationId, Puppet.Schemas.MiniProgramPayload miniProgramPayload)
        {
            var response = await _messageService.MessageSendMiniProgramAsync(conversationId, miniProgramPayload);
            return response;
        }

        public override async Task<string?> MessageSendText(string conversationId, string text, params string[]? mentionIdList)
        {
            var response = await _messageService.MessageSendTextAsync(conversationId,text,mentionIdList);
            return response;
        }

        public override async Task<string?> MessageSendText(string conversationId, string text, IEnumerable<string>? mentionIdList)
        {
            var response = await _messageService.MessageSendTextAsync(conversationId,text,mentionIdList);
            return response;
        }

        //public override async Task<string?> MessageSendUrl(string conversationId, UrlLinkPayload urlLinkPayload)
        //{
        //    var request = new MessageSendUrlRequest()
        //    {
        //        ConversationId = conversationId,
        //        UrlLink = JsonConvert.SerializeObject(urlLinkPayload)
        //    };

        //    var response = await _messageService.MessageSendUrlAsync(request);
        //    return response?.Id;
        //}

        public override async Task<string?> MessageSendUrl(string conversationId, Puppet.Schemas.UrlLinkPayload urlLinkPayload)
        {
            var response = await _messageService.MessageSendUrlAsync(conversationId,urlLinkPayload);
            return response;
        }

        //public override async Task<UrlLinkPayload> MessageUrl(string messageId)
        //{
        //    var request = new MessageUrlRequest()
        //    {
        //        Id = messageId
        //    };

        //    var response = await _messageService.MessageUrlAsync(request);
        //    var payload = JsonConvert.DeserializeObject<UrlLinkPayload>(response.UrlLink);
        //    return payload;
        //}


        public override async Task<Puppet.Schemas.UrlLinkPayload> MessageUrl(string messageId)
        {
            var response = await _messageService.MessageUrlAsync(messageId);
            return response;
        }

        #endregion
    }
}

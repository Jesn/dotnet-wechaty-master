using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wechaty.Module.Filebox;

namespace Wechaty.Module.PuppetService
{
    public partial class GrpcPuppet
    {
        #region Message
        public override async Task<string> MessageContact(string messageId)
        {
            var response = await _grpcClient.MessageContactAsync(messageId);
            return response;
        }

        public override async Task<FileBox> MessageFile(string messageId)
        {
            var response = await _grpcClient.MessageFileAsync(messageId);
            return response;

        }

        public override async Task<FileBox> MessageImage(string messageId, Schemas.ImageType imageType)
        {
            var response = await _grpcClient.MessageImageAsync(messageId, imageType);
            return response;
        }



        public override async Task<byte[]> MessageImageStream(string messageId, Schemas.ImageType imageType, CancellationToken cancellationToken = default)
        {
            var response = await _grpcClient.MessageImageStreamAsync(messageId, imageType, cancellationToken);
            return response;
        }

        //public override async Task<MiniProgramPayload> MessageMiniProgram(string messageId)
        //{
        //    var request = new MessageMiniProgramRequest
        //    {
        //        Id = messageId
        //    };

        //    var response = await _grpcClient.MessageMiniProgramAsync(request);
        //    var payload = response.MiniProgram;
        //    return payload;
        //}

        public override async Task<Schemas.MiniProgramPayload> MessageMiniProgram(string messageId)
        {
            var response = await _grpcClient.MessageMiniProgramAsync(messageId);
            return response;
        }

        public override async Task<bool> MessageRecall(string messageId)
        {
            var response = await _grpcClient.MessageRecallAsync(messageId);
            return response;
        }

        public override async Task<string?> MessageSendContact(string conversationId, string contactId)
        {
            var response = await _grpcClient.MessageSendContactAsync(conversationId, contactId);
            return response;
        }

        public override async Task<string?> MessageSendFile(string conversationId, FileBox file)
        {
            var response = await _grpcClient.MessageSendFileAsync(conversationId, file);
            return response;
        }

        //public override async Task<string?> MessageSendMiniProgram(string conversationId, MiniProgramPayload miniProgramPayload)
        //{
        //    var request = new MessageSendMiniProgramRequest
        //    {
        //        ConversationId = conversationId,
        //        MiniProgram = miniProgramPayload
        //    };

        //    var response = await _grpcClient.MessageSendMiniProgramAsync(request);
        //    return response?.Id;
        //}

        public override async Task<string?> MessageSendMiniProgram(string conversationId, Schemas.MiniProgramPayload miniProgramPayload)
        {
            var response = await _grpcClient.MessageSendMiniProgramAsync(conversationId, miniProgramPayload);
            return response;
        }

        public override async Task<string?> MessageSendText(string conversationId, string text, params string[]? mentionIdList)
        {
            var response = await _grpcClient.MessageSendTextAsync(conversationId,text,mentionIdList);
            return response;
        }

        public override async Task<string?> MessageSendText(string conversationId, string text, IEnumerable<string>? mentionIdList)
        {
            var response = await _grpcClient.MessageSendTextAsync(conversationId,text,mentionIdList);
            return response;
        }

        //public override async Task<string?> MessageSendUrl(string conversationId, UrlLinkPayload urlLinkPayload)
        //{
        //    var request = new MessageSendUrlRequest()
        //    {
        //        ConversationId = conversationId,
        //        UrlLink = JsonConvert.SerializeObject(urlLinkPayload)
        //    };

        //    var response = await _grpcClient.MessageSendUrlAsync(request);
        //    return response?.Id;
        //}

        public override async Task<string?> MessageSendUrl(string conversationId, Schemas.UrlLinkPayload urlLinkPayload)
        {
            var response = await _grpcClient.MessageSendUrlAsync(conversationId,urlLinkPayload);
            return response;
        }

        //public override async Task<UrlLinkPayload> MessageUrl(string messageId)
        //{
        //    var request = new MessageUrlRequest()
        //    {
        //        Id = messageId
        //    };

        //    var response = await _grpcClient.MessageUrlAsync(request);
        //    var payload = JsonConvert.DeserializeObject<UrlLinkPayload>(response.UrlLink);
        //    return payload;
        //}


        public override async Task<Schemas.UrlLinkPayload> MessageUrl(string messageId)
        {
            var response = await _grpcClient.MessageUrlAsync(messageId);
            return response;
        }

        #endregion
    }
}

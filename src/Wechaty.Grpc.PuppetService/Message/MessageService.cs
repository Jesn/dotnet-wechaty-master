using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Newtonsoft.Json;
using Wechaty.Module.Filebox;

namespace Wechaty.Grpc.PuppetService.Message
{
    public class MessageService:WechatyPuppetService,IMessageService
    {
        #region Message
        public async Task<string> MessageContact(string messageId)
        {
            var request = new MessageContactRequest
            {
                Id = messageId
            };

            var response = await _grpcClient.MessageContactAsync(request);
            return response.Id;
        }

        public async Task<FileBox> MessageFile(string messageId)
        {
            var request = new MessageFileRequest
            {
                Id = messageId
            };

            var response = await _grpcClient.MessageFileAsync(request);
            var filebox = response.FileBox;
            return FileBox.FromJson(filebox);

        }

        public async Task<FileBox> MessageImage(string messageId, Module.Puppet.Schemas.ImageType imageType)
        {
            var request = new MessageImageRequest
            {
                Id = messageId,
                Type = (github.wechaty.grpc.puppet.ImageType)imageType
            };

            var response = await _grpcClient.MessageImageAsync(request);
            var fileBox = response.FileBox;
            return FileBox.FromJson(fileBox);
        }



        public async Task<byte[]> MessageImageStream(string messageId, Module.Puppet.Schemas.ImageType imageType, CancellationToken cancellationToken = default)
        {
            var request = new MessageImageStreamRequest
            {
                Id = messageId,
                Type = (github.wechaty.grpc.puppet.ImageType)imageType
            };

            var response = _grpcClient.MessageImageStream(request);
            var bytes = new List<byte>();
            while (await response.ResponseStream.MoveNext(cancellationToken))
            {
                bytes.AddRange(response.ResponseStream.Current.FileBoxChunk.Data.ToByteArray());
            }

            return bytes.ToArray();
        }

        //public  async Task<MiniProgramPayload> MessageMiniProgram(string messageId)
        //{
        //    var request = new MessageMiniProgramRequest
        //    {
        //        Id = messageId
        //    };

        //    var response = await _grpcClient.MessageMiniProgramAsync(request);
        //    var payload = response.MiniProgram;
        //    return payload;
        //}

        public async Task<Module.Puppet.Schemas.MiniProgramPayload> MessageMiniProgram(string messageId)
        {
            var request = new MessageMiniProgramRequest
            {
                Id = messageId
            };
            var response = await _grpcClient.MessageMiniProgramAsync(request);
            var str = JsonConvert.SerializeObject(response.MiniProgram);
            var payload = JsonConvert.DeserializeObject<Module.Puppet.Schemas.MiniProgramPayload>(str);
            return payload;
        }




        public async Task<bool> MessageRecall(string messageId)
        {
            var request = new MessageRecallRequest
            {
                Id = messageId
            };

            var response = await _grpcClient.MessageRecallAsync(request);
            if (response == null)
            {
                return false;
            }
            return response.Success;
        }

        public async Task<string?> MessageSendContact(string conversationId, string contactId)
        {
            var request = new MessageSendContactRequest()
            {
                ConversationId = conversationId,
                ContactId = contactId
            };

            var response = await _grpcClient.MessageSendContactAsync(request);
            return response?.Id;
        }

        public async Task<string?> MessageSendFile(string conversationId, FileBox file)
        {
            var request = new MessageSendFileRequest
            {
                ConversationId = conversationId,
                FileBox = JsonConvert.SerializeObject(file.ToJson())
            };

            var response = await _grpcClient.MessageSendFileAsync(request);
            return response?.Id;
        }

        //public  async Task<string?> MessageSendMiniProgram(string conversationId, MiniProgramPayload miniProgramPayload)
        //{
        //    var request = new MessageSendMiniProgramRequest
        //    {
        //        ConversationId = conversationId,
        //        MiniProgram = miniProgramPayload
        //    };

        //    var response = await _grpcClient.MessageSendMiniProgramAsync(request);
        //    return response?.Id;
        //}

        public async Task<string?> MessageSendMiniProgram(string conversationId, Module.Puppet.Schemas.MiniProgramPayload miniProgramPayload)
        {
            var str = JsonConvert.SerializeObject(miniProgramPayload);

            var request = new MessageSendMiniProgramRequest
            {
                ConversationId = conversationId,
                MiniProgram = JsonConvert.DeserializeObject<MiniProgramPayload>(str)
            };

            var response = await _grpcClient.MessageSendMiniProgramAsync(request);
            return response?.Id;
        }

        public async Task<string?> MessageSendText(string conversationId, string text, params string[]? mentionIdList)
        {
            var request = new MessageSendTextRequest()
            {
                ConversationId = conversationId,
                Text = text,
                //MentonalIds = mentonalIds
            };

            var response = await _grpcClient.MessageSendTextAsync(request);
            return response?.Id;
        }

        public async Task<string?> MessageSendText(string conversationId, string text, IEnumerable<string>? mentionIdList)
        {
            var request = new MessageSendTextRequest()
            {
                ConversationId = conversationId,
                Text = text,
                //MentonalIds = mentonalIds
            };

            var response = await _grpcClient.MessageSendTextAsync(request);
            return response?.Id;
        }

        //public  async Task<string?> MessageSendUrl(string conversationId, UrlLinkPayload urlLinkPayload)
        //{
        //    var request = new MessageSendUrlRequest()
        //    {
        //        ConversationId = conversationId,
        //        UrlLink = JsonConvert.SerializeObject(urlLinkPayload)
        //    };

        //    var response = await _grpcClient.MessageSendUrlAsync(request);
        //    return response?.Id;
        //}

        public async Task<string?> MessageSendUrl(string conversationId, Module.Puppet.Schemas.UrlLinkPayload urlLinkPayload)
        {

            var str = JsonConvert.SerializeObject(urlLinkPayload);
            var request = new MessageSendUrlRequest()
            {
                ConversationId = conversationId,
                UrlLink = JsonConvert.DeserializeObject<UrlLinkPayload>(str)
            };

            var response = await _grpcClient.MessageSendUrlAsync(request);
            return response?.Id;
        }

        //public  async Task<UrlLinkPayload> MessageUrl(string messageId)
        //{
        //    var request = new MessageUrlRequest()
        //    {
        //        Id = messageId
        //    };

        //    var response = await _grpcClient.MessageUrlAsync(request);
        //    var payload = JsonConvert.DeserializeObject<UrlLinkPayload>(response.UrlLink);
        //    return payload;
        //}


        public async Task<Module.Puppet.Schemas.UrlLinkPayload> MessageUrl(string messageId)
        {
            var request = new MessageUrlRequest()
            {
                Id = messageId
            };
            var response = await _grpcClient.MessageUrlAsync(request);

            var str = JsonConvert.SerializeObject(response.UrlLink);

            return JsonConvert.DeserializeObject<Module.Puppet.Schemas.UrlLinkPayload>(str);

        }

        #endregion
    }
}

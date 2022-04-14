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
            var request = new MessageContactRequest
            {
                Id = messageId
            };

            var response = await grpcClient.MessageContactAsync(request);
            return response.Id;
        }

        public override async Task<FileBox> MessageFile(string messageId)
        {
            var request = new MessageFileRequest
            {
                Id = messageId
            };

            var response = await grpcClient.MessageFileAsync(request);
            var filebox = response.FileBox;
            return FileBox.FromJson(filebox);

        }

        public override async Task<FileBox> MessageImage(string messageId, Puppet.Schemas.ImageType imageType)
        {
            var request = new MessageImageRequest
            {
                Id = messageId,
                Type = (github.wechaty.grpc.puppet.ImageType)imageType
            };

            var response = await grpcClient.MessageImageAsync(request);
            var fileBox = response.FileBox;
            return FileBox.FromJson(fileBox);
        }



        public override async Task<byte[]> MessageImageStream(string messageId, Puppet.Schemas.ImageType imageType, CancellationToken cancellationToken = default)
        {
            var request = new MessageImageStreamRequest
            {
                Id = messageId,
                Type = (github.wechaty.grpc.puppet.ImageType)imageType
            };

            var response = grpcClient.MessageImageStream(request);
            var bytes = new List<byte>();
            while (await response.ResponseStream.MoveNext(cancellationToken))
            {
                bytes.AddRange(response.ResponseStream.Current.FileBoxChunk.Data.ToByteArray());
            }

            return bytes.ToArray();
        }

        //public override async Task<MiniProgramPayload> MessageMiniProgram(string messageId)
        //{
        //    var request = new MessageMiniProgramRequest
        //    {
        //        Id = messageId
        //    };

        //    var response = await grpcClient.MessageMiniProgramAsync(request);
        //    var payload = response.MiniProgram;
        //    return payload;
        //}

        public override async Task<Puppet.Schemas.MiniProgramPayload> MessageMiniProgram(string messageId)
        {
            var request = new MessageMiniProgramRequest
            {
                Id = messageId
            };
            var response = await grpcClient.MessageMiniProgramAsync(request);
            var str = JsonConvert.SerializeObject(response.MiniProgram);
            var payload = JsonConvert.DeserializeObject<Puppet.Schemas.MiniProgramPayload>(str);
            return payload;
        }




        public override async Task<bool> MessageRecall(string messageId)
        {
            var request = new MessageRecallRequest
            {
                Id = messageId
            };

            var response = await grpcClient.MessageRecallAsync(request);
            if (response == null)
            {
                return false;
            }
            return response.Success;
        }

        public override async Task<string?> MessageSendContact(string conversationId, string contactId)
        {
            var request = new MessageSendContactRequest()
            {
                ConversationId = conversationId,
                ContactId = contactId
            };

            var response = await grpcClient.MessageSendContactAsync(request);
            return response?.Id;
        }

        public override async Task<string?> MessageSendFile(string conversationId, FileBox file)
        {
            var request = new MessageSendFileRequest
            {
                ConversationId = conversationId,
                FileBox = JsonConvert.SerializeObject(file.ToJson())
            };

            var response = await grpcClient.MessageSendFileAsync(request);
            return response?.Id;
        }

        //public override async Task<string?> MessageSendMiniProgram(string conversationId, MiniProgramPayload miniProgramPayload)
        //{
        //    var request = new MessageSendMiniProgramRequest
        //    {
        //        ConversationId = conversationId,
        //        MiniProgram = miniProgramPayload
        //    };

        //    var response = await grpcClient.MessageSendMiniProgramAsync(request);
        //    return response?.Id;
        //}

        public override async Task<string?> MessageSendMiniProgram(string conversationId, Puppet.Schemas.MiniProgramPayload miniProgramPayload)
        {
            var str = JsonConvert.SerializeObject(miniProgramPayload);

            var request = new MessageSendMiniProgramRequest
            {
                ConversationId = conversationId,
                MiniProgram = JsonConvert.DeserializeObject<MiniProgramPayload>(str)
            };

            var response = await grpcClient.MessageSendMiniProgramAsync(request);
            return response?.Id;
        }

        public override async Task<string?> MessageSendText(string conversationId, string text, params string[]? mentionIdList)
        {
            var request = new MessageSendTextRequest()
            {
                ConversationId = conversationId,
                Text = text,
                //MentonalIds = mentonalIds
            };

            var response = await grpcClient.MessageSendTextAsync(request);
            return response?.Id;
        }

        public override async Task<string?> MessageSendText(string conversationId, string text, IEnumerable<string>? mentionIdList)
        {
            var request = new MessageSendTextRequest()
            {
                ConversationId = conversationId,
                Text = text,
                //MentonalIds = mentonalIds
            };

            var response = await grpcClient.MessageSendTextAsync(request);
            return response?.Id;
        }

        //public override async Task<string?> MessageSendUrl(string conversationId, UrlLinkPayload urlLinkPayload)
        //{
        //    var request = new MessageSendUrlRequest()
        //    {
        //        ConversationId = conversationId,
        //        UrlLink = JsonConvert.SerializeObject(urlLinkPayload)
        //    };

        //    var response = await grpcClient.MessageSendUrlAsync(request);
        //    return response?.Id;
        //}

        public override async Task<string?> MessageSendUrl(string conversationId, Puppet.Schemas.UrlLinkPayload urlLinkPayload)
        {

            var str = JsonConvert.SerializeObject(urlLinkPayload);
            var request = new MessageSendUrlRequest()
            {
                ConversationId = conversationId,
                UrlLink = JsonConvert.DeserializeObject<UrlLinkPayload>(str)
            };

            var response = await grpcClient.MessageSendUrlAsync(request);
            return response?.Id;
        }

        //public override async Task<UrlLinkPayload> MessageUrl(string messageId)
        //{
        //    var request = new MessageUrlRequest()
        //    {
        //        Id = messageId
        //    };

        //    var response = await grpcClient.MessageUrlAsync(request);
        //    var payload = JsonConvert.DeserializeObject<UrlLinkPayload>(response.UrlLink);
        //    return payload;
        //}


        public override async Task<Puppet.Schemas.UrlLinkPayload> MessageUrl(string messageId)
        {
            var request = new MessageUrlRequest()
            {
                Id = messageId
            };
            var response = await grpcClient.MessageUrlAsync(request);

            var str = JsonConvert.SerializeObject(response.UrlLink);

            return JsonConvert.DeserializeObject<Puppet.Schemas.UrlLinkPayload>(str);

        }

        #endregion
    }
}

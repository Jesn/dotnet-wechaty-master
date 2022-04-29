using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Newtonsoft.Json;
using Wechaty.Module.Filebox;
using Wechaty.Module.Puppet.Schemas;

namespace Wechaty.Grpc.Client
{
    public partial class WechatyPuppetClient
    {
        #region Message
        public async Task<MessagePayload> MessagePayloadAsync(string messageId)
        {
            MessagePayload payload = new MessagePayload();

            var request = new MessagePayloadRequest()
            {
                Id = messageId
            };
            var response = await _grpcClient.MessagePayloadAsync(request);

            if (response != null)
            {
                payload = new MessagePayload()
                {
                    Id = messageId,
                    Filename = response.Filename,
                    FromId = response.TalkerId,
                    Text = response.Text,
                    MentionIdList = response.MentionIds.ToList(),
                    RoomId = response.RoomId,
                    Timestamp = (long)response.TimestampDeprecated,
                    Type = (Module.Puppet.Schemas.MessageType)response.Type,
                    ToId = response.ListenerId
                };
            }
            return payload;
        }

        public async Task<string> MessageContactAsync(string messageId)
        {
            var request = new MessageContactRequest
            {
                Id = messageId
            };

            var response = await _grpcClient.MessageContactAsync(request);
            return response.Id;
        }

        public async Task<FileBox> MessageFileAsync(string messageId)
        {
            var request = new MessageFileRequest
            {
                Id = messageId
            };

            var response = await _grpcClient.MessageFileAsync(request);
            var filebox = response.FileBox;
            return FileBox.FromJson(filebox);

        }

        public async Task<FileBox> MessageImageAsync(string messageId, Module.Puppet.Schemas.ImageType imageType)
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



        public async Task<byte[]> MessageImageStreamAsync(string messageId, Module.Puppet.Schemas.ImageType imageType, CancellationToken cancellationToken = default)
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

        public async Task<Module.Puppet.Schemas.MiniProgramPayload> MessageMiniProgramAsync(string messageId)
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




        public async Task<bool> MessageRecallAsync(string messageId)
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

        public async Task<string?> MessageSendContactAsync(string conversationId, string contactId)
        {
            var request = new MessageSendContactRequest()
            {
                ConversationId = conversationId,
                ContactId = contactId
            };

            var response = await _grpcClient.MessageSendContactAsync(request);
            return response?.Id;
        }

        public async Task<string?> MessageSendFileAsync(string conversationId, FileBox file)
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

        public async Task<string?> MessageSendMiniProgramAsync(string conversationId, Module.Puppet.Schemas.MiniProgramPayload miniProgramPayload)
        {
            var str = JsonConvert.SerializeObject(miniProgramPayload);

            var request = new MessageSendMiniProgramRequest
            {
                ConversationId = conversationId,
                MiniProgram = JsonConvert.DeserializeObject<github.wechaty.grpc.puppet.MiniProgramPayload>(str)
            };

            var response = await _grpcClient.MessageSendMiniProgramAsync(request);
            return response?.Id;
        }

        public async Task<string?> MessageSendTextAsync(string conversationId, string text, params string[]? mentionIdList)
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

        public async Task<string?> MessageSendTextAsync(string conversationId, string text, IEnumerable<string>? mentionIdList)
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

        public async Task<string?> MessageSendUrlAsync(string conversationId, Module.Puppet.Schemas.UrlLinkPayload urlLinkPayload)
        {

            var str = JsonConvert.SerializeObject(urlLinkPayload);
            var request = new MessageSendUrlRequest()
            {
                ConversationId = conversationId,
                UrlLink = JsonConvert.DeserializeObject<github.wechaty.grpc.puppet.UrlLinkPayload>(str)
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


        public async Task<Module.Puppet.Schemas.UrlLinkPayload> MessageUrlAsync(string messageId)
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

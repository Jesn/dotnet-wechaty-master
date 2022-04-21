using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Wechaty.Module.Filebox;

namespace Wechaty.Grpc.PuppetService.Room
{
    public class RoomService : WechatyPuppetService, IRoomService
    {
        #region Room

        public async Task RoomAdd(string roomId, string contactId)
        {
            var request = new RoomAddRequest()
            {
                ContactId = contactId,
                Id = roomId
            };
            await _grpcClient.RoomAddAsync(request);
        }

        public async Task<string> RoomAnnounce(string roomId)
        {
            var request = new RoomAnnounceRequest
            {
                Id = roomId
            };


            var response = await _grpcClient.RoomAnnounceAsync(request);
            return response?.Text;
        }

        public async Task RoomAnnounce(string roomId, string text)
        {
            var request = new RoomAnnounceRequest
            {
                Id = roomId,
                Text = text
            };

            await _grpcClient.RoomAnnounceAsync(request);

        }

        public async Task<FileBox> RoomAvatar(string roomId)
        {
            var request = new RoomAvatarRequest()
            { Id = roomId };

            var response = await _grpcClient.RoomAvatarAsync(request);

            return FileBox.FromJson(response.FileBox);
        }

        // TODO 可以合并为一个接口
        public async Task<string> RoomCreate(IEnumerable<string> contactIdList, string? topic)
        {
            var request = new RoomCreateRequest();

            request.ContactIds.AddRange(contactIdList);
            request.Topic = topic;

            var response = await _grpcClient.RoomCreateAsync(request);
            return response?.Id;
        }

        //public async Task<string> RoomCreate(string[] contactIdList, string? topic)
        //{
        //    var request = new RoomCreateRequest();

        //    request.ContactIds.AddRange(contactIdList);
        //    if (topic != "")
        //    {
        //        request.Topic = topic;
        //    }

        //    var response = await _grpcClient.RoomCreateAsync(request);
        //    return response?.Id;
        //}

        public async Task RoomDel(string roomId, string contactId)
        {
            var request = new RoomDelRequest()
            {
                ContactId = contactId,
                Id = roomId
            };
            await _grpcClient.RoomDelAsync(request);
        }

        public async Task RoomInvitationAccept(string roomInvitationId)
        {
            var request = new RoomInvitationAcceptRequest()
            {
                Id = roomInvitationId
            };
            await _grpcClient.RoomInvitationAcceptAsync(request);
        }

        public async Task<IReadOnlyList<string>> RoomList()
        {
            var response = await _grpcClient.RoomListAsync(new RoomListRequest());
            return response?.Ids.ToList();
        }

        public async Task<string[]> RoomMemberList(string roomId)
        {
            var request = new RoomMemberListRequest()
            {
                Id = roomId
            };

            var response = await _grpcClient.RoomMemberListAsync(request);
            return response?.MemberIds.ToArray();
        }

        public async Task<string> RoomQRCode(string roomId)
        {
            var request = new RoomQRCodeRequest()
            {
                Id = roomId
            };
            var response = await _grpcClient.RoomQRCodeAsync(request);
            return response?.Qrcode;
        }

        public async Task RoomQuit(string roomId)
        {
            var request = new RoomQuitRequest()
            {
                Id = roomId
            };
            await _grpcClient.RoomQuitAsync(request);
        }

        public async Task<string> RoomTopic(string roomId)
        {
            var request = new RoomTopicRequest()
            {
                Id = roomId
            };
            var response = await _grpcClient.RoomTopicAsync(request);
            return response?.Topic;
        }

        // TODO  待确定
        public async Task RoomTopic(string roomId, string topic)
        {
            var request = new RoomTopicRequest()
            {
                Id = roomId
            };
            if (!string.IsNullOrEmpty(topic))
            {
                request.Topic = topic;
            }

            var response = await _grpcClient.RoomTopicAsync(request);
            //return response?.Topic;
        }
        #endregion
    }
}

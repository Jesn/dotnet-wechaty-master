using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Wechaty.Module.Filebox;
using Wechaty.Module.Puppet.Schemas;

namespace Wechaty.Grpc.Client
{
    public partial class WechatyPuppetClient
    {
        #region Room

        public async Task<RoomPayload> RoomPayloadAsync(string roomId)
        {
            var roomPayload = new RoomPayload();

            var request = new RoomPayloadRequest()
            {
                Id = roomId
            };

            var response = await _grpcClient.RoomPayloadAsync(request);


            if (response != null)
            {
                roomPayload = new RoomPayload
                {
                    Id = response.Id,
                    Avatar = response.Avatar,
                    OwnerId = response.OwnerId,
                    Topic = response.Topic,
                    AdminIdList = response.AdminIds.ToList(),
                    MemberIdList = response.MemberIds.ToList()
                };
            }
            return roomPayload;
        }

        public async Task RoomAddAsync(string roomId, string contactId)
        {
            var request = new RoomAddRequest()
            {
                ContactId = contactId,
                Id = roomId
            };
            await _grpcClient.RoomAddAsync(request);
        }

        public async Task<string> RoomAnnounceAsync(string roomId)
        {
            var request = new RoomAnnounceRequest
            {
                Id = roomId
            };


            var response = await _grpcClient.RoomAnnounceAsync(request);
            return response?.Text;
        }

        /// <summary>
        /// 设置群公告
        /// 只有群主和管理员才能有权限修改
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task RoomAnnounceAsync(string roomId, string text)
        {
            var request = new RoomAnnounceRequest
            {
                Id = roomId,
                Text = text
            };

            try
            {
                await _grpcClient.RoomAnnounceAsync(request);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("SERVER_ERROR: 2"))
                {
                    throw new Exception("当前用户不是群主或者管理员，无权限修改群公告");
                }
                throw;
            }

        }

        public async Task<FileBox> RoomAvatarAsync(string roomId)
        {
            var request = new RoomAvatarRequest()
            { Id = roomId };

            var response = await _grpcClient.RoomAvatarAsync(request);

            return FileBox.FromJson(response.FileBox);
        }

        // TODO 可以合并为一个接口
        public async Task<string> RoomCreateAsync(IEnumerable<string> contactIdList, string? topic)
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

        public async Task RoomDelAsync(string roomId, string contactId)
        {
            var request = new RoomDelRequest()
            {
                ContactId = contactId,
                Id = roomId
            };

            await _grpcClient.RoomDelAsync(request);
        }

        public async Task RoomInvitationAcceptAsync(string roomInvitationId)
        {
            var request = new RoomInvitationAcceptRequest()
            {
                Id = roomInvitationId
            };
            await _grpcClient.RoomInvitationAcceptAsync(request);
        }

        public async Task<RoomInvitationPayload> RoomInvitationPayloadAsync(string roomInvitationId)
        {
            var payload = new RoomInvitationPayload();
            var request = new RoomInvitationPayloadRequest()
            {
                Id = roomInvitationId
            };
            var response = await _grpcClient.RoomInvitationPayloadAsync(request);

            if (response == null)
            {
                payload = new RoomInvitationPayload()
                {
                    Avatar = response.Avatar,
                    Id = response.Id,
                    Invitation = response.Invitation,
                    InviterId = response.InviterId,
                    MemberCount = (int)response.MemberCount,
                    MemberIdList = response.MemberIds.ToList(),
                    ReceiverId = response.ReceiverId,
                    Timestamp = (long)response.TimestampUint64Deprecated,
                    Topic = response.Topic
                };
            }
            return payload;
        }

        public async Task<IReadOnlyList<string>> RoomListAsync()
        {
            var response = await _grpcClient.RoomListAsync(new RoomListRequest());
            return response?.Ids.ToList();
        }

        public async Task<string[]> RoomMemberListAsync(string roomId)
        {
            var request = new RoomMemberListRequest()
            {
                Id = roomId
            };

            var response = await _grpcClient.RoomMemberListAsync(request);
            return response?.MemberIds.ToArray();
        }

        public async Task<RoomMemberPayload> RoomMemberPayloadAsync(string roomId, string contactId)
        {
            var payload = new RoomMemberPayload();

            var request = new RoomMemberPayloadRequest()
            {
                Id = roomId,
                MemberId = contactId
            };
            var response = await _grpcClient.RoomMemberPayloadAsync(request);
            if (response != null)
            {
                payload = new RoomMemberPayload()
                {
                    Avatar = response.Avatar,
                    Id = response.Id,
                    InviterId = response.InviterId,
                    Name = response.Name,
                    RoomAlias = response.RoomAlias
                };
            }
            return payload;
        }

        public async Task<string> RoomQRCodeAsync(string roomId)
        {
            var request = new RoomQRCodeRequest()
            {
                Id = roomId
            };

            var response = await _grpcClient.RoomQRCodeAsync(request);
            return response?.Qrcode;
        }

        public async Task RoomQuitAsync(string roomId)
        {
            var request = new RoomQuitRequest()
            {
                Id = roomId
            };
            await _grpcClient.RoomQuitAsync(request);
        }

        public async Task<string> RoomTopicAsync(string roomId)
        {
            var request = new RoomTopicRequest()
            {
                Id = roomId
            };
            var response = await _grpcClient.RoomTopicAsync(request);

            return response?.Topic;
        }

        // TODO  待确定
        public async Task RoomTopicAsync(string roomId, string topic)
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

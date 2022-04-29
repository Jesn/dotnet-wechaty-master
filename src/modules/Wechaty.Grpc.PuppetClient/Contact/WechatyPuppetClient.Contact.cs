using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Newtonsoft.Json;
using Wechaty.Module.Filebox;
using Wechaty.Module.Puppet.Schemas;

namespace Wechaty.Grpc.Client
{
    public partial class WechatyPuppetClient
    {
        #region Contact

        public async Task<ContactPayload> ContactPayloadAsync(string contactId)
        {
            var request = new ContactPayloadRequest()
            { Id = contactId };
            var response = await _grpcClient.ContactPayloadAsync(request);

            var payload = new ContactPayload()
            {
                Id = response.Id,
                Name = response.Name,
                Address = response.Address,
                Alias = response.Alias,
                Avatar = response.Avatar,
                City = response.City,
                Friend = response.Friend,
                Gender = (Module.Puppet.Schemas.ContactGender)response.Gender,
                Province = response.Province,
                Signature = response.Signature,
                Star = response.Star,
                Type = (Module.Puppet.Schemas.ContactType)response.Type,
                Weixin = response.Weixin,
            };
            return payload;
        }

        public async Task<string> ContactAliasAsync(string contactId)
        {
            var request = new ContactAliasRequest
            {
                Id = contactId
            };

            var response = await _grpcClient.ContactAliasAsync(request);

            return response.Alias;
        }

        // TODO 待确认
        public async Task ContactAliasAsync(string contactId, string? alias)
        {
            var request = new ContactAliasRequest();
            if (!string.IsNullOrEmpty(alias))
            {
                request.Alias = alias;
            }
            request.Id = contactId;

            await _grpcClient.ContactAliasAsync(request);

        }

        public async Task<FileBox> ContactAvatarAsync(string contactId)
        {
            var request = new ContactAvatarRequest
            {
                Id = contactId,
            };

            var response = await _grpcClient.ContactAvatarAsync(request);
            var filebox = response.FileBox;
            var fileBox = FileBox.FromJson(filebox);

            return fileBox;
        }

        public async Task ContactAvatarAsync(string contactId, FileBox file)
        {
            var request = new ContactAvatarRequest
            {
                Id = contactId,
                FileBox = JsonConvert.SerializeObject(file)
            };
            await _grpcClient.ContactAvatarAsync(request);
        }

        public async Task<List<string>> ContactListAsync()
        {
            var response = await _grpcClient.ContactListAsync(new ContactListRequest());
            return response?.Ids.ToList();
        }



        public async Task ContactSelfNameAsync(string name)
        {
            var request = new ContactSelfNameRequest();
            request.Name = name;
            await _grpcClient.ContactSelfNameAsync(request);
        }

        public async Task<string> ContactSelfQRCodeAsync()
        {
            var response = await _grpcClient.ContactSelfQRCodeAsync(new ContactSelfQRCodeRequest());

            return response?.Qrcode;
        }

        public async Task ContactSelfSignatureAsync(string signature)
        {
            var request = new ContactSelfSignatureRequest
            {
                Signature = signature
            };

            await _grpcClient.ContactSelfSignatureAsync(request);
        }
        #endregion
    }
}

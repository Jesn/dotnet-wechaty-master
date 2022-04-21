using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Newtonsoft.Json;
using Wechaty.Module.Filebox;

namespace Wechaty.Grpc.PuppetService.Contact
{
    public class ContactService: WechatyPuppetService,IContactService
    {
        #region Contact

        public async Task<string> ContactAlias(string contactId)
        {
            var request = new ContactAliasRequest
            {
                Id = contactId
            };

            var response = await _grpcClient.ContactAliasAsync(request);

            return response.Alias;
        }

        // TODO 待确认
        public async Task ContactAlias(string contactId, string? alias)
        {
            var request = new ContactAliasRequest();
            if (!string.IsNullOrEmpty(alias))
            {
                request.Alias = alias;
            }
            request.Id = contactId;

            await _grpcClient.ContactAliasAsync(request);

        }

        public async Task<FileBox> ContactAvatar(string contactId)
        {
            var request = new ContactAvatarRequest
            {
                Id = contactId
            };

            var response = await _grpcClient.ContactAvatarAsync(request);
            var filebox = response.FileBox;
            return FileBox.FromJson(filebox);
        }

        public async Task ContactAvatar(string contactId, FileBox file)
        {
            var request = new ContactAvatarRequest
            {
                Id = contactId,
                FileBox = JsonConvert.SerializeObject(file)
            };
            await _grpcClient.ContactAvatarAsync(request);
        }

        public async Task<List<string>> ContactList()
        {
            var response = await _grpcClient.ContactListAsync(new ContactListRequest());
            return response?.Ids.ToList();
        }

        public async Task ContactSelfName(string name)
        {
            var request = new ContactSelfNameRequest();
            await _grpcClient.ContactSelfNameAsync(request);
        }

        public async Task<string> ContactSelfQRCode()
        {
            var response = await _grpcClient.ContactSelfQRCodeAsync(new ContactSelfQRCodeRequest());

            return response?.Qrcode;
        }

        public async Task ContactSelfSignature(string signature)
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

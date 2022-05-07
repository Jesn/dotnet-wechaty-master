using System.Collections.Generic;
using System.Threading.Tasks;
using Wechaty.Module.Filebox;

namespace Wechaty.Module.PuppetService
{
    public partial class GrpcPuppet
    {
        #region Contact

        public override async Task<string> ContactAlias(string contactId)
        {

            var response = await _grpcClient.ContactAliasAsync(contactId);

            return response;
        }

        // TODO 待确认
        public override async Task ContactAlias(string contactId, string? alias) => await _grpcClient.ContactAliasAsync(contactId, alias);

        public override async Task<FileBox> ContactAvatar(string contactId)
        {
            var response = await _grpcClient.ContactAvatarAsync(contactId);
            return response;
        }

        public override async Task ContactAvatar(string contactId, FileBox file) => await _grpcClient.ContactAvatarAsync(contactId, file);

        public override async Task<List<string>> ContactList()
        {
            var response = await _grpcClient.ContactListAsync();
            return response;
        }

        public override async Task ContactSelfName(string name) => await _grpcClient.ContactSelfNameAsync(name);

        public override async Task<string> ContactSelfQRCode()
        {
            var response = await _grpcClient.ContactSelfQRCodeAsync();
            return response;
        }

        public override async Task ContactSelfSignature(string signature) => await _grpcClient.ContactSelfSignatureAsync(signature);
        #endregion
    }
}

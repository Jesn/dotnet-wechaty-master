using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Newtonsoft.Json;
using Wechaty.Module.Filebox;

namespace Wechaty.Module.PuppetService
{
    public partial class GrpcPuppet
    {
        #region Contact

        public override async Task<string> ContactAlias(string contactId)
        {

            var response = await _contactService.ContactAliasAsync(contactId);

            return response;
        }

        // TODO 待确认
        public override async Task ContactAlias(string contactId, string? alias)
        {
            await _contactService.ContactAliasAsync(contactId, alias);
        }

        public override async Task<FileBox> ContactAvatar(string contactId)
        {

            var response = await _contactService.ContactAvatarAsync(contactId);
            return response;
        }

        public override async Task ContactAvatar(string contactId, FileBox file)
        {
            await _contactService.ContactAvatarAsync(contactId, file);
        }

        public override async Task<List<string>> ContactList()
        {
            var response = await _contactService.ContactListAsync();
            return response;
        }

        public override async Task ContactSelfName(string name)
        {
            await _contactService.ContactSelfNameAsync(name);
        }

        public override async Task<string> ContactSelfQRCode()
        {
            var response = await _contactService.ContactSelfQRCodeAsync();
            return response;
        }

        public override async Task ContactSelfSignature(string signature)
        {
            await _contactService.ContactSelfSignatureAsync(signature);
        }
        #endregion
    }
}

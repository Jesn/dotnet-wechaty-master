using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wechaty.Module.Filebox;

namespace Wechaty.Grpc.PuppetService.Contact
{
    public  interface IContactService: IApplicationService
    {
        Task<string> ContactAlias(string contactId);
        Task ContactAlias(string contactId, string? alias);
        Task<FileBox> ContactAvatar(string contactId);
        Task ContactAvatar(string contactId, FileBox file);
        Task<List<string>> ContactList();
        Task ContactSelfName(string name);
        Task<string> ContactSelfQRCode();
        Task ContactSelfSignature(string signature);
    }
}

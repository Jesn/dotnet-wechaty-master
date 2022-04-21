using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechaty.Grpc.PuppetService.Tag
{
    public interface ITagService:IApplicationService
    {
        Task TagContactAdd(string tagId, string contactId);
        Task TagContactDelete(string tagId);
        Task<List<string>> TagContactList(string contactId);
        Task<List<string>> TagContactList();
        Task TagContactRemove(string tagId, string contactId);

    }
}

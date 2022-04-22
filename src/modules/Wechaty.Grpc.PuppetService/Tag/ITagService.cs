using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechaty.Grpc.PuppetService.Tag
{
    public interface ITagService:IApplicationService
    {
        Task TagContactAddAsync(string tagId, string contactId);
        Task TagContactDeleteAsync(string tagId);
        Task<List<string>> TagContactListAsync(string contactId);
        Task<List<string>> TagContactListAsync();
        Task TagContactRemoveAsync(string tagId, string contactId);

    }
}

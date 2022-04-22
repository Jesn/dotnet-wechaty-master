using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wechaty.Module.PuppetService
{
    public partial class GrpcPuppet
    {
        #region Tag
        public override async Task TagContactAdd(string tagId, string contactId)
        {
            await _tagService.TagContactAddAsync(tagId,contactId);
        }

        public override async Task TagContactDelete(string tagId)
        {
            await _tagService.TagContactDeleteAsync(tagId);
        }

        public override async Task<List<string>> TagContactList(string contactId)
        {
            var response = await _tagService.TagContactListAsync(contactId);
            return response;
        }

        public override async Task<List<string>> TagContactList()
        {
            var response = await _tagService.TagContactListAsync();
            return response;
        }

        public override async Task TagContactRemove(string tagId, string contactId)
        {
            await _tagService.TagContactRemoveAsync(tagId,contactId);
        }
        #endregion
    }
}

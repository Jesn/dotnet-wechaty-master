using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wechaty.Module.PuppetService
{
    public partial class GrpcPuppet
    {
        #region Tag
        public override async Task TagContactAdd(string tagId, string contactId) => await _grpcClient.TagContactAddAsync(tagId, contactId);

        public override async Task TagContactDelete(string tagId) => await _grpcClient.TagContactDeleteAsync(tagId);

        public override async Task<List<string>> TagContactList(string contactId)
        {
            var response = await _grpcClient.TagContactListAsync(contactId);
            return response;
        }

        public override async Task<List<string>> TagContactList()
        {
            var response = await _grpcClient.TagContactListAsync();
            return response;
        }

        public override async Task TagContactRemove(string tagId, string contactId) => await _grpcClient.TagContactRemoveAsync(tagId, contactId);
        #endregion
    }
}

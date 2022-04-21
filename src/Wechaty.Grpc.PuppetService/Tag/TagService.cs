using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;

namespace Wechaty.Grpc.PuppetService.Tag
{
    public class TagService : WechatyPuppetService, ITagService
    {
        #region Tag
        public async Task TagContactAdd(string tagId, string contactId)
        {
            var request = new TagContactAddRequest()
            {
                Id = tagId,
                ContactId = contactId
            };
            await _grpcClient.TagContactAddAsync(request);
        }

        public async Task TagContactDelete(string tagId)
        {
            var request = new TagContactDeleteRequest()
            {
                Id = tagId
            };

            await _grpcClient.TagContactDeleteAsync(request);
        }

        public async Task<List<string>> TagContactList(string contactId)
        {
            // TODO   确认这里的 contactId 参数是否有效
            var request = new TagContactListRequest();


            var response = await _grpcClient.TagContactListAsync(request);
            return response.Ids.ToList();
        }

        public async Task<List<string>> TagContactList()
        {
            var request = new TagContactListRequest();

            var response = await _grpcClient.TagContactListAsync(request);
            return response.Ids.ToList();
        }

        public async Task TagContactRemove(string tagId, string contactId)
        {
            var request = new TagContactRemoveRequest()
            {
                Id = tagId,
                ContactId = contactId
            };

            await _grpcClient.TagContactRemoveAsync(request);
        }
        #endregion
    }
}

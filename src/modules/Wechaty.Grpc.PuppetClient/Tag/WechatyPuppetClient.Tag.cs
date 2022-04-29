using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;

namespace Wechaty.Grpc.Client
{
    public partial class WechatyPuppetClient
    {
        #region Tag
        public async Task TagContactAddAsync(string tagId, string contactId)
        {
            var request = new TagContactAddRequest()
            {
                Id = tagId,
                ContactId = contactId
            };
            await _grpcClient.TagContactAddAsync(request);
        }

        public async Task TagContactDeleteAsync(string tagId)
        {
            var request = new TagContactDeleteRequest()
            {
                Id = tagId
            };

            await _grpcClient.TagContactDeleteAsync(request);
        }

        public async Task<List<string>> TagContactListAsync(string contactId)
        {
            // TODO   确认这里的 contactId 参数是否有效
            var request = new TagContactListRequest();


            var response = await _grpcClient.TagContactListAsync(request);
            return response.Ids.ToList();
        }

        public async Task<List<string>> TagContactListAsync()
        {
            var request = new TagContactListRequest();

            var response = await _grpcClient.TagContactListAsync(request);
            return response.Ids.ToList();
        }

        public async Task TagContactRemoveAsync(string tagId, string contactId)
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

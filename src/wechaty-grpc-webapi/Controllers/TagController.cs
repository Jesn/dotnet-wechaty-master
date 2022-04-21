using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wechaty.Grpc.PuppetService.Tag;

namespace wechaty_grpc_webapi.Controllers
{
    public class TagController : WechatyApiController
    {

        private readonly ITagService _tagService;
        public TagController(ITagService tagService) => _tagService = tagService;

        [HttpPost]
        public async Task<ActionResult> TagContactAdd(string tagId, string contactId)
        {
            await _tagService.TagContactAdd(tagId, contactId);
            return Ok();
        }

        [HttpDelete("{tagId}")]
        public async Task<IActionResult> TagContactDelete(string tagId)
        {
            await _tagService.TagContactDelete(tagId);
            return NoContent();
        }


        [HttpGet("{contactId}")]
        public async Task<ActionResult<List<string>>> TagContactList(string contactId)
        {
            var response = await _tagService.TagContactList(contactId);
            return response;
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> TagContactList()
        {
            var response = await _tagService.TagContactList();
            return response;
        }


        [HttpPut]
        public async Task<ActionResult> TagContactRemove(string tagId, string contactId)
        {
            await _tagService.TagContactRemove(tagId, contactId);
            return NoContent();
        }
    }
}

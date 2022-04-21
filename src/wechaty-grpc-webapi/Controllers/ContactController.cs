using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wechaty.Grpc.PuppetService.Contact;
using Wechaty.Module.Filebox;

namespace wechaty_grpc_webapi.Controllers
{
    public class ContactController : WechatyApiController
    {

        private readonly IContactService _contactService;

        public ContactController(IContactService contactService) => _contactService = contactService;

        [HttpGet]
        public async Task<ActionResult> ContactAlias(string contactId)
        {
            var response = await _contactService.ContactAlias(contactId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> ContactAlias(string contactId, string? alias)
        {
            await _contactService.ContactAlias(contactId, alias);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> ContactAvatar(string contactId)
        {
            var response = await _contactService.ContactAvatar(contactId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> ContactAvatar(string contactId, FileBox file)
        {
            await _contactService.ContactAvatar(contactId, file);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> ContactList()
        {
            var response = await _contactService.ContactList();
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> ContactSelfName(string name)
        {
            await _contactService.ContactSelfName(name);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> ContactSelfQRCode()
        {
            var response = await _contactService.ContactSelfQRCode();
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> ContactSelfSignature(string signature)
        {
            await _contactService.ContactSelfSignature(signature);
            return Ok();
        }
    }
}

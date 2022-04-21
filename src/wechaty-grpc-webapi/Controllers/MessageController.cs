using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wechaty.Grpc.PuppetService.Message;
using Wechaty.Module.Filebox;
using Wechaty.Module.Puppet.Schemas;

namespace wechaty_grpc_webapi.Controllers
{
    public class MessageController : WechatyApiController
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService) => _messageService = messageService;

        [HttpGet]
        public async Task<ActionResult> MessageContact(string messageId)
        {
            var response = await _messageService.MessageContact(messageId);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> MessageFile(string messageId)
        {
            var response = await _messageService.MessageFile(messageId);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> MessageImage(string messageId, ImageType imageType)
        {
            var response = await _messageService.MessageImage(messageId, imageType);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> MessageImageStream(string messageId, ImageType imageType, CancellationToken cancellationToken = default)
        {
            var response = await _messageService.MessageImageStream(messageId, imageType, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> MessageMiniProgram(string messageId)
        {
            var response = await _messageService.MessageMiniProgram(messageId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> MessageRecall(string messageId)
        {
            var response = await _messageService.MessageRecall(messageId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> MessageSendContact(string conversationId, string contactId)
        {
            var response = await _messageService.MessageSendContact(conversationId, contactId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> MessageSendFile(string conversationId, FileBox file)
        {
            var response = await _messageService.MessageSendFile(conversationId, file);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> MessageSendMiniProgram(string conversationId, MiniProgramPayload miniProgramPayload)
        {
            var response = await _messageService.MessageSendMiniProgram(conversationId, miniProgramPayload);
            return Ok(response);
        }

        //[HttpPut]
        //public async Task<ActionResult> MessageSendText(string conversationId, string text, params string[]? mentionIdList)
        //{
        //    var response=await _messageService.MessageSendText(conversationId, text, mentionIdList);
        //    return Ok(response);
        //}

        [HttpPut]
        public async Task<ActionResult> MessageSendText(string conversationId, string text, IEnumerable<string>? mentionIdList)
        {
            var response = await _messageService.MessageSendText(conversationId, text, mentionIdList);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> MessageSendUrl(string conversationId, UrlLinkPayload urlLinkPayload)
        {
            var response = await _messageService.MessageSendUrl(conversationId, urlLinkPayload);
            return Ok(response);
        }


        [HttpPut]
        public async Task<ActionResult> MessageUrl(string messageId)
        {
            var response = await _messageService.MessageUrl(messageId);
            return Ok(response);
        }
    }
}

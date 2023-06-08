using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIMDS.Controllers
{
    [ApiController]
    [Route("messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            var messages = await _messageRepository.GetMessages();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessageById(int id)
        {
            var message = await _messageRepository.GetMessageById(id);
            if (message == null)
                return NotFound();

            return Ok(message);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> CreateMessage(Message message)
        {
            var createdMessage = await _messageRepository.CreateMessage(message);
            return CreatedAtAction(nameof(GetMessageById), new { id = createdMessage.Id }, createdMessage);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Message>> UpdateMessage(int id, Message message)
        {
            if (id != message.Id)
                return BadRequest();

            var existingMessage = await _messageRepository.GetMessageById(id);
            if (existingMessage == null)
                return NotFound();

            var updatedMessage = await _messageRepository.UpdateMessage(message);
            return Ok(updatedMessage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var deleted = await _messageRepository.DeleteMessage(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

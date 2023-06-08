using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIMDS.Controllers
{
    [ApiController]
    [Route("conversations")]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationRepository _conversationRepository;

        public ConversationController(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversations()
        {
            var conversations = await _conversationRepository.GetConversations();
            return Ok(conversations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Conversation>> GetConversationById(int id)
        {
            var conversation = await _conversationRepository.GetConversationById(id);
            if (conversation == null)
                return NotFound();

            return Ok(conversation);
        }

        [HttpPost]
        public async Task<ActionResult<Conversation>> CreateConversation(Conversation conversation)
        {
            var createdConversation = await _conversationRepository.CreateConversation(conversation);
            return CreatedAtAction(nameof(GetConversationById), new { id = createdConversation.Id }, createdConversation);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Conversation>> UpdateConversation(int id, Conversation conversation)
        {
            if (id != conversation.Id)
                return BadRequest();

            var existingConversation = await _conversationRepository.GetConversationById(id);
            if (existingConversation == null)
                return NotFound();

            var updatedConversation = await _conversationRepository.UpdateConversation(conversation);
            return Ok(updatedConversation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteConversation(int id)
        {
            var deleted = await _conversationRepository.DeleteConversation(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

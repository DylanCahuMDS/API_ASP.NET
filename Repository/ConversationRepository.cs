using Microsoft.EntityFrameworkCore;

namespace APIMDS
{
    public interface IConversationRepository
    {
        Task<IEnumerable<Conversation>> GetConversations();
        Task<Conversation> GetConversationById(int id);
        Task<Conversation> CreateConversation(Conversation conversation);
        Task<Conversation> UpdateConversation(Conversation conversation);
        Task<bool> DeleteConversation(int id);
    }

    public class ConversationRepository : IConversationRepository
    {
        private readonly ChatDbContext _dbContext;

        public ConversationRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Conversation>> GetConversations()
        {
            return await _dbContext.Conversations.ToListAsync();
        }

        public async Task<Conversation> GetConversationById(int id)
        {
            return await _dbContext.Conversations.FindAsync(id);
        }

        public async Task<Conversation> CreateConversation(Conversation conversation)
        {
            _dbContext.Conversations.Add(conversation);
            await _dbContext.SaveChangesAsync();
            return conversation;
        }

        public async Task<Conversation> UpdateConversation(Conversation conversation)
        {
            _dbContext.Conversations.Update(conversation);
            await _dbContext.SaveChangesAsync();
            return conversation;
        }

        public async Task<bool> DeleteConversation(int id)
        {
            var conversation = await _dbContext.Conversations.FindAsync(id);
            if (conversation == null)
                return false;

            _dbContext.Conversations.Remove(conversation);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

}

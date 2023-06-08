using Microsoft.EntityFrameworkCore;

namespace APIMDS
{


    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessages();
        Task<Message> GetMessageById(int id);
        Task<Message> CreateMessage(Message message);
        Task<Message> UpdateMessage(Message message);
        Task<bool> DeleteMessage(int id);
    }

    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _dbContext;

        public MessageRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await _dbContext.Messages.ToListAsync();
        }

        public async Task<Message> GetMessageById(int id)
        {
            return await _dbContext.Messages.FindAsync(id);
        }

        public async Task<Message> CreateMessage(Message message)
        {
            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();
            return message;
        }

        public async Task<Message> UpdateMessage(Message message)
        {
            _dbContext.Messages.Update(message);
            await _dbContext.SaveChangesAsync();
            return message;
        }

        public async Task<bool> DeleteMessage(int id)
        {
            var message = await _dbContext.Messages.FindAsync(id);
            if (message == null)
                return false;

            _dbContext.Messages.Remove(message);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace APIMDS
{
    public interface IUniversRepository
    {
        Task<IEnumerable<Univers>> GetUnivers();
        Task<Univers> GetUniversById(int id);
        Task<Univers> CreateUnivers(Univers Univers);
        Task<Univers> UpdateUnivers(Univers Univers);
        Task<bool> DeleteUnivers(int id);
    }

    public class UniversRepository : IUniversRepository
    {
        private readonly ChatDbContext _dbContext;

        public UniversRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Univers>> GetUnivers()
        {
            return await _dbContext.Univers.ToListAsync();
        }

        public async Task<Univers> GetUniversById(int id)
        {
            return await _dbContext.Univers.FindAsync(id);
        }

        public async Task<Univers> CreateUnivers(Univers Univers)
        {
            _dbContext.Univers.Add(Univers);
            await _dbContext.SaveChangesAsync();
            return Univers;
        }

        public async Task<Univers> UpdateUnivers(Univers Univers)
        {
            _dbContext.Univers.Update(Univers);
            await _dbContext.SaveChangesAsync();
            return Univers;
        }

        public async Task<bool> DeleteUnivers(int id)
        {
            var Univers = await _dbContext.Univers.FindAsync(id);
            if (Univers == null)
                return false;

            _dbContext.Univers.Remove(Univers);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

}

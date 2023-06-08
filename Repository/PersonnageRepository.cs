using Microsoft.EntityFrameworkCore;

namespace APIMDS
{
    public interface IPersonnageRepository
    {
        Task<IEnumerable<Personnage>> GetPersonnages();
        Task<Personnage> GetPersonnageById(int id);
        Task<Personnage> CreatePersonnage(Personnage personnage);
        Task<Personnage> UpdatePersonnage(Personnage personnage);
        Task<bool> DeletePersonnage(int id);
    }

    public class PersonnageRepository : IPersonnageRepository
    {
        private readonly ChatDbContext _dbContext;

        public PersonnageRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Personnage>> GetPersonnages()
        {
            return await _dbContext.Personnages.ToListAsync();
        }

        public async Task<Personnage> GetPersonnageById(int id)
        {
            return await _dbContext.Personnages.FindAsync(id);
        }

        public async Task<Personnage> CreatePersonnage(Personnage personnage)
        {
            _dbContext.Personnages.Add(personnage);
            await _dbContext.SaveChangesAsync();
            return personnage;
        }

        public async Task<Personnage> UpdatePersonnage(Personnage personnage)
        {
            _dbContext.Personnages.Update(personnage);
            await _dbContext.SaveChangesAsync();
            return personnage;
        }

        public async Task<bool> DeletePersonnage(int id)
        {
            var personnage = await _dbContext.Personnages.FindAsync(id);
            if (personnage == null)
                return false;

            _dbContext.Personnages.Remove(personnage);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

}

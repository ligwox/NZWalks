using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await _nZWalksDbContext.AddAsync(walkDifficulty);
            await _nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkDifficulty = await _nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDifficulty == null)
                return null;
            _nZWalksDbContext.WalkDifficulty.Remove(walkDifficulty);
            await _nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await _nZWalksDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await _nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty updatedWalkDifficulty)
        {
            var walkDifficulty = await _nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDifficulty == null)
                return null;

            walkDifficulty.Code = updatedWalkDifficulty.Code;
            await _nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }
    }
}

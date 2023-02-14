using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<Walks> AddAsync(Walks walks)
        {
            walks.Id = Guid.NewGuid();
            await _nZWalksDbContext.Walks.AddAsync(walks);
            await _nZWalksDbContext.SaveChangesAsync();
            return walks;
        }

        public async Task<Walks> DeleteAsync(Guid id)
        {
            var walk = await _nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
                return null;
            _nZWalksDbContext.Walks.Remove(walk);
            await _nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walks>> GetAllAsync()
        {
            return await _nZWalksDbContext.Walks
                .Include(x=> x.Region)
                .Include(x=> x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walks> GetAsync(Guid id)
        {
            return await _nZWalksDbContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walks> UpdateAsync(Guid id, Walks walks)
        {
            var walk = await _nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walk == null)
                return null;

            walk.Name = walks.Name;
            walk.WalkDifficultyId = walks.WalkDifficultyId;
            walk.RegionId = walks.RegionId;
            walk.Length = walks.Length;

            await _nZWalksDbContext.SaveChangesAsync();
            return walk;
        }
    }
}

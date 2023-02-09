using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;
        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _nZWalksDbContext.AddAsync(region);
            await _nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = _nZWalksDbContext.Regions.FirstOrDefault(region=> region.Id == id);
            if (region == null) {
                return null;
            }
            _nZWalksDbContext.Regions.Remove(region);
            await _nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await _nZWalksDbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var reg = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);
            if (reg == null) {
                return null;
            }
            reg.Code = region.Code;
            reg.Name = region.Name;
            reg.Lat = region.Lat;
            reg.Long = region.Long;
            reg.Area = region.Area;
            reg.Population = region.Population;
            
            await _nZWalksDbContext.SaveChangesAsync();
            
            return reg;
        }
    }
}

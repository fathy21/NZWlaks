using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RepositoryRegion : IRepositoryRegion
    {
        private readonly NZWalksDbContext _dbContext;

        public RepositoryRegion(NZWalksDbContext dbContext)
        {
           _dbContext = dbContext;
        }

        public async Task<Region> Create(Region region)
        {
            await _dbContext.regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return (region);
        }

        public async Task<Region?> Delete(Guid id)
        {
            var exestingregion = await _dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (exestingregion is null)
            {
                return null;
            }
            
           _dbContext.regions.Remove(exestingregion);
            await _dbContext.SaveChangesAsync();
            return exestingregion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.regions.ToListAsync();
        }

        public async Task<Region?> GetById(Guid id)
        {
            return await _dbContext.regions.FirstOrDefaultAsync(r => r.Id == id);   
        }

        public async Task<Region?> Update(Guid id, Region region)
        {
            var exestingregion = await _dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
            if(exestingregion is null)
            {
                return null;
            }
            exestingregion.Code = region.Code;
            exestingregion.Name = region.Name;
            exestingregion.RegionImageUrl = region.RegionImageUrl;

            await _dbContext.SaveChangesAsync();
            return exestingregion;
        }

    }
}

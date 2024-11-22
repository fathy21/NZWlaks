using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public WalkRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Walk> Create(Walk walk)
        {
            await _dbContext.walks.AddAsync(walk);  
            await _dbContext.SaveChangesAsync();    
            return(walk);   
        }

        public async Task<Walk?> Delete(Guid id)
        {
            var exestingwalk = await _dbContext.walks.FirstOrDefaultAsync(x => x.Id == id);
            if (exestingwalk == null)
            {
                return null;
            }

             _dbContext.walks.Remove(exestingwalk);
            await _dbContext.SaveChangesAsync();

            return(exestingwalk);
        }

        public async Task<List<Walk>> GetAll(string? filteroncolumn = null, string? filteronword = null ,
             string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            //filter by word in column 
            var walks = _dbContext.walks.Include("Difficulty").Include("Region").AsQueryable(); //Queryable عشان هجيب الداتا من الداتا بيز 
            if (string.IsNullOrWhiteSpace(filteroncolumn) == false && string.IsNullOrWhiteSpace(filteronword) == false)
            {
                if(filteroncolumn.Equals("Name" , StringComparison.OrdinalIgnoreCase)) 
                {
                    walks = walks.Where(x=>x.Name.Contains(filteronword));                  
                }
                else if(filteroncolumn.Equals("Difficulty" , StringComparison.OrdinalIgnoreCase))
                { 
                    walks = walks.Where(x=>x.Difficulty.Name.Contains(filteronword));   
                }
            }

            //sorting 
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name" , StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Length" , StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //pagination
            var skipresultes = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipresultes).Take(pageSize).ToListAsync();

        }

        public async Task<Walk?> GetById(Guid id)
        {
           return await _dbContext.walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk?> Upate(Guid id, Walk walk)
        {
            var exestingwalk = await _dbContext.walks.FirstOrDefaultAsync(x => x.Id == id);
            if (exestingwalk == null)
            {
                return null;
            }
            exestingwalk.Name = walk.Name;
            exestingwalk.Description = walk.Description;
            exestingwalk.LengthInKm = walk.LengthInKm;   
            exestingwalk.WalkIageUrl = walk.WalkIageUrl;    
            exestingwalk.DifficultyId = walk.DifficultyId;   
            exestingwalk.RegionId =  walk.RegionId;
            
            await _dbContext.SaveChangesAsync();    

            return (exestingwalk);  
        }
    }
}

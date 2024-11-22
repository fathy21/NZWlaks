using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> Create(Walk walk);   
        Task<List<Walk>> GetAll(string? filteroncolumn = null , string? filteronword = null , 
            string? sortBy = null , bool isAscending = true , 
            int pageNumber = 1 , int pageSize = 1000);  
        Task<Walk?>GetById (Guid id);
        Task<Walk?> Upate (Guid id , Walk walk);
        Task<Walk?> Delete (Guid id);
    }
}

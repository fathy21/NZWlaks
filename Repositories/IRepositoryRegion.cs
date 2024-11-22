using NZWalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories
{
    public interface IRepositoryRegion
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetById(Guid id);
        Task<Region> Create(Region region);
        Task<Region?> Update(Guid id , Region region);
        Task<Region?> Delete(Guid id);
    }
}

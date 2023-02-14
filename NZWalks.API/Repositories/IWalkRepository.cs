using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walks>> GetAllAsync();
        Task<Walks> GetAsync(Guid id);
        Task<Walks> AddAsync(Walks walks);
        Task<Walks> UpdateAsync(Guid id, Walks walks);
        Task<Walks> DeleteAsync(Guid id);
    }
}

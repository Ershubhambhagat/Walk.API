using Microsoft.Identity.Client;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDiffucaltyRepository  
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        Task <WalkDifficulty> GetAsync(Guid Id);
        Task <WalkDifficulty> addAsync(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> DeleteAsync(Guid id);
        Task<WalkDifficulty> updateAsync(Guid id, WalkDifficulty walkDifficulty);
    }
}

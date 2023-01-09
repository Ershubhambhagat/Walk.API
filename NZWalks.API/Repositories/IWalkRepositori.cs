using NZWalks.API.Models.Domain;
using System.Collections;
//using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepositori
    {
        Task <IEnumerable<Models.Domain.Walk>> GetAllAsync();
        Task<Models.Domain.Walk> GetWalkAsync(Guid Id);

        Task<Models.Domain.Walk> AddWalkAsync(Models.Domain.Walk walk);

        Task<Walk> UpdateWalkAsync(Guid Id, Models.Domain.Walk walk);
        Task<Walk> DeleteWalkAsync(Guid Id);

        
    }
}

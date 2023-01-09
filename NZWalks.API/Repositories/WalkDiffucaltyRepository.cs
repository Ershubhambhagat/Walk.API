using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDiffucaltyRepository : IWalkDiffucaltyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDiffucaltyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> addAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.walkDifficulties.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return (walkDifficulty);

        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var DeleteWalkDiffucalty = await nZWalksDbContext.walkDifficulties.FindAsync(id);
            if (DeleteWalkDiffucalty != null)
            {
                nZWalksDbContext.walkDifficulties.Remove(DeleteWalkDiffucalty);
                await nZWalksDbContext.SaveChangesAsync();
                return DeleteWalkDiffucalty;

            }
            return null;



        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalksDbContext.walkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid Id)
        {
            return nZWalksDbContext.walkDifficulties.FirstOrDefault(x => x.Id == Id);


        }

        public async Task<WalkDifficulty> updateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var ExistinWalkgDiffu = await nZWalksDbContext.walkDifficulties.FindAsync(id);
            if (ExistinWalkgDiffu == null)
            {
                return (null);
            }
            ExistinWalkgDiffu.Code = walkDifficulty.Code;
            await nZWalksDbContext.SaveChangesAsync();
            return ExistinWalkgDiffu;


        }



    }
}

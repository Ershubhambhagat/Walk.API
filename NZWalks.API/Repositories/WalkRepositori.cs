using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
//using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class WalkRepositori : IWalkRepositori
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        public WalkRepositori(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<Models.Domain.Walk> AddWalkAsync(Walk walk)
        {
            //Assign New Id
            walk.Id = new Guid();
            await nZWalksDbContext.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteWalkAsync(Guid Id)
        {
            var existing = await nZWalksDbContext.walks.FindAsync(Id);
            if (existing == null)
            {
                return (null);

            }
            else
            {
                nZWalksDbContext.walks.Remove(existing);
                await nZWalksDbContext.SaveChangesAsync();
                
            }
            return (existing);
        }

        public async Task<Walk> UpdateWalkAsync(Guid Id, Walk walk)
        {
           var ExistingWalk= await nZWalksDbContext.walks.FindAsync(Id);
            if (ExistingWalk == null)
            {
                throw new NotImplementedException();
            }
            ExistingWalk.Length = walk.Length;
            ExistingWalk.RegionId=    walk.RegionId;
            ExistingWalk.walkDifficulty= walk.walkDifficulty;
            ExistingWalk.Name=walk.Name;
            await nZWalksDbContext.SaveChangesAsync();
            return ExistingWalk;

        }
        async Task<IEnumerable<Walk>> IWalkRepositori.GetAllAsync()
        {
            return await nZWalksDbContext.walks
                 .Include(x => x.region)
                 .Include(x => x.walkDifficulty).
                ToListAsync();
        }

        public async Task<Walk> GetWalkAsync(Guid id)
        {
            return await nZWalksDbContext.walks
                 .Include(x => x.region)
                 .Include(x => x.walkDifficulty)
                 .FirstOrDefaultAsync(x => x.Id == id);


        }
    }
}

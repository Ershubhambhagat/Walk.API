using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepositori : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepositori(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = new Guid();
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid Id)
        {
            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (region == null)
            {
                throw new Exception();
            }
            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync(); return region;

        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksDbContext.Regions.ToArrayAsync();
        }

        public async Task<Region> GetAsync(Guid Id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(r => r.Id == Id);

        }
        public async Task<Region> UpdateAsync(Guid Id, Region region)
        {
            var ExistingRegion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (ExistingRegion == null)
            {
                return null;
            }
           // ExistingRegion.Code = region.Code;
            ExistingRegion.Name = region.Name;
            ExistingRegion.Walks = region.Walks;
            ExistingRegion.Area = region.Area;
            ExistingRegion.Lat = region.Lat;
            ExistingRegion.Long = region.Long;
            ExistingRegion.Population = region.Population;

            await nZWalksDbContext.SaveChangesAsync();
            return ExistingRegion;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext:DbContext
    {
        //why I  am writing base class 

        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options):base(options)
        {        }
        //If that is not present in DB then Create this table using property 
        public DbSet<Region>  Regions { get; set; }
        public DbSet<Walk> walks { get; set; }
        public DbSet<WalkDifficulty>  walkDifficulties{ get; set; }
    }
}

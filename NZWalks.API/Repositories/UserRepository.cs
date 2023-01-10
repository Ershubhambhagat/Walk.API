using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public UserRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<User> AuthencaticateUSer(string name, string password)
        {
           var user=await nZWalksDbContext.Users.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.Password == password);

            if(user == null)
            {
                return(null);
            }
            var userRole=await nZWalksDbContext.user_Roles.Where(x=>x.UserId == user.Id).ToListAsync();
           //Now I am Checking EMpty 
            if(userRole.Any() )
            {
                user.Roles = new List<string>();
                foreach(var user_Role in userRole )
                {
                   var role= await nZWalksDbContext.Roles.FirstOrDefaultAsync(x => x.Id == user_Role.RoleId);
                    if (role == null)
                    {
                        return (null);
                    }
                    else
                    {
                        user.Roles.Add(role.Name);

                    }
                }

            }
            user.Password = null;
            return user;
        }
    }
}

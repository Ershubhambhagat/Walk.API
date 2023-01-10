using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            new User()
            {
                Id=new Guid(),
                Name="read",
                Email="readonly.com",
                Password="readonly",
                Roles=new List<string>{"Reader"}
            },


            new User()
            {
                Id=new Guid(),
                Name="readwrite",
                Email="readwrite.com",
                Password="readwrite",
                Roles=new List<string>{"Reader","Write"}
            }
        };
        public async Task<User> AuthencaticateUSer(string name, string password)
        {
            var user = Users.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) &&
             x.Password == password);
    
            return user;
        }
    }
}

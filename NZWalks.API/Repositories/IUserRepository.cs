using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthencaticateUSer(string username, string password);

    }
}

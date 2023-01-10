using NZWalks.API.Models.Domain;
using System.Runtime.CompilerServices;

namespace NZWalks.API.Repositories
{
    public interface ITockenHandler
    {
         Task<string> createTocken(User user);
        
    }
}

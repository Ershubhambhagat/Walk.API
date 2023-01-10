using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repositories
{
    public class TockenHandlerRepository : ITockenHandler
    {
        private readonly IConfiguration configuration;

        public TockenHandlerRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<string> createTocken(User user)
        {

            //Create clame for this tocken

            var clame = new List<Claim>();
            clame.Add(new Claim(ClaimTypes.GivenName,user.Name));
            clame.Add(new Claim(ClaimTypes.Email,user.Email));
            //Loop into Rool of user


            user.Roles.ForEach((role) =>
            {
                clame.Add(new Claim(ClaimTypes.Role, role));


            });
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credential = new SigningCredentials(Key,SecurityAlgorithms.HmacSha256);
            var Tocken = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                clame,
                expires: DateTime.Now.AddMinutes(9),
                signingCredentials: credential);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(Tocken));
        }

    }
}

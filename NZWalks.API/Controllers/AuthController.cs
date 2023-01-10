using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITockenHandler tockenHandler;

        public AuthController(IUserRepository userRepository, ITockenHandler tockenHandler)
        {
            this.userRepository = userRepository;
            this.tockenHandler = tockenHandler;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> loginAsync(Models.DTO.loginRequest loginRequest)
        {
            //validate user request {validater Folder} 
            //check user is authentincate 
            //check user name or Password 
            var user = await userRepository.AuthencaticateUSer(loginRequest.Name, loginRequest.Password);
            if (user != null)
            {

                //Genrate Auth tocken
                var tocken = await tockenHandler.createTocken(user);
                return Ok(tocken);

            }
            return BadRequest("User name or password is wronge ");
        }
    }
}

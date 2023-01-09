

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class WalkController : Controller

    {
        private readonly IWalkRepositori walkRepositori;
        private readonly IMapper mapper;

        public WalkController(IWalkRepositori walkRepositori, IMapper mapper)
        {
            this.walkRepositori = walkRepositori;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkAsync()
        {
            //Fatch data to databa - Domain Walk
            var WalkDomain = await walkRepositori.GetAllAsync();


            //Convert Domain walk to DTO walk
            var WalkDTO = mapper.Map<List<Models.DTO.Walk>>(WalkDomain);

            //Return Responce
            return Ok(WalkDTO);

        }
        [HttpGet]
        [Route("{Id:Guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid Id)
        {
            //Get Walk Domain OBject From Database 
            var Walkdomain =await walkRepositori.GetWalkAsync(Id);
             
            //Check Null 
            if(Walkdomain == null)
            {
                return BadRequest();
            }
            //Convert domain to DTO 

            var WalkDTO = mapper.Map<Models.DTO.Walk>(Walkdomain);

            //Return Responce 
            return Ok(WalkDTO);
        }



        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            // Convert DTO to Domain Obj
            var walkDomain = new Models.Domain.Walk()
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };
            //Pass Domain  Object to repositry to persoist this
            var walkDomains = await walkRepositori.AddWalkAsync(walkDomain);
            //Conver the domain Obj Back To DTO
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDomains.Id,
                Length = walkDomains.Length,
                Name = walkDomains.Name,
                RegionId = walkDomains.RegionId,
                WalkDifficultyId = walkDomains.WalkDifficultyId
            };
            // Send DTO Back To Clint

            return CreatedAtAction(nameof(GetAllWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }


        [HttpPut]
        [Route("{id :Guid}")]
        public async Task<IActionResult> UpdateWalkAsync(Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)

        {
            // convert DTO to domain Object
            var walkDomain = new Models.Domain.Walk
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,

            };

            //Pass Details to Repositry  Get Domain object in repositry (or null)

            walkDomain= await walkRepositori.UpdateWalkAsync(id, walkDomain);

            //handle Null
            if (walkDomain == null)
            {
                return BadRequest();
            }
            else
            {
                var walkDTO = new Models.DTO.Walk
                {
                    Length = walkDomain.Length,
                    Name = walkDomain.Name,
                    RegionId = walkDomain.RegionId,
                    Id= walkDomain.Id,
                    WalkDifficultyId= walkDomain.WalkDifficultyId,
                };
                return(Ok(walkDTO) );
            }
            //Convert back to DTO 
            //Return Responce 

        }

        [HttpDelete]
        [Route("{id :Guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
           var workDomain= await walkRepositori.DeleteWalkAsync(id);
            if(workDomain == null){
                return BadRequest();

            }
            var walkDTO = mapper.Map<Models.Domain.Walk>(workDomain);
            return(Ok(walkDTO));
        }



    }
}

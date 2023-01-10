

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Data;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkController : Controller

    {
        private readonly IWalkRepositori walkRepositori;
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDiffucaltyRepository walkDiffucaltyRepository;

        public WalkController(IWalkRepositori walkRepositori, IMapper mapper, IRegionRepository regionRepository,IWalkDiffucaltyRepository walkDiffucaltyRepository)
        {
            this.walkRepositori = walkRepositori;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDiffucaltyRepository = walkDiffucaltyRepository;
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
            var Walkdomain = await walkRepositori.GetWalkAsync(Id);

            //Check Null 
            if (Walkdomain == null)
            {
                return BadRequest();
            }
            //Convert domain to DTO 

            var WalkDTO = mapper.Map<Models.DTO.Walk>(Walkdomain);

            //Return Responce 
            return Ok(WalkDTO);
        }
        [HttpPost]
        [Authorize(Roles = "reader")]

        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            //Validate Incoming Request 

            if (!(await ValidateAddWalkAsync(addWalkRequest)))
            {
                return BadRequest(ModelState);
            }
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
        [Authorize(Roles = "write")]
        public async Task<IActionResult> UpdateWalkAsync(Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            //Validate Incoming Request
            if (!(await ValidateUpdateWalkAsync(updateWalkRequest)))
            {
                return BadRequest(ModelState);
            }
            // convert DTO to domain Object
            var walkDomain = new Models.Domain.Walk
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,

            };

            //Pass Details to Repositry  Get Domain object in repositry (or null)

            walkDomain = await walkRepositori.UpdateWalkAsync(id, walkDomain);

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
                    Id = walkDomain.Id,
                    WalkDifficultyId = walkDomain.WalkDifficultyId,
                };
                return (Ok(walkDTO));
            }
            //Convert back to DTO 
            //Return Responce 
        }

        [HttpDelete]
        [Route("{id :Guid}")]
        [Authorize(Roles = "write")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var workDomain = await walkRepositori.DeleteWalkAsync(id);
            if (workDomain == null)
            {
                return BadRequest();

            }
            var walkDTO = mapper.Map<Models.Domain.Walk>(workDomain);
            return (Ok(walkDTO));
        }


        #region Private methods

        private async Task<bool> ValidateAddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            //if (addWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest), $"{nameof(addWalkRequest)} All not Be Empty ");
            //    return false;
            //}
            //if (string.IsNullOrEmpty(addWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Name), $"{nameof(addWalkRequest.Name)} Name not be Empty ,Only string Will be vilade ");
            //}
            //if (addWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Length), $"{nameof(addWalkRequest.Length)}Lenth Not be less Then 0 or Empty");
            //}
            var region =await regionRepository.GetAsync(addWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId), $"{nameof(addWalkRequest.RegionId)}Region Id is not Correct ");
            }
            var WorkDifficulty =await walkDiffucaltyRepository.GetAsync(addWalkRequest.WalkDifficultyId);
            if (WorkDifficulty == null) 
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId), $"{nameof(addWalkRequest.WalkDifficultyId)}WorkDifficulty Id is not Correct ");

            }
            if(ModelState.ErrorCount> 0)
            {
                return false;
            }
            return true;


            
        }
        private  async Task<bool> ValidateUpdateWalkAsync(Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            if (updateWalkRequest == null)
            {
                ModelState.AddModelError(nameof(ValidateUpdateWalkAsync), $"{nameof(ValidateUpdateWalkAsync)} All not Be Empty ");
                return false;

            }
            if (string.IsNullOrEmpty(updateWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Name), $"{nameof(updateWalkRequest.Name)} Name not be Empty ,Only string Will be vilade ");
            }
            if (updateWalkRequest.Length <= 0)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Length), $"{nameof(updateWalkRequest.Length)}Lenth Not be less Then 0 or Empty");
            }
            var region = await regionRepository.GetAsync(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId), $"{nameof(updateWalkRequest.RegionId)}Region Id is not Correct ");
            }
            var WorkDifficulty = await walkDiffucaltyRepository.GetAsync(updateWalkRequest.WalkDifficultyId);
            if (WorkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId), $"{nameof(updateWalkRequest.WalkDifficultyId)}WorkDifficulty Id is not Correct ");

            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        #endregion


    }
}

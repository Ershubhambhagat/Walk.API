using AutoMapper;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
//using NZWalks.API.Data;
//using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Data;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkDiffucaltyControllerr : ControllerBase
    {
        private readonly IWalkDiffucaltyRepository walkDiffucaltyRepository;
        private readonly IMapper mapper;

        public WalkDiffucaltyControllerr(IWalkDiffucaltyRepository walkDiffucaltyRepository, IMapper mapper)
        {
            this.walkDiffucaltyRepository = walkDiffucaltyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDiffuculty()
        {
            //DTO To Domain
            var workDiffucaltyDomain = await walkDiffucaltyRepository.GetAllAsync();
            //Domain TO DTO
            var workDiffucaltyDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(workDiffucaltyDomain);
            return Ok(workDiffucaltyDTO);
        }
        [HttpGet]
        [Route("{id :Guid}")]
        [ActionName("GetWalkdiffucalty")]
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetWalkdiffucalty(Guid id)
        {
            var walkDiffucalty = await walkDiffucaltyRepository.GetAsync(id);
            if (walkDiffucalty == null)
            {
                return NotFound();
            }
            //convert  Domain to DTO 
            var wolkDiffucaltyMapper = mapper.Map<Models.DTO.WalkDifficulty>(walkDiffucalty);

            return Ok(wolkDiffucaltyMapper);
        }
        [HttpPost]
        [Authorize(Roles = "Write")]

        public async Task<IActionResult> addwalkDiffucaltyAsync(Models.DTO.AddWalkDiffucaltyRequest addWalkDiffucaltyRequest)
        {

            //validate 

            //if(!ValidateaddwalkDiffucaltyAsync(addWalkDiffucaltyRequest))
            //{
            //    return BadRequest(ModelState);
            //};
            //Convert DTO ToDomain
            var workDiffucaltyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDiffucaltyRequest.Code
            };
            // call Repositry 
            var WalkDiffucaltyDomain = await walkDiffucaltyRepository.addAsync(workDiffucaltyDomain);

            //conver domain to dto
            var workDoffucaltyDTO = mapper.Map<Models.DTO.WalkDifficulty>(WalkDiffucaltyDomain);
            //Return Responce  
            return CreatedAtAction(nameof(GetWalkdiffucalty), new { id = workDoffucaltyDTO.Id }, workDoffucaltyDTO);
        }
        [HttpPut]
        [Route("{id :Guid}")]
        [Authorize(Roles = "Write")]
        public async Task<IActionResult> UpdateWalkDiffucaltyAsync(Guid id, [FromBody] Models.DTO.UpdateWalkDiffucaltyRequest updateWalkDiffucaltyRequest)
        {
            //validate Incoming value
            //if (!ValidateUpdateWalkDiffucaltyAsync(updateWalkDiffucaltyRequest))
            //{
            //    return BadRequest(ModelState);
            //};
            // Conver DTO TO Domain Model
            var walkUpdateDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDiffucaltyRequest.Code,
            };
            //Call Repositry For update
            var walkUpdateDifficultyDomains = walkDiffucaltyRepository.updateAsync(id, walkUpdateDifficultyDomain);
            if (walkUpdateDifficultyDomains == null)
            {
                return NotFound();
            }
            // Conver Domain TO DTO 
            var walkDiffucaltyDTO = mapper.Map<Models.Domain.WalkDifficulty>(walkUpdateDifficultyDomains);
            //return Responce 
            return Ok(walkDiffucaltyDTO);
        }
        [HttpDelete]
        [Authorize(Roles = "Write")]
        [Route("{id :Guid}")]
        public async Task<IActionResult> DeleteWalkDiffucalty(Guid id)

        {
            //Convert to Domain Form Dto
            var walkDiffucaltyDomain = await walkDiffucaltyRepository.DeleteAsync(id);
            if (walkDiffucaltyDomain == null)
            {
                return BadRequest();
            }
            //Domain to DTO
            var walkDiffucaltyDeleteDomain = mapper.Map<Models.DTO.WalkDifficulty>(walkDiffucaltyDomain);
            //REturn Responcre


            return Ok();

        }

        #region private methods
        private bool ValidateaddwalkDiffucaltyAsync(Models.DTO.AddWalkDiffucaltyRequest addWalkDiffucaltyRequest)
        {
            if (addWalkDiffucaltyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDiffucaltyRequest), $"{nameof(addWalkDiffucaltyRequest)}need to fill all boxx ");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addWalkDiffucaltyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDiffucaltyRequest.Code), $"{nameof(addWalkDiffucaltyRequest.Code)} need to be  fill ");
                return false;
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;

        }

        private bool ValidateUpdateWalkDiffucaltyAsync(Models.DTO.UpdateWalkDiffucaltyRequest updateWalkDiffucaltyRequest)
        {
            if (updateWalkDiffucaltyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDiffucaltyRequest), $"{nameof(updateWalkDiffucaltyRequest)}need to fill all boxx ");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateWalkDiffucaltyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDiffucaltyRequest.Code), $"{nameof(updateWalkDiffucaltyRequest.Code)} need to be  fill ");
                return false;
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

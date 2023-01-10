using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.ComponentModel;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetAllRegionsAsync()
        {
            //Region Domain Model 
            var region = await regionRepository.GetAllAsync();
            //// return  region DTOs
            //var regionsDTO = new List<Models.DTO.Region>();
            //region.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Code2 = region.Code,
            //        Long = region.Long,
            //        Lat = region.Lat,
            //        Population = region.Population,
            //    };
            //    regionsDTO.Add(regionDTO);
            //});
            var regionDTO = mapper.Map<List<Models.Domain.Region>>(region);

            return Ok(regionDTO);

        }

        [HttpGet]
        [Route("{Id:Guid}")]
        [ActionName("GetRegionAsync")]
        //[Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetRegionAsync(Guid Id)
        {
            //Get Walk Domain OBject From Database 
            var Region = await regionRepository.GetAsync(Id);
            if (Region == null)
            {
                return NotFound(Id);
            }
            //Convert domain to DTO 
            var RegionDTO = mapper.Map<Models.DTO.Region>(Region);
            return Ok(RegionDTO);

        }

        [HttpPost]
        [Authorize]
        //[Authorize(Roles = "Write")]
        public async Task<IActionResult> AddRegionAsync(AddRegionrequest addRegionrequest)
        {
            //Validate the Request 168 to 213

            //if (!ValidateAddRegionAsync(addRegionrequest))
            //{
            //    return BadRequest(ModelState);
            //}

            //Request(DTO) to Domain model 
            var region = new Models.Domain.Region()
            {
                Code = addRegionrequest.Code2,
                Area = addRegionrequest.Area,
                Long = addRegionrequest.Long,
                Lat = addRegionrequest.Lat,
                Name = addRegionrequest.Name,
                Population = addRegionrequest.Population,
            };
            //Pass Details to Repository
            region = await regionRepository.AddAsync(region);
            //Convert Region Back To DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code2 = region.Code,
                Area = region.Area,
                Long = region.Long,
                Lat = region.Lat,
                Name = region.Name,
                Population = region.Population,
            };
            //hear we are geeting newly create recource (Location )
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }
        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Write")]

        public async Task<IActionResult> DeleteRegionAsync(Guid Id)
        {
            //Get Region From Database
            var region = await regionRepository.DeleteAsync(Id);

            //If not Found
            if (region == null)
            {
                return BadRequest();
            }

            //Convort back To DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code2 = region.Code,
                Area = region.Area,
                Long = region.Long,
                Lat = region.Lat,
                Name = region.Name,
                Population = region.Population
            };
            //Return Ok 
            return Ok(regionDTO);
        }
        [HttpPut]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Write")]

        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid Id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequestregion)
        {
            //validate incoming Request 
            if (!ValidateupdateRegionRequestregion(updateRegionRequestregion))
                {
                return BadRequest(ModelState);
            }
            //Convert DTO To Domain Model 
            //Domain Model Is Ready 
            var region = new Models.Domain.Region()
            {
                Name = updateRegionRequestregion.Name,
                Lat = updateRegionRequestregion.Lat,
                Long = updateRegionRequestregion.Long,
                Population = updateRegionRequestregion.Population,
                Area = updateRegionRequestregion.Area,
            };
            // Update region Using Reositry
            var regions = await regionRepository.UpdateAsync(Id, region);
            //If Null then Not Found 
            if (region == null)
            {
                return BadRequest();
            }
            //Convert Back To DTO
            var regionDTO = new Models.DTO.Region()
            {
                Name = regions.Name,
                Lat = regions.Lat,
                Long = regions.Long,
                Population = regions.Population,
                Area = regions.Area,
            };
            return Ok();
        }

        #region Private methods


        private bool ValidateAddRegionAsync(Models.DTO.AddRegionrequest addRegionrequest)
        {
            if (addRegionrequest == null)
            {

                ModelState.AddModelError(nameof(addRegionrequest), $"This  Cannot be Null ,Emplty ");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addRegionrequest.Code2))
            {

                ModelState.AddModelError(nameof(addRegionrequest.Code2), $"{nameof(addRegionrequest.Code2)} This  Cannot be Null ,Emplty or White Space ");
            }
            if (string.IsNullOrWhiteSpace(addRegionrequest.Name))
            {

                ModelState.AddModelError(nameof(addRegionrequest.Name), $"{nameof(addRegionrequest.Name)} This  Cannot be Null ,Emplty or White Space ");
            }
            if (addRegionrequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionrequest.Area), $"{nameof(addRegionrequest.Area)} This  Cannot be Lessthen Or == 0 Or Empty  , ");

            }
            if (addRegionrequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(addRegionrequest.Lat), $"{nameof(addRegionrequest.Lat)} This  Cannot be Lessthen Or == 0 Or Empty , ");

            }
            if (addRegionrequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegionrequest.Long), $"{nameof(addRegionrequest.Long)} This  Cannot be Lessthen Or == 0 Or Empty , ");

            }
            if (addRegionrequest.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionrequest.Population), $"{nameof(addRegionrequest.Population)}  Popoulation Cna not be Empty , ");

            }
            if(ModelState.ErrorCount>0) {

                return false;
                    
                    }
            return true;

        }
        //Valoidation For Update 
        private bool ValidateupdateRegionRequestregion(Models.DTO.UpdateRegionRequest updateRegionRequestregion)
        {
            if (updateRegionRequestregion == null)
            {

                ModelState.AddModelError(nameof(updateRegionRequestregion), $"This  Cannot be Null ,Emplty ");
                return false;
            }
            //if (string.IsNullOrWhiteSpace(updateRegionRequestregion.))
            //{

            //    ModelState.AddModelError(nameof(updateRegionRequestregion.Code), $"{nameof(UpdateRegionAsync.Code2)} This  Cannot be Null ,Emplty or White Space ");
            //}
            if (string.IsNullOrWhiteSpace(updateRegionRequestregion.Name))
            {

                ModelState.AddModelError(nameof(updateRegionRequestregion.Name), $"{nameof(updateRegionRequestregion.Name)} This  Cannot be Null ,Emplty or White Space ");
            }
            if (updateRegionRequestregion.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequestregion.Area), $"{nameof(updateRegionRequestregion.Area)} This  Cannot be Lessthen Or == 0 Or Empty  , ");

            }
            if (updateRegionRequestregion.Lat <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequestregion.Lat), $"{nameof(updateRegionRequestregion.Lat)} This  Cannot be Lessthen Or == 0 Or Empty , ");

            }
            if (updateRegionRequestregion.Long <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequestregion.Long), $"{nameof(updateRegionRequestregion.Long)} This  Cannot be Lessthen Or == 0 Or Empty , ");

            }
            if (updateRegionRequestregion.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequestregion.Population), $"{nameof(updateRegionRequestregion.Population)}  Popoulation Cna not be Empty , ");
                    
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


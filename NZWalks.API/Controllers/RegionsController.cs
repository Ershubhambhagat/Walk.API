using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

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
        public async Task<IActionResult> AddRegionAsync(AddRegionrequest addRegionrequest)
        {
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
        public async Task<IActionResult> DeleteRegionAsync(Guid Id)
        {
            //Get Region From Database
            var region= await regionRepository.DeleteAsync(Id);

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
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid Id,[FromBody] Models.DTO.UpdateRegionRequest updateRegionRequestregion)
        {
            //Convert DTO To Domain Model 
            //Domain Model Is Ready 
            var region = new Models.Domain.Region()
            {
                Name = updateRegionRequestregion.Name,
                Lat= updateRegionRequestregion.Lat,
                Long = updateRegionRequestregion.Long,
                Population= updateRegionRequestregion.Population,
                Area= updateRegionRequestregion.Area,
            };

            // Update region Using Reositry
            var regions = await regionRepository.UpdateAsync(Id, region);

            //If Null then Not Found 
            if(region== null)
            {
                return BadRequest();
            }

            //Convert Back To DTO
            var regionDTO = new Models.DTO.Region()
            { 
                Name= regions.Name,
                Lat= regions.Lat,
                Long= regions.Long,
                Population= regions.Population,
                Area= regions.Area,

            };

            return Ok();

            //Return Ok 

        }

    }
}

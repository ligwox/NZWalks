using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllRegionsAsync() {
            var regions = await _regionRepository.GetAllAsync();
            var regionsDTO = _mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionByIdAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetRegionByIdAsync(Guid id) {
            var region = await _regionRepository.GetAsync(id);

            if(region == null) 
                return NotFound();
                
            var regionDTO = _mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }
        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest) {

            var region = new Models.Domain.Region() {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population
            };
            region = await _regionRepository.AddAsync(region);
            var regionDTO = new Models.DTO.Region {
                Code = region.Code,
                Id = region.Id,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegionByIdAsync), new { id = regionDTO.Id }, regionDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id) { 
            var region = await _regionRepository.DeleteAsync(id);
            
            if (region == null) { 
                return NotFound();
            }

            var regionDTO = _mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest) {        
            
            var region = new Models.Domain.Region() {
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Code = updateRegionRequest.Code,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population
            };
            region = await _regionRepository.UpdateAsync(id, region);
            if (region == null) {
                return NotFound();
            }

            var regionDTO = new Models.DTO.Region()
            {
                Code = region.Code,
                Id = region.Id,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            return Ok(regionDTO);
        }

        //#region Private methods

        //private bool ValidateAddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest) {
            
        //    if (addRegionRequest == null) {
        //        ModelState.AddModelError(nameof(addRegionRequest), "Add region data is required.");
        //        return false;
        //    }
        //    if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
        //    {
        //        ModelState.AddModelError(nameof(addRegionRequest.Code), 
        //            $"{nameof(addRegionRequest.Code)} Can not be null or white space.");

        //    }
        //    if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
        //    {
        //        ModelState.AddModelError(nameof(addRegionRequest.Name),
        //            $"{nameof(addRegionRequest.Name)} Can not be null or white space.");
        //    }
        //    if (addRegionRequest.Area <= 0)
        //    {
        //        ModelState.AddModelError(nameof(addRegionRequest.Area),
        //            $"{nameof(addRegionRequest.Area)} Can not be less than or equal to zero.");
        //    }
        //    if (addRegionRequest.Lat <= 0)
        //    {
        //        ModelState.AddModelError(nameof(addRegionRequest.Lat),
        //            $"{nameof(addRegionRequest.Lat)} Can not be less than or equal to zero.");
        //    }
        //    if (addRegionRequest.Long <= 0)
        //    {
        //        ModelState.AddModelError(nameof(addRegionRequest.Long),
        //            $"{nameof(addRegionRequest.Long)} Can not be less than or equal to zero.");
        //    }
        //    if (addRegionRequest.Population < 0)
        //    {
        //        ModelState.AddModelError(nameof(addRegionRequest.Population),
        //            $"{nameof(addRegionRequest.Population)} Can not be less than zero.");
        //    }

        //    if(ModelState.ErrorCount > 0)
        //        return false;
        //    return true;
        //}

        //private bool ValidateUpdateRegionAsync(Models.DTO.UpdateRegionRequest updateRegionRequest)
        //{
        //    if (updateRegionRequest == null)
        //    {
        //        ModelState.AddModelError(nameof(updateRegionRequest), "Update region data is required.");
        //        return false;
        //    }
        //    if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
        //    {
        //        ModelState.AddModelError(nameof(updateRegionRequest.Code),
        //            $"{nameof(updateRegionRequest.Code)} Can not be null or white space.");

        //    }
        //    if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
        //    {
        //        ModelState.AddModelError(nameof(updateRegionRequest.Name),
        //            $"{nameof(updateRegionRequest.Name)} Can not be null or white space.");
        //    }
        //    if (updateRegionRequest.Area <= 0)
        //    {
        //        ModelState.AddModelError(nameof(updateRegionRequest.Area),
        //            $"{nameof(updateRegionRequest.Area)} Can not be less than or equal to zero.");
        //    }
        //    if (updateRegionRequest.Lat <= 0)
        //    {
        //        ModelState.AddModelError(nameof(updateRegionRequest.Lat),
        //            $"{nameof(updateRegionRequest.Lat)} Can not be less than or equal to zero.");
        //    }
        //    if (updateRegionRequest.Long <= 0)
        //    {
        //        ModelState.AddModelError(nameof(updateRegionRequest.Long),
        //            $"{nameof(updateRegionRequest.Long)} Can not be less than or equal to zero.");
        //    }
        //    if (updateRegionRequest.Population < 0)
        //    {
        //        ModelState.AddModelError(nameof(updateRegionRequest.Population),
        //            $"{nameof(updateRegionRequest.Population)} Can not be less than zero.");
        //    }

        //    if (ModelState.ErrorCount > 0)
        //        return false;
        //    return true;
        //}
        //#endregion
    }
}

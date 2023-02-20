using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Data;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Walks")]
    public class WalksController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IRegionRepository _regionRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository, 
            IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
            _regionRepository = regionRepository;
            _walkDifficultyRepository = walkDifficultyRepository;
        }

        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllWalksAsync() { 
            var walksResponse = await _walkRepository.GetAllAsync();
            if(walksResponse == null)
                return NotFound();
            var walksDTO = _mapper.Map<List<Models.DTO.Walks>>(walksResponse);
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkByIdAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetWalkByIdAsync(Guid id)
        {
            var walksResponse = await _walkRepository.GetAsync(id);

            if (walksResponse == null)
                return NotFound();

            var walk = _mapper.Map<Models.DTO.Walks>(walksResponse);
            return Ok(walk);
        }
        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            if (!await ValidateAddWalkAsync(addWalkRequest))
                return BadRequest(ModelState);

            var walk = new Models.Domain.Walks()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
                RegionId = addWalkRequest.RegionId,
            };
            var responseWalk = await _walkRepository.AddAsync(walk);

            var walkDTO = new Models.DTO.Walks()
            {
                Id = responseWalk.Id,
                Name = responseWalk.Name,
                Length = responseWalk.Length,
                WalkDifficultyId = responseWalk.WalkDifficultyId,
                RegionId = responseWalk.RegionId,
            };
            return CreatedAtAction(nameof(GetWalkByIdAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walkResponse = await _walkRepository.DeleteAsync(id);
            if (walkResponse == null)
                return NotFound();

            var walkDTO = _mapper.Map<Models.DTO.Walks>(walkResponse);
            return Ok(walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            if (!await ValidateUpdateWalkAsync(updateWalkRequest))
                return BadRequest(ModelState);

            var walk = new Models.Domain.Walks()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            var walkResponse = await _walkRepository.UpdateAsync(id, walk);

            if (walkResponse == null)
                return NotFound();

            var walkDTO = new Models.DTO.Walks()
            {
                Id = walkResponse.Id,
                Name = walkResponse.Name,
                Length = walkResponse.Length,
                WalkDifficultyId = walkResponse.WalkDifficultyId,
                RegionId = walkResponse.RegionId,
            };

            return Ok(walkDTO);
        }

        #region Private methods

        private async Task<bool> ValidateAddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest) {
            
            var region = await _regionRepository.GetAsync(addWalkRequest.RegionId);
            if (region == null) {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId),
                    $"{nameof(addWalkRequest.RegionId)} is invalid");
            }
            var walkDifficulty = await _walkDifficultyRepository.GetAsync(addWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null) {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId),
                    $"{nameof(addWalkRequest.WalkDifficultyId)} is invalid");
            }

            if (ModelState.ErrorCount > 0)
                return false;
            return true;
        }

        private async Task<bool> ValidateUpdateWalkAsync(Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            if (await _regionRepository.GetAsync(updateWalkRequest.RegionId) == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId),
                    $"{nameof(updateWalkRequest.RegionId)} is invalid");
            }
            if (await _walkDifficultyRepository.GetAsync(updateWalkRequest.WalkDifficultyId) == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId),
                    $"{nameof(updateWalkRequest.WalkDifficultyId)} is invalid");
            }

            if (ModelState.ErrorCount > 0)
                return false;
            return true;
        }

        #endregion
    }
}

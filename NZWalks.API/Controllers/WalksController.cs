using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Walks")]
    public class WalksController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }
        [HttpGet]
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
        public async Task<IActionResult> GetWalkByIdAsync(Guid id)
        {
            var walksResponse = await _walkRepository.GetAsync(id);

            if (walksResponse == null)
                return NotFound();

            var walk = _mapper.Map<Models.DTO.Walks>(walksResponse);
            return Ok(walk);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
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
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
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

    }
}

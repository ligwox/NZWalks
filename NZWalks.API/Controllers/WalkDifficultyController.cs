using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("WalkDifficulty")]
    public class WalkDifficultyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;

        public WalkDifficultyController(IMapper mapper, IWalkDifficultyRepository walkDifficultyRepository)
        {
            _walkDifficultyRepository = walkDifficultyRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync() { 
            var walkDifficulties = await _walkDifficultyRepository.GetAllAsync();

            if (walkDifficulties == null)
                return NotFound();

            var walkDiffictultiesDTO = _mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficulties);

            return Ok(walkDiffictultiesDTO);
        }
        
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyByIdAsync")]
        public async Task<IActionResult> GetWalkDifficultyByIdAsync(Guid id)
        {
            var walkDifficulty = await _walkDifficultyRepository.GetAsync(id);

            if (walkDifficulty == null)
                return NotFound();

            var walkDifficultyDTO = _mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);
            return Ok(walkDifficultyDTO);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {

            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code
            };
            var walkDifficultyResponse = await _walkDifficultyRepository.AddAsync(walkDifficulty);

            var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Code = walkDifficultyResponse.Code,
                Id = walkDifficultyResponse.Id
            };
            return CreatedAtAction(nameof(GetWalkDifficultyByIdAsync), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code
            };
            var walkDifficultyResponse = await _walkDifficultyRepository.UpdateAsync(id, walkDifficulty);

            if (walkDifficultyResponse == null)
                return NotFound();

            var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Code = walkDifficultyResponse.Code,
                Id = walkDifficultyResponse.Id
            };

            return Ok(walkDifficultyDTO);
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await _walkDifficultyRepository.DeleteAsync(id);
            if (walkDifficulty == null)
                return NotFound();
            var walkDifficultyDTO = _mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);
            return Ok(walkDifficultyDTO);
        }
    }
}

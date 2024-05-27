using AutoMapper;
using BCS.API.Dtos;
using BCS.Core.Entities;
using BCS.Repositories.Streets;
using Microsoft.AspNetCore.Mvc;

namespace BCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreetController : Controller
    {
        private readonly IStreetRepository _streetRepository;
        private readonly IMapper _mapper;

        public StreetController(
            IStreetRepository streetRepository,
            IMapper mapper)
        {
            _streetRepository = streetRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStreets()
        {
            var streets = await _streetRepository.GetAllAsync();
            var streetDtos = _mapper.Map<List<StreetUpdateDto>>(streets);
            return Ok(streetDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStreet([FromBody] StreetCreateDto model)
        {
            var entity = _mapper.Map<Street>(model);
            await _streetRepository.CreateAsync(entity);
            return CreatedAtAction(nameof(GetStreetById), new { id = entity.Id }, entity);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStreetById(Guid id)
        {
            var entity = await _streetRepository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStreet(Guid id, [FromBody] StreetUpdateDto model)
        {
            var entity = _mapper.Map<Street>(model);
            entity.Id = id;
            await _streetRepository.UpdateAsync(entity);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStreet(Guid id)
        {
            await _streetRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

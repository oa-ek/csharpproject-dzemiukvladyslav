using AutoMapper;
using BCS.API.Dtos;
using BCS.Core.Entities;
using BCS.Repositories.Structures;
using Microsoft.AspNetCore.Mvc;

namespace BCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StructureController : Controller
    {
        private readonly IStructureRepository _structureRepository;
        private readonly IMapper _mapper;

        public StructureController(
            IStructureRepository structureRepository,
            IMapper mapper)
        {
            _structureRepository = structureRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStructures()
        {
            var structures = await _structureRepository.GetAllAsync();
            var structureDtos = _mapper.Map<List<StructureUpdateDto>>(structures);
            return Ok(structureDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStructure([FromBody] StructureCreateDto model)
        {
            var entity = _mapper.Map<Structure>(model);
            await _structureRepository.CreateAsync(entity);
            return CreatedAtAction(nameof(GetStructureById), new { id = entity.Id }, entity);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStructureById(Guid id)
        {
            var entity = await _structureRepository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStructure(Guid id, [FromBody] StructureUpdateDto model)
        {
            var entity = _mapper.Map<Structure>(model);
            entity.Id = id;
            await _structureRepository.UpdateAsync(entity);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStructure(Guid id)
        {
            await _structureRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

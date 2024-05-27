using AutoMapper;
using BCS.API.Dtos;
using BCS.Repositories.Types;
using Microsoft.AspNetCore.Mvc;

namespace BCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : Controller
    {
        private readonly ITypeRepository _typeRepository;
        private readonly IMapper _mapper;

        public TypeController(
            ITypeRepository typeRepository,
            IMapper mapper)
        {
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTypes()
        {
            var types = await _typeRepository.GetAllAsync();
            var typeDtos = _mapper.Map<List<TypeUpdateDto>>(types);
            return Ok(typeDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateType([FromBody] TypeCreateDto model)
        {
            var entity = _mapper.Map<Core.Entities.Type>(model);
            await _typeRepository.CreateAsync(entity);
            return CreatedAtAction(nameof(GetTypeById), new { id = entity.Id }, entity);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeById(Guid id)
        {
            var entity = await _typeRepository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateType(Guid id, [FromBody] TypeUpdateDto model)
        {
            var entity = _mapper.Map<BCS.Core.Entities.Type>(model);
            entity.Id = id;
            await _typeRepository.UpdateAsync(entity);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(Guid id)
        {
            await _typeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

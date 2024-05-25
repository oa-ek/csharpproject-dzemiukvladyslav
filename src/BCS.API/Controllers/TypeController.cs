using AutoMapper;
using BCS.API.Dtos;
using BCS.Repositories.Types;
/*using Microsoft.AspNetCore.Identity;*/
using Microsoft.AspNetCore.Mvc;

namespace BCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : Controller
    {
        private readonly ITypeRepository _typeRepository;
        /*private readonly UserManager<AppUser> _userManager;*/
        private readonly IMapper _mapper;

        public TypeController(
            ITypeRepository typeRepository,
            /*UserManager<AppUser> userManager,*/
            IMapper mapper)
        {
            _typeRepository = typeRepository;
            /*_userManager = userManager;*/
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTypes()
        {
            var types = await _typeRepository.GetAllAsync();
            return Ok(types);
        }

        [HttpPost]
        public async Task<IActionResult> CreateType([FromBody] TypeCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<BCS.Core.Entities.Type>(model);
            if (User.Identity.IsAuthenticated)
            {
                await _typeRepository.CreateAsync(entity);
                return CreatedAtAction(nameof(GetTypeById), new { id = entity.Id }, entity);
            }

            return Unauthorized();
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<BCS.Core.Entities.Type>(model);
            entity.Id = id;
            await _typeRepository.UpdateAsync(entity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(Guid id)
        {
            await _typeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

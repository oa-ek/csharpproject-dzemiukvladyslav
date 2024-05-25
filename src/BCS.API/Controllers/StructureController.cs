using AutoMapper;
using BCS.API.Dtos;
using BCS.Core.Entities;
using BCS.Repositories.Structures;
/*using Microsoft.AspNetCore.Identity;*/
using Microsoft.AspNetCore.Mvc;

namespace BCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StructureController : Controller
    {
        private readonly IStructureRepository _structureRepository;
        /*private readonly UserManager<AppUser> _userManager;*/
        private readonly IMapper _mapper;

        public StructureController(
            IStructureRepository structureRepository,
            /*UserManager<AppUser> userManager,*/
            IMapper mapper)
        {
            _structureRepository = structureRepository;
            /*_userManager = userManager;*/
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetStructures()
        {
            var structures = await _structureRepository.GetAllAsync();
            return Ok(structures);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStructure([FromBody] StructureCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Structure>(model);
            if (User.Identity.IsAuthenticated)
            {
                await _structureRepository.CreateAsync(entity);
                return CreatedAtAction(nameof(GetStructureById), new { id = entity.Id }, entity);
            }

            return Unauthorized();
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Structure>(model);
            entity.Id = id;
            await _structureRepository.UpdateAsync(entity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStructure(Guid id)
        {
            await _structureRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

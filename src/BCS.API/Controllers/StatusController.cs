using AutoMapper;
using BCS.API.Dtos;
using BCS.Core.Entities;
using BCS.Repositories.Statuses;
/*using Microsoft.AspNetCore.Identity;*/
using Microsoft.AspNetCore.Mvc;

namespace BCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IStatusRepository _statusRepository;
        /*private readonly UserManager<AppUser> _userManager;*/
        private readonly IMapper _mapper;

        public StatusController(
            IStatusRepository statusRepository,
            /*UserManager<AppUser> userManager,*/
            IMapper mapper)
        {
            _statusRepository = statusRepository;
            /*_userManager = userManager;*/
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetStatuses()
        {
            var statuses = await _statusRepository.GetAllAsync();
            return Ok(statuses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] StatusCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Status>(model);
            if (User.Identity.IsAuthenticated)
            {
                await _statusRepository.CreateAsync(entity);
                return CreatedAtAction(nameof(GetStatusById), new { id = entity.Id }, entity);
            }

            return Unauthorized();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusById(Guid id)
        {
            var entity = await _statusRepository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] StatusUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Status>(model);
            entity.Id = id;
            await _statusRepository.UpdateAsync(entity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(Guid id)
        {
            await _statusRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

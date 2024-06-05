using AutoMapper;
using BCS.API.Dtos;
using BCS.Core.Entities;
using BCS.Repositories.Statuses;
using Microsoft.AspNetCore.Mvc;

namespace BCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusController(
            IStatusRepository statusRepository,
            IMapper mapper)
        {
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetStatuses()
        {
            var statuses = await _statusRepository.GetAllAsync();
            var statusDtos = _mapper.Map<List<StatusUpdateDto>>(statuses);
            return Ok(statusDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] StatusCreateDto model)
        {
            var entity = _mapper.Map<Status>(model);
            await _statusRepository.CreateAsync(entity);
            return CreatedAtAction(nameof(GetStatusById), new { id = entity.Id }, entity);
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
            var entity = _mapper.Map<Status>(model);
            entity.Id = id;
            await _statusRepository.UpdateAsync(entity);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(Guid id)
        {
            await _statusRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

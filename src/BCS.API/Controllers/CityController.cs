using AutoMapper;
using BCS.API.Dtos;
using BCS.Core.Entities;
using BCS.Repositories.Cityes;
using Microsoft.AspNetCore.Mvc;

namespace BCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityController(
            ICityRepository cityRepository,
            IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _cityRepository.GetAllAsync();
            var cityDtos = _mapper.Map<List<CityUpdateDto>>(cities);
            return Ok(cityDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityCreateDto model)
        {

            var entity = _mapper.Map<City>(model);
            await _cityRepository.CreateAsync(entity);
            return CreatedAtAction(nameof(GetCityById), new { id = entity.Id }, entity);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(Guid id)
        {
            var entity = await _cityRepository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(Guid id, [FromBody] CityUpdateDto model)
        {
            var entity = _mapper.Map<City>(model);
            entity.Id = id;
            await _cityRepository.UpdateAsync(entity);

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            await _cityRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

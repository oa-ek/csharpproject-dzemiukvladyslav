using AutoMapper;
using BCS.API.Dtos;
using BCS.Core.Entities;
using BCS.Repositories.Cityes;
using BCS.Repositories.Statuses;
using BCS.Repositories.Streets;
using BCS.Repositories.Structures;
using BCS.Repositories.Suggestions;
using BCS.Repositories.Types;
using Microsoft.AspNetCore.Mvc;

namespace BCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : Controller
    {
        private readonly ISuggestionRepository _suggestionRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStreetRepository _streetRepository;
        private readonly IStructureRepository _structureRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly BCS.API.FileService.IFileService _fileService;
        private readonly IMapper _mapper;

        public SuggestionController(
            ISuggestionRepository suggestionRepository,
            ITypeRepository typeRepository,
            IStatusRepository statusRepository,
            ICityRepository cityRepository,
            IStreetRepository streetRepository,
            IStructureRepository structureRepository,
            IWebHostEnvironment webHostEnvironment,
            FileService.IFileService fileService,
            IMapper mapper)
        {
            _suggestionRepository = suggestionRepository;
            _typeRepository = typeRepository;
            _statusRepository = statusRepository;
            _cityRepository = cityRepository;
            _streetRepository = streetRepository;
            _structureRepository = structureRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuggestions()
        {
            var suggestions = await _suggestionRepository.GetAllAsync();
            var suggestionDtos = _mapper.Map<List<SuggestionUpdateDto>>(suggestions);
            return Ok(suggestionDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSuggestionById(Guid id)
        {
            var entity = await _suggestionRepository.GetAsync(id);
            var entityDtos = _mapper.Map<SuggestionUpdateDto>(entity);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entityDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SuggestionCreateDto entity)
        {
            if (entity.PhotoIMG != null)
            {
                var fileResult = _fileService.SaveIFormFile(entity.PhotoIMG);
                if (fileResult.Contains("Only") || fileResult.Contains("Error"))
                    return BadRequest(fileResult);
                else
                    entity.Photo = fileResult;
                await _suggestionRepository.CreateAsync(_mapper.Map<Suggestion>(entity));
                return Ok(entity);
            }
            var model = _mapper.Map<Suggestion>(entity);
            model.Sdatetime = DateTime.Now;

            await _suggestionRepository.CreateAsync(model);
            return CreatedAtAction(nameof(GetSuggestionById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromForm] SuggestionUpdateDto entity)
        {
            if (entity.PhotoIMG != null)
            {
                _fileService.DeleteImage(entity.Photo);
                var fileResult = _fileService.SaveIFormFile(entity.PhotoIMG);
                if (fileResult.Contains("Only") || fileResult.Contains("Error"))
                    return BadRequest(fileResult);
                else
                    entity.Photo = fileResult;
            }
            var model = _mapper.Map<Suggestion>(entity);
            model.Id = id;

            await _suggestionRepository.UpdateAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _suggestionRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

using AutoMapper;
using BCS.API.Dtos;
using BCS.Core.Entities;
using BCS.Repositories.Cityes;
using BCS.Repositories.Complaints;
using BCS.Repositories.Statuses;
using BCS.Repositories.Streets;
using BCS.Repositories.Structures;
using BCS.Repositories.Types;
using Microsoft.AspNetCore.Mvc;

namespace BCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : Controller
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStreetRepository _streetRepository;
        private readonly IStructureRepository _structureRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly BCS.API.FileService.IFileService _fileService;
        private readonly IMapper _mapper;

        public ComplaintController(
            IComplaintRepository complaintRepository,
            ITypeRepository typeRepository,
            IStatusRepository statusRepository,
            ICityRepository cityRepository,
            IStreetRepository streetRepository,
            IStructureRepository structureRepository,
            IWebHostEnvironment webHostEnvironment,
            FileService.IFileService fileService,
            IMapper mapper)
        {
            _complaintRepository = complaintRepository;
            _typeRepository = typeRepository;
            _statusRepository = statusRepository;
            _cityRepository = cityRepository;
            _streetRepository = streetRepository;
            _structureRepository = structureRepository;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetComplaints()
        {
            var complaints = await _complaintRepository.GetAllAsync();
            var complaintDtos = _mapper.Map<List<ComplaintUpdateDto>>(complaints);
            return Ok(complaintDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComplaintById(Guid id)
        {
            var entity = await _complaintRepository.GetAsync(id);
            var entityDtos = _mapper.Map<ComplaintUpdateDto>(entity);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entityDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ComplaintCreateDto entity)
        {
            if (entity.PhotoIMG != null)
            {
                var fileResult = _fileService.SaveIFormFile(entity.PhotoIMG);
                if (fileResult.Contains("Only") || fileResult.Contains("Error"))
                    return BadRequest(fileResult);
                else
                    entity.Photo = fileResult;
                await _complaintRepository.CreateAsync(_mapper.Map<Complaint>(entity));
                return Ok(entity);
            }
            var model = _mapper.Map<Complaint>(entity);
            model.Sdatetime = DateTime.Now;

            await _complaintRepository.CreateAsync(model);
            return CreatedAtAction(nameof(GetComplaintById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromForm] ComplaintUpdateDto entity)
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
            var model = _mapper.Map<Complaint>(entity);
            model.Id = id;

            await _complaintRepository.UpdateAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _complaintRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

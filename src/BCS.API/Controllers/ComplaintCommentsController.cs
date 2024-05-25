using AutoMapper;
using BCS.API.Dtos;
using BCS.Core.Entities;
using BCS.Repositories.ComplaintCommentses;
using BCS.Repositories.Complaints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintCommentsController : Controller
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IComplaintCommentsRepository _complaintCommentsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ComplaintCommentsController(
            IComplaintRepository complaintRepository,
            IComplaintCommentsRepository complaintCommentsRepository,
            IWebHostEnvironment webHostEnvironment,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _complaintRepository = complaintRepository;
            _complaintCommentsRepository = complaintCommentsRepository;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("Index/{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var entity = await _complaintRepository.GetAsync(id);
            if (Guid.Empty != id)
            {
                HttpContext.Session.SetString("currentSession", id.ToString());
            }
            return Ok(entity.ComplaintCommentses.OrderByDescending(x => x.ComCommentData));
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ComplaintCommentsCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<ComplaintComments>(model);
            var currentUser = await _userManager.GetUserAsync(User);
            entity.ComplaintId = Guid.Parse(HttpContext.Session.GetString("currentSession"));
            entity.UserId = currentUser.Id;
            entity.ComCommentData = DateTime.Now;

            if (model.ComCommentPhoto != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                var fileExt = Path.GetExtension(model.ComCommentPhoto.FileName);
                var filePath = Path.Combine("/data/img/", $"{entity.Id}{fileExt}");
                string path = Path.Combine(wwwRootPath, "data\\img\\", $"{entity.Id}{fileExt}");

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.ComCommentPhoto.CopyToAsync(fileStream);
                }

                entity.ComCommentPhoto = filePath;
            }
            await _complaintCommentsRepository.CreateAsync(entity);
            return NoContent();
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _complaintCommentsRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

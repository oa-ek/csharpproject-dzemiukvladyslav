using AutoMapper;
using BCS.API.Dtos;
using BCS.Core.Entities;
using BCS.Repositories.SuggestionCommentses;
using BCS.Repositories.Suggestions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionCommentsController : Controller
    {
        private readonly ISuggestionRepository _suggestionRepository;
        private readonly ISuggestionCommentsRepository _suggestionCommentsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public SuggestionCommentsController(
            ISuggestionRepository suggestionRepository,
            ISuggestionCommentsRepository suggestionCommentsRepository,
            IWebHostEnvironment webHostEnvironment,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _suggestionRepository = suggestionRepository;
            _suggestionCommentsRepository = suggestionCommentsRepository;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("Index/{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var entity = await _suggestionRepository.GetAsync(id);
            if (Guid.Empty != id)
            {
                HttpContext.Session.SetString("currentSession", id.ToString());
            }
            return Ok(entity.SuggestionCommentses.OrderByDescending(x => x.SugCommentData));
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] SuggestionCommentsCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<SuggestionComments>(model);
            var currentUser = await _userManager.GetUserAsync(User);
            entity.SuggestionId = Guid.Parse(HttpContext.Session.GetString("currentSession"));
            entity.UserId = currentUser.Id;

            entity.SugCommentData = DateTime.Now;

            if (model.SugCommentPhoto != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                var fileExt = Path.GetExtension(model.SugCommentPhoto.FileName);
                var filePath = Path.Combine("/data/img/", $"{entity.Id}{fileExt}");
                string path = Path.Combine(wwwRootPath, "data\\img\\", $"{entity.Id}{fileExt}");

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.SugCommentPhoto.CopyToAsync(fileStream);
                }

                entity.SugCommentPhoto = filePath;
            }
            await _suggestionCommentsRepository.CreateAsync(entity);
            return NoContent();
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _suggestionCommentsRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

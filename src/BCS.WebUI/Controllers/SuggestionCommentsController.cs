using AutoMapper;
using BCS.Core.Entities;
using BCS.Repositories.SuggestionCommentses;
using BCS.Repositories.Suggestions;
using BCS.WebUI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCS.WebUI.Controllers
{
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

        public async Task<IActionResult> Index(Guid id)
        {
            var entity = await _suggestionRepository.GetAsync(id);
            if (Guid.Empty != id)
            {
                HttpContext.Session.SetString("currentSession", id.ToString());
            }
            return View(entity.SuggestionCommentses.OrderByDescending(x => x.SugCommentData));
        }

        public async Task<IActionResult> Create()
        {
            var suggestionCommentsCreateDto = new SuggestionCommentsCreateDto();
            return View(suggestionCommentsCreateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SuggestionCommentsCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
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
                    model.SugCommentPhoto.CopyTo(fileStream);
                }

                entity.SugCommentPhoto = filePath;
            }
            await _suggestionCommentsRepository.CreateAsync(entity);
            return RedirectToAction("Index", "Suggestion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _suggestionCommentsRepository.DeleteAsync(id);
            return RedirectToAction("Index", "Suggestion");
        }
    }
}

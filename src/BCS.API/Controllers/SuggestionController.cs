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
using Xceed.Document.NET;
using Xceed.Words.NET;

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
        /*private readonly UserManager<AppUser> _userManager;*/
        private readonly IMapper _mapper;

        public SuggestionController(
            ISuggestionRepository suggestionRepository,
            ITypeRepository typeRepository,
            IStatusRepository statusRepository,
            ICityRepository cityRepository,
            IStreetRepository streetRepository,
            IStructureRepository structureRepository,
            IWebHostEnvironment webHostEnvironment,
            /*UserManager<AppUser> userManager,*/
            IMapper mapper)
        {
            _suggestionRepository = suggestionRepository;
            _typeRepository = typeRepository;
            _statusRepository = statusRepository;
            _cityRepository = cityRepository;
            _streetRepository = streetRepository;
            _structureRepository = structureRepository;
            _webHostEnvironment = webHostEnvironment;
            /*_userManager = userManager;*/
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var suggestions = await _suggestionRepository.GetAllAsync();
            return Ok(suggestions);
        }

        [HttpGet("MyIndex")]
        public async Task<IActionResult> MyIndex()
        {
            /*var currentUser = await _userManager.GetUserAsync(User);
            var suggestions = await _suggestionRepository.GetAllByUserAsync(currentUser);*/
            return Ok(/*suggestions*/);
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] SuggestionCreateDto model)
        {
            var entity = _mapper.Map<Suggestion>(model);
            /*var currentUser = await _userManager.GetUserAsync(User);
            entity.User = currentUser;*/
            entity.Sdatetime = DateTime.Now;

            if (model.Photo != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                var fileExt = Path.GetExtension(model.Photo.FileName);
                var filePath = Path.Combine("/data/img/", $"{entity.Id}{fileExt}");
                string path = Path.Combine(wwwRootPath, "data\\img\\", $"{entity.Id}{fileExt}");

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }

                entity.Photo = filePath;
            }

            if (ModelState.IsValid)
            {
                await _suggestionRepository.CreateAsync(entity);
                return RedirectToAction("Index");
            }

            return BadRequest(ModelState);
        }

        [HttpPut("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [FromBody] SuggestionUpdateDto model)
        {
            var entity = await _suggestionRepository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _mapper.Map(model, entity);

            if (model.Sdatetime != null)
                entity.Sdatetime = DateTime.ParseExact(model.Sdatetime, @"dd.MM.yyyy HH:mm:ss", null).ToLocalTime();

            if (model.PhotoIMG != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                var fileExt = Path.GetExtension(model.PhotoIMG.FileName);
                var filePath = Path.Combine("/data/img/", $"{entity.Id}{fileExt}");
                string path = Path.Combine(wwwRootPath, "data\\img\\", $"{entity.Id}{fileExt}");

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.PhotoIMG.CopyToAsync(fileStream);
                }

                entity.Photo = filePath;
            }

            await _suggestionRepository.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var suggestion = await _suggestionRepository.GetAsync(id);
            if (suggestion == null)
            {
                return NotFound();
            }

            suggestion.Status = (await _statusRepository.GetAllAsync()).FirstOrDefault(x => x.Title == "Скасовано");
            await _suggestionRepository.UpdateAsync(suggestion);
            return RedirectToAction("Index");
        }

        [HttpGet("GenerateWordReport/{id}")]
        public async Task<IActionResult> GenerateWordReport(Guid id)
        {
            var suggestion = await _suggestionRepository.GetAsync(id);

            var fileExt = Path.GetExtension(suggestion.Photo);

            var wwwRootPath = _webHostEnvironment.WebRootPath;

            using (var doc = DocX.Create($"Звіт пропозиції від {suggestion.User.FullName}.docx"))
            {
                var titleFormat = new Formatting
                {
                    FontFamily = new Font("Times New Roman"),
                    Size = 18,
                    Bold = true
                };

                var titleParagraph = doc.InsertParagraph($"Звіт пропозиції від {suggestion.User.FullName}", false, titleFormat);
                titleParagraph.Alignment = Alignment.center;

                var titleTextFormat = new Formatting
                {
                    FontFamily = new Font("Times New Roman"),
                    Size = 14,
                    UnderlineStyle = UnderlineStyle.singleLine
                };

                var textFormat = new Formatting
                {
                    FontFamily = new Font("Times New Roman"),
                    Size = 14
                };

                doc.InsertParagraph("   ", false, textFormat);
                doc.InsertParagraph("   ", false, textFormat);
                var paragraph = doc.InsertParagraph("", false, textFormat);
                paragraph.Append($"Автор пропозиції:", titleTextFormat);
                paragraph.Append($" {suggestion.User.FullName}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var paragraph2 = doc.InsertParagraph("", false, textFormat);
                paragraph2.Append($"Email автора пропозиції:", titleTextFormat);
                paragraph2.Append($" {suggestion.User.UserName}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var paragraph3 = doc.InsertParagraph("", false, textFormat);
                paragraph3.Append($"Міська структура:", titleTextFormat);
                paragraph3.Append($" {suggestion.Structure.Title}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var paragraph4 = doc.InsertParagraph("", false, textFormat);
                paragraph4.Append($"Тип пропозиції:", titleTextFormat);
                paragraph4.Append($" {suggestion.Type.Title}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var paragraph5 = doc.InsertParagraph("", false, textFormat);
                paragraph5.Append($"Статус розгляду:", titleTextFormat);
                paragraph5.Append($" {suggestion.Status.Title}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var paragraph6 = doc.InsertParagraph("", false, textFormat);
                paragraph6.Append($"Дата та час створення:", titleTextFormat);
                paragraph6.Append($" {suggestion.Sdatetime}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var paragraph7 = doc.InsertParagraph("", false, textFormat);
                paragraph7.Append($"Адреса:", titleTextFormat);
                paragraph7.Append($" {suggestion.City.Title}, вул. {suggestion.Street.Title}, №. {suggestion.Number}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var descriptionTextParagraph = doc.InsertParagraph("Опис пропозиції", false, titleTextFormat);
                descriptionTextParagraph.Alignment = Alignment.center;
                doc.InsertParagraph($"{suggestion.Text}", false, textFormat);

                var filePath = Path.Combine("/data/img/", $"{suggestion.Id}{fileExt}");
                string path = Path.Combine(wwwRootPath, "data\\img\\", $"{suggestion.Id}{suggestion}");

                if (!string.IsNullOrEmpty(suggestion.Photo))
                {
                    doc.InsertParagraph("   ", false, textFormat);
                    var paragraph8 = doc.InsertParagraph("Фото", false, titleTextFormat);
                    paragraph8.Alignment = Alignment.center;
                    doc.InsertParagraph("   ", false, textFormat);

                    byte[] photoBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRootPath, "data", "img", $"{suggestion.Id}{fileExt}"));
                    Image image = doc.AddImage(new MemoryStream(photoBytes));
                    Picture picture = image.CreatePicture();

                    picture.Width = 500;
                    picture.Height = 300;

                    doc.InsertParagraph().InsertPicture(picture).Alignment = Alignment.center;
                }

                doc.InsertParagraph("   ", false, textFormat);
                doc.InsertParagraph("   ", false, textFormat);
                doc.InsertParagraph("   ", false, textFormat);
                var paragraph9 = doc.InsertParagraph("", false, textFormat);
                paragraph9.Append($"З повагою адміністрація BCS", textFormat).Alignment = Alignment.left;
                paragraph9.Append("\t\t\t\t\t\t").Append(DateTime.Now.ToString("dd.MM.yyyy"), textFormat);

                doc.Save();

                var memory = new MemoryStream();
                using (var stream = new FileStream($"Звіт пропозиції від {suggestion.User.FullName}.docx", FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"Звіт пропозиції від {suggestion.User.FullName}.docx");
            }
        }
    }
}

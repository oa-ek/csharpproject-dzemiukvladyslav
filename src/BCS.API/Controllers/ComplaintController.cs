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
using Xceed.Document.NET;
using Xceed.Words.NET;

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
        /*private readonly UserManager<AppUser> _userManager;*/
        private readonly IMapper _mapper;

        public ComplaintController(
            IComplaintRepository complaintRepository,
            ITypeRepository typeRepository,
            IStatusRepository statusRepository,
            ICityRepository cityRepository,
            IStreetRepository streetRepository,
            IStructureRepository structureRepository,
            IWebHostEnvironment webHostEnvironment,
            /*UserManager<AppUser> userManager,*/
            IMapper mapper)
        {
            _complaintRepository = complaintRepository;
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
            var complaints = await _complaintRepository.GetAllAsync();
            return Ok(complaints);
        }

        [HttpGet("MyIndex")]
        public async Task<IActionResult> MyIndex()
        {
            /*var currentUser = await _userManager.GetUserAsync(User);
            var complaints = await _complaintRepository.GetAllByUserAsync(currentUser);*/
            return Ok(/*complaints*/);
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ComplaintCreateDto model)
        {
            var entity = _mapper.Map<Complaint>(model);
            /*var currentUser = await _userManager.GetUserAsync(User);*/
            /*entity.User = currentUser;*/
            entity.Sdatetime = DateTime.Now;

            if (model.Photo is not null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                var fileExt = Path.GetExtension(model.Photo.FileName);
                var filePath = Path.Combine("/data/img/", $"{entity.Id}{fileExt}");
                string path = Path.Combine(wwwRootPath, "data\\img\\", $"{entity.Id}{fileExt}");

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }

                entity.Photo = filePath;
            }

            if (ModelState.IsValid)
            {
                await _complaintRepository.CreateAsync(entity);
                return RedirectToAction("Index");
            }

            return BadRequest(ModelState);
        }

        [HttpPut("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [FromBody] ComplaintUpdateDto model)
        {
            var entity = await _complaintRepository.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _mapper.Map(model, entity);

            if (model.PhotoIMG is not null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                var fileExt = Path.GetExtension(model.PhotoIMG.FileName);
                var filePath = Path.Combine("/data/img/", $"{entity.Id}{fileExt}");
                string path = Path.Combine(wwwRootPath, "data\\img\\", $"{entity.Id}{fileExt}");

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    model.PhotoIMG.CopyTo(fileStream);
                }

                entity.Photo = filePath;
            }

            await _complaintRepository.UpdateAsync(entity);

            return RedirectToAction("Index");
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var complaint = await _complaintRepository.GetAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }

            complaint.Status = (await _statusRepository.GetAllAsync()).FirstOrDefault(x => x.Title == "Скасовано");
            await _complaintRepository.UpdateAsync(complaint);

            return RedirectToAction("Index");
        }

        [HttpGet("GenerateWordReport/{id}")]
        public async Task<IActionResult> GenerateWordReport(Guid id)
        {
            var complaint = await _complaintRepository.GetAsync(id);

            var fileExt = Path.GetExtension(complaint.Photo);

            var wwwRootPath = _webHostEnvironment.WebRootPath;

            using (var doc = DocX.Create($"Звіт скарги від {complaint.User.FullName}.docx"))
            {
                var titleFormat = new Formatting
                {
                    FontFamily = new Font("Times New Roman"),
                    Size = 18,
                    Bold = true
                };

                var titleParagraph = doc.InsertParagraph($"Звіт скарги від {complaint.User.FullName}", false, titleFormat);
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
                paragraph.Append($"Автор скарги:", titleTextFormat);
                paragraph.Append($" {complaint.User.FullName}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var paragraph2 = doc.InsertParagraph("", false, textFormat);
                paragraph2.Append($"Email автора скарги:", titleTextFormat);
                paragraph2.Append($" {complaint.User.UserName}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var paragraph3 = doc.InsertParagraph("", false, textFormat);
                paragraph3.Append($"Міська структура:", titleTextFormat);
                paragraph3.Append($" {complaint.Structure.Title}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var paragraph4 = doc.InsertParagraph("", false, textFormat);
                paragraph4.Append($"Тип скарги:", titleTextFormat);
                paragraph4.Append($" {complaint.Type.Title}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var paragraph5 = doc.InsertParagraph("", false, textFormat);
                paragraph5.Append($"Статус розгляду:", titleTextFormat);
                paragraph5.Append($" {complaint.Status.Title}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var paragraph6 = doc.InsertParagraph("", false, textFormat);
                paragraph6.Append($"Дата та час створення:", titleTextFormat);
                paragraph6.Append($" {complaint.Sdatetime}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var paragraph7 = doc.InsertParagraph("", false, textFormat);
                paragraph7.Append($"Адреса:", titleTextFormat);
                paragraph7.Append($" {complaint.City.Title}, вул. {complaint.Street.Title}, №. {complaint.Number}", textFormat);

                doc.InsertParagraph("   ", false, textFormat);
                var descriptionTextParagraph = doc.InsertParagraph("Опис скарги", false, titleTextFormat);
                descriptionTextParagraph.Alignment = Alignment.center;
                doc.InsertParagraph($"{complaint.Text}", false, textFormat);

                var filePath = Path.Combine("/data/img/", $"{complaint.Id}{fileExt}");
                string path = Path.Combine(wwwRootPath, "data\\img\\", $"{complaint.Id}{complaint}");

                if (!string.IsNullOrEmpty(complaint.Photo))
                {
                    doc.InsertParagraph("   ", false, textFormat);
                    var paragraph8 = doc.InsertParagraph("Фото", false, titleTextFormat);
                    paragraph8.Alignment = Alignment.center;
                    doc.InsertParagraph("   ", false, textFormat);

                    byte[] photoBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(wwwRootPath, "data", "img", $"{complaint.Id}{fileExt}"));
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
                using (var stream = new FileStream($"Звіт скарги від {complaint.User.FullName}.docx", FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"Звіт скарги від {complaint.User.FullName}.docx");
            }
        }
    }
}

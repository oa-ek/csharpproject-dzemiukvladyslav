using AutoMapper;
using BCS.Core.Entities;
using BCS.Repositories.Cityes;
using BCS.Repositories.Complaints;
using BCS.Repositories.Statuses;
using BCS.Repositories.Streets;
using BCS.Repositories.Structures;
using BCS.Repositories.Types;
using BCS.WebUI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace BCS.WebUI.Controllers
{
    public class ComplaintController(
            IComplaintRepository complaintRepository,
            ITypeRepository typeRepository,
            IStatusRepository statusRepository,
            ICityRepository cityRepository,
            IStreetRepository streetRepository,
            IStructureRepository structureRepository,
            IWebHostEnvironment webHostEnvironment,
            UserManager<AppUser> userManager,
            IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index(
        string type = null,
        string status = null,
        string structure = null,
        DateTime? fromDate = null,
        DateTime? toDate = null)
        {
            var complaints = await complaintRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(type))
            {
                complaints = complaints.Where(s => s.Type.Id.ToString() == type);
            }
            if (!string.IsNullOrEmpty(status))
            {
                complaints = complaints.Where(s => s.Status.Id.ToString() == status);
            }
            if (!string.IsNullOrEmpty(structure))
            {
                complaints = complaints.Where(s => s.Structure.Id.ToString() == structure);
            }
            if (fromDate.HasValue && toDate.HasValue)
            {
                complaints = complaints.Where(s => s.Sdatetime >= fromDate && s.Sdatetime <= toDate);
            }

            ViewBag.Types = (await typeRepository.GetAllAsync())
                .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

            ViewBag.Statuses = (await statusRepository.GetAllAsync())
                .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

            ViewBag.Structures = (await structureRepository.GetAllAsync())
                .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

            return View(complaints);
        }

        public async Task<IActionResult> MyIndex()
        {
            var currentUser = await userManager.GetUserAsync(User);
            return View(await complaintRepository.GetAllByUserAsync(currentUser));
        }

        public async Task<IActionResult> Create()
        {
            var currentUser = await userManager.GetUserAsync(User);

            ViewBag.Types = (await typeRepository.GetAllAsync())
                .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

            ViewBag.Cities = (await cityRepository.GetAllAsync())
                .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

            ViewBag.Streets = (await streetRepository.GetAllAsync())
                .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

            ViewBag.Structures = (await structureRepository.GetAllAsync())
                .Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

            ViewBag.Statuses = (await statusRepository.GetAllAsync())
                .Select(x =>
                {
                    if (x.Title == "Новий")
                        return new SelectListItem { Text = x.Title, Value = x.Id.ToString(), Selected = true };
                    return new SelectListItem { Text = x.Title, Value = x.Id.ToString() };
                }).ToList();


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComplaintCreateDto model)
        {
            var entity = mapper.Map<Complaint>(model);
            var currentUser = await userManager.GetUserAsync(User);
            entity.User = currentUser;
            entity.Sdatetime = DateTime.Now;

            if (model.Photo is not null)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;

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
                if (User.Identity.IsAuthenticated)
                {
                    await complaintRepository.CreateAsync(entity);
                    return RedirectToAction("Index");
                }
            }

            return View(entity);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var entity = await complaintRepository.GetAsync(id);

            ViewBag.Types = (await typeRepository.GetAllAsync())
                .ToList();

            ViewBag.Cities = (await cityRepository.GetAllAsync())
                .ToList();

            ViewBag.Streets = (await streetRepository.GetAllAsync())
                .ToList();

            ViewBag.Structures = (await structureRepository.GetAllAsync())
                .ToList();

            ViewBag.Statuses = (await statusRepository.GetAllAsync())
                .ToList();

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ComplaintUpdateDto model)
        {
            var entity = mapper.Map<Complaint>(model);
            if (model.Sdatetime != null)
                entity.Sdatetime = DateTime.ParseExact(model.Sdatetime, @"dd.MM.yyyy HH:mm:ss", null).ToLocalTime();

            if (model.PhotoIMG is not null)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;

                var fileExt = Path.GetExtension(model.PhotoIMG.FileName);
                var filePath = Path.Combine("/data/img/", $"{entity.Id}{fileExt}");
                string path = Path.Combine(wwwRootPath, "data\\img\\", $"{entity.Id}{fileExt}");

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    model.PhotoIMG.CopyTo(fileStream);
                }

                entity.Photo = filePath;
            }

            await complaintRepository.UpdateAsync(entity);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            return View(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
        {
            var complaint = await complaintRepository.GetAsync(id);
            complaint.Status = (await statusRepository.GetAllAsync()).FirstOrDefault(x => x.Title == "Скасовано");
            await complaintRepository.UpdateAsync(complaint);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GenerateWordReport(Guid id)
        {
            var complaint = await complaintRepository.GetAsync(id);

            var fileExt = Path.GetExtension(complaint.Photo);

            var wwwRootPath = webHostEnvironment.WebRootPath;

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
                var paragraph10 = doc.InsertParagraph("", false, textFormat);
                paragraph10.Append($"Геолокація:", titleTextFormat);
                paragraph10.Append($"Широта {complaint.Latitude}, Довгота {complaint.Longitude}", textFormat);

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

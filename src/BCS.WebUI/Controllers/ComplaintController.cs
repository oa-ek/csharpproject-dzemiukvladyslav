﻿using AutoMapper;
using BCS.Core.Entities;
using BCS.Repositories.Cityes;
using BCS.Repositories.Complaints;
using BCS.Repositories.Statuses;
using BCS.Repositories.Streets;
using BCS.Repositories.Types;
using BCS.WebUI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BCS.WebUI.Controllers
{
    public class ComplaintController(
            IComplaintRepository complaintRepository,
            ITypeRepository typeRepository,
            IStatusRepository statusRepository,
            ICityRepository cityRepository,
            IStreetRepository streetRepository,
            IWebHostEnvironment webHostEnvironment,
            UserManager<AppUser> userManager,
            IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
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
            await complaintRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
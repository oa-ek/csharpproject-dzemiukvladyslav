using AutoMapper;
using BCS.Core.Entities;
using BCS.Repositories.Streets;
using BCS.WebUI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCS.WebUI.Controllers
{
    public class StreetController : Controller
    {
        private readonly IStreetRepository _streetRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public StreetController(
            IStreetRepository streetRepository,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _streetRepository = streetRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return View(await _streetRepository.GetAllByUserAsync(currentUser));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StreetCreateDto model)
        {
            var entity = _mapper.Map<Street>(model);
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    await _streetRepository.CreateAsync(entity);
                    return RedirectToAction("Index");
                }
            }

            return View(entity);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var entity = await _streetRepository.GetAsync(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StreetUpdateDto model)
        {
            var entity = _mapper.Map<Street>(model);

            await _streetRepository.UpdateAsync(entity);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            return View(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, int a)
        {
            await _streetRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}

using AutoMapper;
using BCS.Core.Entities;
using BCS.Repositories.Cityes;
using BCS.WebUI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCS.WebUI.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityRepository _cityRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CityController(
            ICityRepository cityRepository,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _cityRepository = cityRepository;
            _userManager = userManager;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return View(await _cityRepository.GetAllByUserAsync(currentUser));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityCreateDto model)
        {
            var entity = _mapper.Map<City>(model);
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    await _cityRepository.CreateAsync(entity);
                    return RedirectToAction("Index");
                }
            }

            return View(entity);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var entity = await _cityRepository.GetAsync(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CityUpdateDto model)
        {
            var entity = _mapper.Map<City>(model);

            await _cityRepository.UpdateAsync(entity);

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
            await _cityRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}

using AutoMapper;
using BCS.Core.Entities;
using BCS.Repositories.Types;
using BCS.WebUI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCS.WebUI.Controllers
{

    public class TypeController : Controller
    {
        private readonly ITypeRepository _typeRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public TypeController(
            ITypeRepository typeRepository,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _typeRepository = typeRepository;
            _userManager = userManager;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return View(await _typeRepository.GetAllByUserAsync(currentUser));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TypeCreateDto model)
        {
            var entity = _mapper.Map<Core.Entities.Type>(model);
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    await _typeRepository.CreateAsync(entity);
                    return RedirectToAction("Index");
                }
            }

            return View(entity);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var entity = await _typeRepository.GetAsync(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TypeUpdateDto model)
        {
            var entity = _mapper.Map<Core.Entities.Type>(model);

            await _typeRepository.UpdateAsync(entity);

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
            await _typeRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}

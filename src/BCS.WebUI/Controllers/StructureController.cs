using AutoMapper;
using BCS.Core.Entities;
using BCS.Repositories.Structures;
using BCS.WebUI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCS.WebUI.Controllers
{
    public class StructureController : Controller
    {
        private readonly IStructureRepository _structureRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public StructureController(
            IStructureRepository structureRepository,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _structureRepository = structureRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return View(await _structureRepository.GetAllByUserAsync(currentUser));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StructureCreateDto model)
        {
            var entity = _mapper.Map<Structure>(model);
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    await _structureRepository.CreateAsync(entity);
                    return RedirectToAction("Index");
                }
            }

            return View(entity);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var entity = await _structureRepository.GetAsync(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StructureUpdateDto model)
        {
            var entity = _mapper.Map<Structure>(model);

            await _structureRepository.UpdateAsync(entity);

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
            await _structureRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}

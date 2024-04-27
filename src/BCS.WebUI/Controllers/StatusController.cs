using AutoMapper;
using BCS.Core.Entities;
using BCS.Repositories.Statuses;
using BCS.WebUI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCS.WebUI.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusRepository _statusRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public StatusController(
            IStatusRepository statusRepository,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _statusRepository = statusRepository;
            _userManager = userManager;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return View(await _statusRepository.GetAllByUserAsync(currentUser));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StatusCreateDto model)
        {
            var entity = _mapper.Map<Status>(model);
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    await _statusRepository.CreateAsync(entity);
                    return RedirectToAction("Index");
                }
            }

            return View(entity);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var entity = await _statusRepository.GetAsync(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StatusUpdateDto model)
        {
            var entity = _mapper.Map<Status>(model);

            await _statusRepository.UpdateAsync(entity);

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
            await _statusRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}

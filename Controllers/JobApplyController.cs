using Microsoft.AspNetCore.Mvc;
using NestAlbania.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using NestAlbania.Data;
using NestAlbania.Services;

namespace NestAlbania.Controllers
{
    public class JobApplyController : Controller
    {
        private readonly IJobApplyService _jobApplyService;
        public JobApplyController(IJobApplyService jobApplyService)
        {
            _jobApplyService = jobApplyService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            int rows = 10;
            ViewBag.Page = page;
            var applications = await _jobApplyService.GetPaginatedApplication(page, rows);
            return View(applications);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            JobApplyModel jobApplyModel = new JobApplyModel();

            return View(jobApplyModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(JobApplyModel dto)
        {
            JobApply application = new JobApply()
            {
                Name = dto.Name,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,

            };

            await _jobApplyService.CreateApplicationAsync(application);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var application = await _jobApplyService.GetApplicationByIdAsync(id);
            return View(application);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(JobApply application)
        {

            await _jobApplyService.EditApplicationAsync(application);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var itemToDelete = await _jobApplyService.GetApplicationByIdAsync(id);
            await _jobApplyService.RemoveApplicationAsync(itemToDelete);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var apptoshow = await _jobApplyService.GetApplicationByIdAsync(id);
            return View(apptoshow);
        }
    }
}

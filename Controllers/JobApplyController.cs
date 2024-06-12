using Microsoft.AspNetCore.Mvc;
using NestAlbania.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using NestAlbania.Data;

namespace NestAlbania.Controllers
{
    public class JobApplyController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public JobApplyController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(JobApply application)
        {
            if (ModelState.IsValid)
            {
                // Save the resume file
                var fileName = Path.GetFileName(application.Resume.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "resumes", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await application.Resume.CopyToAsync(fileStream);
                }

                // Process application (e.g., save to database)

                // Redirect to a success page
                return RedirectToAction("Success");
            }

            return View(application);
        }

        public IActionResult Success()
        {
            return View();
        }


    }
}

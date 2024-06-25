using Microsoft.AspNetCore.Mvc;
using NestAlbania.Data;
using NestAlbania.Models;
using NestAlbania.Services;
using NestAlbania.Services.Extensions;
using System.Runtime.ConstrainedExecution;

namespace NestAlbania.Controllers
{
    public class JobApplyController : Controller
    {
        private readonly IJobApplyService _jobapplyService;
        private readonly IFileHandlerService _fileHandleService;
        private readonly IConfiguration _configuration;
        public JobApplyController(IJobApplyService jobapplyService , IFileHandlerService fileHandlerService, IConfiguration configuration)
        {
            _jobapplyService = jobapplyService;
            _fileHandleService = fileHandlerService;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index(int page =1)
        {
            int pageSize = 10;
            var applications = await _jobapplyService.GetAllPaginatedJobApplicationsAsync(page = 1, pageSize = 10);
            return View(applications);
        }
        public async Task<IActionResult> Details(int id)
        {
            var application = await _jobapplyService.GetJobApplicatonByIdAsync(id);
            if (application == null) return NotFound();
            return View(application);
        }
        public async Task<IActionResult> Delete(int id)
        {
            
            var application = await _jobapplyService.GetJobApplicatonByIdAsync(id);
            if (application == null) return NotFound();
            else
            {
                await _jobapplyService.DeleteJobApplicationAsync(application);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            JobApplicationDto dto = new JobApplicationDto();
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(JobApplicationDto dto)
        {
            if (dto == null) return BadRequest();

            JobApply application = new JobApply();
            application.FirstName = dto.FirstName;
            application.LastName = dto.LastName;
            application.CV = dto.CV;
            application.Email = dto.Email;
            application.Message = dto.Message;
            await _jobapplyService.CreateJobApplicationAsync(application);


            if (application == null) return NotFound();

            var file = HttpContext.Request.Form.Files.FirstOrDefault();
           
                var uploadDir = _configuration["Uploads:JobApplyDocuments"];
                var fileName = $"{application.FirstName}_{application.LastName}_{application.Id}_CV";
                fileName = await _fileHandleService.UploadAndRenameFileAsync(file, uploadDir, fileName);
                application.CV = fileName;
            
            await _jobapplyService.EditJobApplicationAsync(application);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var application = await _jobapplyService.GetJobApplicatonByIdAsync(id);

            return View(application);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(JobApply jobApply)
        {
            if (jobApply == null) return BadRequest();
            await _jobapplyService.EditJobApplicationAsync(jobApply);
            return RedirectToAction("Index");
        }

    }
}

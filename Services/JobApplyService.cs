using NestAlbania.Data;
using NestAlbania.Repositories;
using NestAlbania.Repositories.Pagination;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace NestAlbania.Services
{
    public class JobApplyService : IJobApplyService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly JobApplyRepo _jobApplyRepo;

        public JobApplyService(IWebHostEnvironment webHostEnvironment, JobApplyRepo jobApplyRepo)
        {
            _webHostEnvironment = webHostEnvironment;
            _jobApplyRepo = jobApplyRepo;
        }

        public async Task SaveApplicationAsync(JobApply application)
        {
            // Save the resume file
            var fileName = Path.GetFileName(application.Resume.FileName);
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "resumes", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await application.Resume.CopyToAsync(fileStream);
            }

            // Save application to the database
            await _jobApplyRepo.Create(application);
        }

        public async Task<PaginatedList<JobApply>> GetPaginatedApplicationsAsync(int pageIndex, int pageSize)
        {
            return await _jobApplyRepo.GetPaginatedApplicationsAsync(pageIndex, pageSize);
        }
    }
}

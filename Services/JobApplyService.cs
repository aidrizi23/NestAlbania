using NestAlbania.Data;
using NestAlbania.Repositories;
using NestAlbania.Repositories.Pagination;


namespace NestAlbania.Services
{
    public class JobApplyService : IJobApplyService
    {
        private readonly JobApplyRepo _jobApplyRepo;
        public JobApplyService(JobApplyRepo jobApplyRepo)
        {
            _jobApplyRepo = jobApplyRepo;
        }

        public async Task CreateApplicationAsync(JobApply apply)
        {
            await _jobApplyRepo.Create(apply);
        }
        public async Task EditApplicationAsync(JobApply apply)
        {
            await _jobApplyRepo.Edit(apply);
        }
        public async Task RemoveApplicationAsync(JobApply apply)
        {
            await _jobApplyRepo.Delete(apply);
        }
        public async Task<PaginatedList<JobApply>> GetPaginatedApplication(int page = 1, int pageSize = 10)
        {
            return await _jobApplyRepo.GetPaginatedApplication(page, pageSize);
        }
        public async Task<List<JobApply>> GetAllApplicationsAync()
        {
            var applications = await _jobApplyRepo.GetAll();
            return applications.ToList();
        }
        public async Task<JobApply> GetApplicationByIdAsync(int id)
        {
            return await _jobApplyRepo.GetById(id);
        }

    }
}

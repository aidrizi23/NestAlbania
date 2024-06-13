using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public interface IJobApplyService
    {
        Task<IEnumerable<JobApply>> GetAllJobApplicationsAsync();
        Task<JobApply> GetJobApplicatonByIdAsync(int id);
        Task CreateJobApplicationAsync(JobApply application);
        Task EditJobApplicationAsync(JobApply application);
        Task DeleteJobApplicationAsync(JobApply application);
        Task<PaginatedList<JobApply>> GetAllPaginatedJobApplicationsAsync(int pageIndex = 1, int pageSize = 10);

    }
}

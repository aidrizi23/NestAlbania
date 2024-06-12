using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public interface IJobApplyService
    {
        Task CreateApplicationAsync(JobApply apply);
        Task EditApplicationAsync(JobApply apply);
        Task RemoveApplicationAsync(JobApply apply);
        Task<PaginatedList<JobApply>> GetPaginatedApplication(int page = 1, int pageSize = 10);
        Task<List<JobApply>> GetAllApplicationsAync();
        Task<JobApply> GetApplicationByIdAsync(int id);
    }
}

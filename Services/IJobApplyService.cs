using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public interface IJobApplyService
    {
        Task SaveApplicationAsync(JobApply application);
        Task<PaginatedList<JobApply>> GetPaginatedApplicationsAsync(int pageIndex, int pageSize);
    }
}

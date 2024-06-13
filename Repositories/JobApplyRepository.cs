using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Repositories
{
    public class JobApplyRepository : BaseRepository<JobApply>
    {
        private readonly ApplicationDbContext _context;
       public JobApplyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
       public async Task<PaginatedList<JobApply>> GetPaginatedJobApplications(int pageIndex = 1, int pageSize = 10)
        {
            var jobAplications = _context.JobApplications/*.AsNoTracking()*/.OrderByDescending(x =>  x.Id).AsQueryable();
            return await PaginatedList<JobApply>.CreateAsync(jobAplications, pageIndex, pageSize);
        }

    }
}

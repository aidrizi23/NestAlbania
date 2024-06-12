using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Repositories
{
    public class JobApplyRepo : BaseRepository<JobApply>
    {
        private readonly ApplicationDbContext _context;
       public JobApplyRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PaginatedList<JobApply>> GetPaginatedApplicationsAsync(int pageIndex, int pageSize)
        {
            var source = _context.JobApplications.AsQueryable();
            return await PaginatedList<JobApply>.CreateAsync(source, pageIndex, pageSize);
        }


    }
}

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

        public async Task<PaginatedList<JobApply>> GetPaginatedApplication(int page = 1, int pageSize = 10)
        {
            var appResult = _context.JobApplications.OrderByDescending(x => x.Id).AsQueryable();
            var application = await PaginatedList<JobApply>.CreateAsync(appResult, page, pageSize);
            return application;
        }
        public async Task<JobApply> GetApplicationsAsync(int id)
        {
            return  _context.JobApplications.FirstOrDefault(x => x.Id == id);
        }


    }
}

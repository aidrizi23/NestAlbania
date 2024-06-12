using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PaginatedList<Role>> GetAllPaginatedRoles(int page = 1, int pageSize = 10)
        {
            var rolesResult = _context.Roles.OrderByDescending(x => x.Id).AsQueryable();
            var role = await PaginatedList<Role>.CreateAsync(rolesResult, page, pageSize);
            return role;
        }
    }
}

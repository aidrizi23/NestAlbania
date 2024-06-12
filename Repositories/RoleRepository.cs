using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Repositories
{
    public class RoleRepository : BaseRepository<ApplicationRole>
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ApplicationRole> GetRoleByUserIdAsync(string UserId)
        {
            return await _context.ApplicationRoles.FirstOrDefaultAsync(x => x.Id == UserId);
        }
    }
}

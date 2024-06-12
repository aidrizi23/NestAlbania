using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;

namespace NestAlbania.Repositories
{
    public class UserRoleRepository : BaseRepository<ApplicationUserRole>
    {
        private readonly ApplicationDbContext _context;
        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUserRole>> GetRolesByUserIdAsync(string UserId)
        {
            return await _context.ApplicationUserRoles.Where(x => x.UserId == UserId).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersByRoleIdAsync(string RoleId)
        {
            return (IEnumerable<ApplicationUser>)await _context.ApplicationUserRoles.Where(x => x.RoleId == RoleId).ToListAsync();
        }
        public async Task<ApplicationUserRole> GetUserRoleByIdAsync(string id)
        {
            return await _context.ApplicationUserRoles.FirstOrDefaultAsync(x => x.RoleId == id);
        }
    }
}

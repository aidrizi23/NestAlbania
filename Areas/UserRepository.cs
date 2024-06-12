using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;
using static NestAlbania.Areas.UserRepository;

namespace NestAlbania.Areas
{
    public class UserRepository : IUserRepository
    {
            private readonly ApplicationDbContext _context;
            public UserRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<PaginatedList<ApplicationUser>> GetPaginatedUsersAsync(int page = 1, int pagesize = 10)
            {

                return await PaginatedList<ApplicationUser>.CreateAsync(_context.ApplicationUsers.AsQueryable(), page, pagesize);
            }

            public async Task<ApplicationUser> GetUserByIdAsync(string id)
            {
                return await _context.ApplicationUsers.FindAsync(id);
            }

            public async Task<IEnumerable<ApplicationUser>> FindAllAsync()
            {
                return await _context.ApplicationUsers.ToListAsync();
            }

            public async Task UpdateUserAsync(ApplicationUser user)
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteUserAsync(ApplicationUser user)
            {
                _context.ApplicationUsers.Remove(user);
                await _context.SaveChangesAsync();
            }

            public async Task SaveChangesAsync()
            {
                await _context.SaveChangesAsync();
            }

        }

    }


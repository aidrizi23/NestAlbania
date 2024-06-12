using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public interface IUserService
    {
        Task<PaginatedList<ApplicationUser>> GetPaginatedUsersAsync(int page = 1, int pagesize = 10);

        Task<ApplicationUser> GetUserByIdAsync(string id);

        Task<IEnumerable<ApplicationUser>> FindAllAsync();

        Task UpdateUserAsync(ApplicationUser user);

        Task DeleteUserAsync(ApplicationUser user);

        Task SaveChangesAsync();
    }
}
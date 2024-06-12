using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public interface IRoleService
    {
        Task<PaginatedList<Role>> GetPaginatedRoles(int page = 1, int pageSize = 10);
        Task<Role> GetRoleById(int id);
        Task CreateRole(Role role);
        Task EditRole(Role role);
        Task Delete(Role role);
        Task<IEnumerable<Role>> GetAllRoles();
    }
}

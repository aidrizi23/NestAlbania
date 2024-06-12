using NestAlbania.Data;

namespace NestAlbania.Services
{
    public interface IUserRoleService
    {

        Task<IEnumerable<ApplicationUserRole>> GetAllAsync();
        Task CreateAsync(ApplicationUserRole entity);
        Task UpdateAsync(ApplicationUserRole entity);
        Task DeleteAsync(string id);
        Task<ApplicationUserRole> GetUserRoleByIdAsync(string id);
    }
}
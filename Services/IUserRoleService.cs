using NestAlbania.Data;

namespace NestAlbania.Services
{
    public interface IUserRoleService
    {

        Task<IEnumerable<ApplicationUserRole>> GetAllAsync();
        Task CreateAsync(ApplicationUserRole entity);
        Task UpdateAsync(ApplicationUserRole entity);
        Task DeleteAsync(ApplicationUserRole entity);
        Task<ApplicationUserRole> GetUserRoleByIdAsync(string id);
    }
}
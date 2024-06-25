using NestAlbania.Data;
using NestAlbania.Repositories;

namespace NestAlbania.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UserRoleRepository _userRoleRepository;
        public UserRoleService(UserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IEnumerable<ApplicationUserRole>> GetAllAsync()
        {
            return await _userRoleRepository.GetAll();
        }

        public async Task CreateAsync(ApplicationUserRole entity)
        {
            await _userRoleRepository.Create(entity);
        }

        public async Task UpdateAsync(ApplicationUserRole entity)
        {
            await _userRoleRepository.Edit(entity);
        }

        public async Task DeleteAsync(ApplicationUserRole entity)
        {
           
            await _userRoleRepository.Delete(entity);
        }

        public async Task<ApplicationUserRole> GetUserRoleByIdAsync(string id)
        {
            return await _userRoleRepository.GetUserRoleByIdAsync(id);
        }

    }
}

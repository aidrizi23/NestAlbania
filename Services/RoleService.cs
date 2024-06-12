using NestAlbania.Data;
using NestAlbania.Repositories;

namespace NestAlbania.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleRepository _roleRepository;
        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ApplicationRole> GetRoleByUserIdAsync(string UserId)
        {
            return await _roleRepository.GetRoleByUserIdAsync(UserId);
        }
        public async Task<IEnumerable<ApplicationRole>> GetAllAsync()
        {
            return await _roleRepository.GetAll();
        }


        public async Task CreateAsync(ApplicationRole entity)
        {
            await _roleRepository.Create(entity);
        }

        public async Task UpdateAsync(ApplicationRole entity)
        {
            await _roleRepository.Edit(entity);
        }

        public async Task DeleteAsync(string id)
        {
            var role = await _roleRepository.GetRoleByUserIdAsync(id);
            await _roleRepository.Delete(role);
        }
    }
}

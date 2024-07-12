using Microsoft.Identity.Client;
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

        public async Task<ApplicationRole> GetByIdAsync(string UserId)
        {
            return await _roleRepository.GetByRoleId(UserId);
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
            var role = await _roleRepository.GetByRoleId(id);
            await _roleRepository.Delete(role);
        }

       
    }
}

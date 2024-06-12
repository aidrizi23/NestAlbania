using NestAlbania.Data;
using NestAlbania.Repositories;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleRepository _roleRepository;
        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<PaginatedList<Role>> GetPaginatedRoles(int page = 1, int pageSize = 10)
        {
            return await _roleRepository.GetAllPaginatedRoles(page, pageSize);
        }
        public async Task<Role> GetRoleById(int id)
        {
            return await _roleRepository.GetById(id);
        }
        public async Task CreateRole(Role role)
        {
            await _roleRepository.Create(role);
        }
        public async Task EditRole(Role role)
        {
            await _roleRepository.Edit(role);
        }
        public async Task Delete(Role role)
        {
            await _roleRepository.Delete(role);
        }
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _roleRepository.GetAll();
        }
    }
}

using NestAlbania.Areas;
using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PaginatedList<ApplicationUser>> GetPaginatedUsersAsync(int page = 1, int pagesize = 10)
        {
            return await _userRepository.GetPaginatedUsersAsync(page, pagesize);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
        public async Task<IEnumerable<ApplicationUser>> FindAllAsync()
        {
            return await _userRepository.FindAllAsync();
        }
        public async Task UpdateUserAsync(ApplicationUser user)
        {
            await _userRepository.UpdateUserAsync(user);
        }
        public async Task DeleteUserAsync(ApplicationUser user)
        {
            await _userRepository.DeleteUserAsync(user);
        }
        public async Task SaveChangesAsync()
        {
            await _userRepository.SaveChangesAsync();
        }
    }

}

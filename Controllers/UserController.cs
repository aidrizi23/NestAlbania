using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NestAlbania.Areas;
using NestAlbania.Data;
using NestAlbania.Services;

namespace NestAlbania.Controllers
{
    [Authorize]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;
        private readonly IAgentService _agentService;
        public UserController(IUserRepository userRepository, IRoleService roleService, IUserRoleService userRoleService, IAgentService agentService)
        {
            _userRepository = userRepository;
            _roleService = roleService;
            _userRoleService = userRoleService;
            _agentService = agentService;
        }
        
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.FindAllAsync();
            return View(users);
        }
        
        
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userRepository.GetUserByIdAsync(id);
            await _userRepository.DeleteUserAsync(user);

            var userRole = await _userRoleService.GetUserRoleByUserIdAsync(user.Id);
            if(userRole != null)await _userRoleService.DeleteAsync(userRole);

            var agent = await _agentService.GetAgentByUserIdAsync(user.Id);
            if (agent != null) await _agentService.HardDeleteAgent(agent);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            await _userRepository.UpdateUserAsync(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);

        }


        [HttpGet]
        [Route("role-list")]
        public async Task<IActionResult> RoleIndex()
        {
            var roles = await _roleService.GetAllAsync();
            return View(roles);
        }

  
        [Route("role-delete/{id}")]
        public async Task<IActionResult> RoleDelete(string id)
        {
            await _roleService.DeleteAsync(id);
            return RedirectToAction("RoleIndex");
        }

        [HttpGet]
        [Route("role-create")]
        public IActionResult RoleCreate()
        {
            ApplicationRole role = new ApplicationRole();
            return View(role);
        }

        [HttpPost]
        [Route("role-create")]
        public async Task<IActionResult> RoleCreate(ApplicationRole role)
        {
            await _roleService.CreateAsync(role);
            return RedirectToAction("RoleIndex");
        }

        [HttpGet]
        [Route("role-edit/{id}")]
        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }
        [HttpPost]
        [Route("role-edit/{id}")]
        public async Task<IActionResult> RoleEdit(ApplicationRole role)
        {
            await _roleService.UpdateAsync(role);
            return RedirectToAction("RoleIndex");
        }

        [HttpGet]
        [Route("role-details/{id}")]
        public async Task<IActionResult> RoleDetails(string id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }


        // ketu do te krijojme metodat per applicationuserrole.

        [HttpGet]
        [Route("userrole-list")]
        public async Task<IActionResult> UserRoleIndex(string userId)
        {
            var userRoles = (await _userRoleService.GetAllAsync()).Where(x => x.UserId == userId);
            var user = await _userRepository.GetUserByIdAsync(userId);
            // get userrole
            var userRole = await _userRoleService.GetUserRoleByUserIdAsync(user.Id);
            var role = await _roleService.GetByIdAsync(userRole.RoleId);
            ViewBag.RoleName  = role.Name;
            ViewBag.UserName = user.CustomUserName;
            ViewBag.UserId = userId;
            return View(userRoles);
        }


        [Route("userrole-delete/{id}")]
        public async Task<IActionResult> UserRoleDelete(string id)
        {
            var user = await _userRoleService.GetUserRoleByRoleIdAsync(id);
            await _userRoleService.DeleteAsync(user);
            return RedirectToAction("UserRoleIndex");
        }

        [HttpGet]
        [Route("userrole-create")]
        public async Task<IActionResult> UserRoleCreate(string userId)
        {
            ViewBag.UserId = userId;

            ApplicationUserRole userRole = new ApplicationUserRole();

            userRole.UserId = userId;
            var roles = await _roleService.GetAllAsync();
            ViewBag.Roles = roles;
            return View(userRole);

        }
        [HttpPost]
        [Route("userrole-create")]
        public async Task<IActionResult> UserRoleCreate(ApplicationUserRole userRole)
        {
            await _userRoleService.CreateAsync(userRole);
            return RedirectToAction("UserRoleIndex", new { userId = userRole.UserId });
        }

        [HttpGet]
        [Route("userrole-edit/{id}")]
        public async Task<IActionResult> UserRoleEdit(string id)
        {
            var userRole = await _userRoleService.GetUserRoleByRoleIdAsync(id);

            return View(userRole);
        }
        [HttpPost]
        [Route("userrole-edit/{id}")]
        public async Task<IActionResult> UserRoleEdit(ApplicationUserRole userRole)
        {
            await _userRoleService.UpdateAsync(userRole);
            return RedirectToAction("UserRoleIndex");
        }

        [HttpGet]
        [Route("userrole-details/{id}")]
        public async Task<IActionResult> UserRoleDetails(string id)
        {
            var userRole = await _userRoleService.GetUserRoleByRoleIdAsync(id);
            return View(userRole);
        }
    }
}

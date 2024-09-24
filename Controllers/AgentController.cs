using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NestAlbania.Data;
using NestAlbania.FilterHelpers;
using NestAlbania.Models;
using NestAlbania.Services;
using NestAlbania.Services.Extensions;
using System.Security.Claims;
using NestAlbania.Models.DtoForEdit;

namespace NestAlbania.Controllers
{
    [Authorize]
    [Route("agent")]
    public class AgentController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public readonly IAgentService _agent;
        private readonly IConfiguration _configuration;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly IRoleService _roleService;
        public readonly IUserRoleService _userRoleService;
        public readonly IUserService _userService;

        public AgentController(IWebHostEnvironment webHostEnvironment, IAgentService agentService, IConfiguration configuration, IFileHandlerService fileHandlerService, IUserRoleService userRoleService, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _webHostEnvironment = webHostEnvironment;
            _agent = agentService;
            _configuration = configuration;
            _fileHandlerService = fileHandlerService;
            _userManager = userManager;
            _userRoleService = userRoleService;
            _userService = userService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var paginatedAgents = await _agent.GetPaginatedAgentsAsync(pageIndex, pageSize);

            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;

            ViewData["ActivePage"] = "agentIndex";
            return View(paginatedAgents);
        }
        
        
        [Route("delete/{id}")]
        [HttpGet]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> Delete(int id)
        {

            var agent = await _agent.GetAgentById(id);

            if (agent.UserId != null)
            {
                var user = await _userService.GetUserByIdAsync(agent.UserId);
                if (user != null)
                {
                    await _userService.DeleteUserAsync(user);
                }
                var userRole = await _userRoleService.GetUserRoleByUserIdAsync(agent.UserId);
                if (userRole != null)
                {
                    await _userRoleService.DeleteAsync(userRole);
                }
                await _agent.HardDeleteAgent(agent);
            }

            if (agent == null)
            {
                return NotFound();
            }

            var agentDirectoryIdentity = $"{agent.Id}";
            var uploadsFolderDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "files", "agent", agentDirectoryIdentity);

            if (!string.IsNullOrEmpty(agent.Image))
            {
                var filePath = Path.Combine(uploadsFolderDirectory, agent.Image);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }


            if (Directory.Exists(uploadsFolderDirectory))
            {
                Directory.Delete(uploadsFolderDirectory);
            }


            return RedirectToAction("Index");
        }
        
        [Route("softdelete/{id}")]
        [HttpPost]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var agent = await _agent.GetAgentById(id);
            if (agent == null)
            {
                return NotFound();
            }
            var agentUser = await _userService.GetUserByIdAsync(agent.UserId);
            
            agentUser.IsDeleted = true;
            agentUser.LockoutEnabled = true;

            if (agent.Properties != null)
                foreach (var item in agent.Properties)
                {
                    item.AgentId = null;
                    item.Agent = null;
                }

            agentUser.LockoutEnd = DateTimeOffset.MaxValue;
    
            await _userService.UpdateUserAsync(agentUser);
    
            // Proceed to soft-delete the agent
            await _agent.SoftDeleteAgentAsync(agent);

            return RedirectToAction("Index");
        }

        
        [HttpPost]
        [Route("undelete/{id}")]
        public async Task<IActionResult> UnDelete(int id)
        {
            var agent = await _agent.GetAgentById(id);
            var user = await _userService.GetUserByIdAsync(agent.UserId);

            
            user.IsDeleted = false;
            user.LockoutEnd = null;
            
            foreach(var item in agent.Properties)
            {
                item.AgentId = agent.Id;
                item.Agent = agent;
            }

            await _agent.UnDeleteAgentAsync(agent);
            return RedirectToAction("Index");
        }



        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {

            var dto = new AgentForCreationDto();

            ViewData["ActivePage"] = "agentIndex";
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(AgentForCreationDto dto)
        {
            // kontrollojme nqs emili ekziston
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "There already exists a user with this email");
                return View(dto);
            }

            // Krijojme userin qe do te kete te njejtin email si Agjenti qe do te krijojme ne
            var user = new ApplicationUser()
            {
                CustomUserName = $"{dto.Name}_{dto.Surname}", // proprty e shtuar tek useri pasi ne .net Identity kur bethet log in e bejme me ane te UserName dhe jo emailit by default, dhe i rash shkurt qe te na dale nje username nqs do te na duhet ndonjehere
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = true,
                Id = Guid.NewGuid().ToString(),
                IsDeleted = false,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return View(dto);
            }

            // Ketij Useri qe sapo krijuam, do ti bejme nj lidhje me nje rol statik, qe ne kete rast esth roli agent
            var selectedRole = dto.RoleId;
            var userRole = new ApplicationUserRole()
            {
                UserId = user.Id,
                RoleId = selectedRole
            };
            try
            {
                await _userRoleService.CreateAsync(userRole);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error creating user role.");
                return View(dto);
            }

            // Create the agent
            var agent = new Agent()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                LicenseNumber = dto.LicenseNumber,
                Motto = dto.Motto,
                PhoneNumber = dto.PhoneNumber,
                YearsOfExeperience = dto.YearsOfExeperience,
                Email = dto.Email,
                UserId = user.Id,
                RoleId = dto.RoleId,
                Password = dto.Password,
                isDeleted = false,
            };

            await _agent.CreateAgent(agent);

            
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            if (file != null)
            {

                var userDirectoryName = $"{agent.Id}";
                var uploadsFolderAgent = Path.Combine(_webHostEnvironment.WebRootPath, "files", "agent", userDirectoryName);
                if (!Directory.Exists(uploadsFolderAgent))
                    Directory.CreateDirectory(uploadsFolderAgent);

                var fileName = $"{Guid.NewGuid().ToString().Substring(0, 8)}{Path.GetExtension(file.FileName)}";
                

                var filePath = Path.Combine(uploadsFolderAgent, fileName);

                agent.Image = fileName;

                await _agent.EditAgent(agent);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            ViewData["ActivePage"] = "agentIndex";
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var agentToShowDetails = await _agent.GetAgentWithPropertiesAsync(id);

            ViewData["ActivePage"] = "agentIndex";
            return View(agentToShowDetails);
        }



        [HttpGet]
        [Route("edit/{id}")]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var agent = await _agent.GetAgentById(id);
            var dto = new AgentForEditDto()
            {
                Id = agent.Id,
                Name = agent.Name,
                Surname = agent.Surname,
                Image = agent.Image,
                PhoneNumber = agent.PhoneNumber,
                LicenseNumber = agent.LicenseNumber,
                Motto = agent.Motto,
                YearsOfExeperience = agent.YearsOfExeperience,
                Email = agent.Email,
                Password = agent.Password
            };

            ViewData["ActivePage"] = "agentIndex";
            return View(dto);
        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AgentForEditDto dto)
        {

            var agent = await _agent.GetAgentById(dto.Id);
            
            // // see if the current user is the agent
            // var currentUser = await _userManager.GetUserAsync(User);
            // if (agent.UserId != currentUser.Id && !User.IsInRole("admin"))
            // {
            //     return Unauthorized();
            // }
            
            // make it so that the agent can only edit their own details or if the user is an admin
            var currentUser = await _userManager.GetUserAsync(User);
            if (agent.UserId != currentUser.Id && !User.IsInRole("admin"))
            {
                return Unauthorized();
            }
            
            agent.Name = dto.Name;
            agent.Surname = dto.Surname;
            agent.PhoneNumber = dto.PhoneNumber;
            agent.LicenseNumber = dto.LicenseNumber;
            agent.Motto = dto.Motto;
            agent.YearsOfExeperience = dto.YearsOfExeperience;
            agent.Email = dto.Email;
            // agent.Password = dto.Password;
            
            // now we will change the user details
            var user = await _userManager.FindByIdAsync(agent.UserId);
            
            user.Email = dto.Email;
            user.UserName = dto.Email;
            user.CustomUserName = $"{dto.Name}_{dto.Surname}";

            if (dto.Password != agent.Password)
            {
                // Update password if provided 
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, dto.Password);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Error resetting password.");
                    return View(dto);
                }
                agent.Password = dto.Password;
            }
            
            await _userManager.UpdateAsync(user);
            await _agent.EditAgent(agent);


            ViewData["ActivePage"] = "agentIndex";
            return RedirectToAction("Index");
            
        }
        
        

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> GetFilteredAgents([FromQuery] AgentObjectQuery query, int pageIndex = 1, int pageSize = 10)
        {
            var agents = await _agent.GetFilteredAgents(query, pageIndex, pageSize);

            ViewData["CurrentNameFilter"] = query.Name;
            ViewData["CurrentSurnameFilter"] = query.Surname;
            ViewData["CurrentYearsOfExperienceFilter"] = query.YearsOfExeperience;
            ViewData["CurrentEmailFilter"] = query.Email;

            ViewData["ActivePage"] = "agentIndex";
            return View("Index", agents);
        }
        
        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetAgentDetailsUser()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);
            var agent = await _agent.GetAgentByUserIdAsync(userId);

            ViewData["ActivePage"] = "agentIndex";
            return View("Details", agent);
        }
    }
}

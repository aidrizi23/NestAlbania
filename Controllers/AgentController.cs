using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NestAlbania.Data;
using NestAlbania.Models;
using NestAlbania.Services;
using NestAlbania.Services.Extensions;

namespace NestAlbania.Controllers
{
    public class AgentController : Controller
    {
        public readonly IAgentService _agent;
        private readonly IConfiguration _configuration;
        private readonly IFileHandlerService _fileHandlerService;

        // importet qe duhet te behen per krijimin e nje useri 

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public readonly IRoleService _roleService;
        public readonly IUserRoleService _userRoleService;

        public AgentController(IAgentService agentService, IConfiguration configuration, IFileHandlerService fileHandlerService, IUserRoleService userRoleService, UserManager<ApplicationUser> userManager)
        {
            _agent = agentService;
            _configuration = configuration;
            _fileHandlerService = fileHandlerService;
            _userManager = userManager;
            _userRoleService = userRoleService;
        }
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var agent = await _agent.GetPaginatedAgent(pageIndex, pageSize);
            return View(agent);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var agent = await _agent.GetAgentById(id);
         
            await _agent.DeleteAgent(agent);
            return RedirectToAction("Index");
        }



        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{
        //    var dto = new AgentForCreationDto();
        //    return View(dto);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create(AgentForCreationDto dto)
        //{
        //    var user = new ApplicationUser()
        //    {
        //        UserName = $"{dto.Name}_{dto.Surname}",
        //        Email = dto.Email,
        //        Id = Guid.NewGuid().ToString()

        //    };  // ketu krijohet automatikisht nje user qe do t kete si userName Emrin_Mbiemrin e Agjentit dhe si email, emailin e agjentit.

        //    //provojme te krijojme nje user
        //    var result = await _userManager.CreateAsync(user, dto.Password);

        //    // tani do te krijojme automatikisht rolin per cdo agjent te krijuar
        //    // meqe roli eshte i krijuar ne database si nje rol static i quajturr Agent mund ta bejme ne kete menyre.

        //    var selectedRole = dto.RoleId;

        //    // tani do te krijojme nje UserRole qe do te lidhi userin me rolin e caktuar.

        //    var userRole = new ApplicationUserRole()
        //    {
        //        UserId = user.Id,
        //        RoleId = selectedRole
        //    };
        //    await _userRoleService.CreateAsync(userRole); // ketu u krijua automatikisht edhe roli.


        //    Agent agentss = new Agent()
        //    {
        //        Name = dto.Name,
        //        Surname = dto.Surname,
        //        LicenseNumber = dto.LicenseNumber,
        //        Motto = dto.Motto,
        //        PhoneNumber = dto.PhoneNumber,

        //        YearsOfExeperience = dto.YearsOfExeperience,
        //        Email = dto.Email,
        //        UserId = user.Id,
        //        Password = dto.Password,
        //        RoleId = dto.RoleId
        //    };
        //     await _agent.CreateAgent(agentss);

        //    var file = HttpContext.Request.Form.Files.FirstOrDefault();
        //    if (file != null)
        //    {
        //        var uploadDir = _configuration["Uploads:AgentImg"];
        //        var fileName = agentss.Name + "_" + agentss.Id;
        //        fileName = await _fileHandlerService.UploadAndRenameFileAsync(file, uploadDir, fileName);
        //        agentss.Image = fileName;
        //        await _agent.EditAgent(agentss);
        //    }

        //        return RedirectToAction("Index");
        //}


        //[HttpGet]
        //public IActionResult Create()
        //{
        //    var dto = new AgentForCreationDto();
        //    return View(dto);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public async Task<IActionResult> Create( AgentForCreationDto dto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(dto);
        //    }

        //    // perpara set e krijojme nje user duhet ta kontrollojme nqs i ekziston emaili qe ne duam te vendosim.
        //    var userEmail = await _userManager.FindByEmailAsync(dto.Email);
        //    if(userEmail != null)
        //    {
        //        ModelState.AddModelError(string.Empty,"There already exists a user with this email");
        //        return View(dto);
        //    }

        //    var user = new ApplicationUser()
        //    {
        //        UserName = $"{dto.Name}_{dto.Surname}",
        //        Email = dto.Email,
        //        EmailConfirmed = true,
        //        LockoutEnabled = true,
        //        SecurityStamp = "",
        //        Id = Guid.NewGuid().ToString(),
        //    };

        //    var result = await _userManager.CreateAsync(user, dto.Password);
        //    if (!result.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //        return View(dto);
        //    }

        //    var selectedRole = dto.RoleId;

        //    // Create the user role
        //    try
        //    {
        //        var userRole = new ApplicationUserRole()
        //        {
        //            UserId = user.Id,
        //            RoleId = selectedRole
        //        };
        //        await _userRoleService.CreateAsync(userRole);
        //    }
        //    catch (Exception ex)
        //    {

        //        ModelState.AddModelError(string.Empty, "Error creating user role.");
        //        return View(dto);
        //    }



        //    // Create the agent
        //    var agentss = new Agent()
        //    {
        //        Name = dto.Name,
        //        Surname = dto.Surname,
        //        LicenseNumber = dto.LicenseNumber,
        //        Motto = dto.Motto,
        //        PhoneNumber = dto.PhoneNumber,
        //        YearsOfExeperience = dto.YearsOfExeperience,
        //        Email = dto.Email,
        //        UserId = user.Id,
        //        RoleId = dto.RoleId,
        //        Password = dto.Password,
        //    };

        //    await _agent.CreateAgent(agentss);

        //    // Handle file upload if a file is provided
        //    var file = HttpContext.Request.Form.Files.FirstOrDefault();
        //    if (file != null)
        //    {
        //        var uploadDir = _configuration["Uploads:AgentImg"];
        //        var fileName = $"{agentss.Name}_{agentss.Id}_{Guid.NewGuid()}"; // Ensure unique file name
        //        fileName = await _fileHandlerService.UploadAndRenameFileAsync(file, uploadDir, fileName);
        //        agentss.Image = fileName;
        //        await _agent.EditAgent(agentss);
        //    }

        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public IActionResult Create()
        {
            var dto = new AgentForCreationDto();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                Id = Guid.NewGuid().ToString(),
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
            };

            await _agent.CreateAgent(agent);

            // Handle file upload if a file is provided
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            if (file != null)
            {
                var uploadDir = _configuration["Uploads:AgentImg"];
                var fileName = $"{agent.Name}_{agent.Id}_{Guid.NewGuid()}"; // Ensure unique file name
                fileName = await _fileHandlerService.UploadAndRenameFileAsync(file, uploadDir, fileName);
                agent.Image = fileName;
                await _agent.EditAgent(agent);
            }

            return RedirectToAction("Index");
        }




        public async Task<IActionResult> Details(int id)
        {
            var agentToShowDetails = await _agent.GetAgentById(id);
            return View(agentToShowDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var agentToEdit = await _agent.GetAgentById(id);
            return View(agentToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Agent agent)
        {
            await _agent.EditAgent(agent);
            return RedirectToAction("Index");


        }

    }
}

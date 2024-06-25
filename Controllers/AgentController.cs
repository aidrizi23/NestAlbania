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
        public AgentController(IAgentService agentService, IConfiguration configuration, IFileHandlerService fileHandlerService)
        {
            _agent = agentService;
            _configuration = configuration;
            _fileHandlerService = fileHandlerService;
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
        public async Task<IActionResult> Create()
        {
            var dto = new AgentForCreationDto();
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AgentForCreationDto dto)
        {
            Agent agentss = new Agent()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                LicenseNumber = dto.LicenseNumber,
                Motto = dto.Motto,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                YearsOfExeperience = dto.YearsOfExeperience,
          
            };
             await _agent.CreateAgent(agentss);

            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            if (file != null)
            {
                var uploadDir = _configuration["Uploads:AgentImg"];
                var fileName = agentss.Name + "_" + agentss.Id;
                fileName = await _fileHandlerService.UploadAndRenameFileAsync(file, uploadDir, fileName);
                agentss.Image = fileName;
                await _agent.EditAgent(agentss);
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

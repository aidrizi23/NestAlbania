using LandingPageApi.Models;
using Microsoft.AspNetCore.Mvc;
using NestAlbania.FilterHelpers;
using NestAlbania.Services;

namespace LandingPageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentApiController : ControllerBase
    {
        private readonly IAgentService _agentService;
        private readonly IPropertyService _propertyService;
        
        public AgentApiController(IAgentService agentService, IPropertyService propertyService)
        {
            _agentService = agentService;
            _propertyService = propertyService;
        }

        
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAgents(int pageIndex, int pageSize)
        {
            // return Ok(await _agentService.GetPaginatedAgentsAsync(pageIndex, pageSize));
            // will not include the related entities here
            
            
            var agents = await _agentService.GetPaginatedAgentsAsync(pageIndex, pageSize);
            
            foreach (var agent in agents)
            {
                var fileUrl = $"https://localhost:44314/files/agent/{agent.Id}/{agent.Image}";
                agent.Image = fileUrl;
            }
            
            return Ok(agents);
        }
        
        
        
        [HttpGet]
        [Route("one/{id}")]
        public async Task<IActionResult> GetAgent(int id)
        {
            var agent = await _agentService.GetAgentWithPropertiesAsync(id);
            if (agent == null)
            {
                return NotFound();
            }

            
            var fileUrl = $"https://localhost:44314/files/agent/{agent.Id}/{agent.Image}";
    
            var agentDto = new AgentDtoForAgentApi
            {
                Id = agent.Id,
                Name = agent.Name,
                Surname = agent.Surname,
                Image = fileUrl,
                PhoneNumber = agent.PhoneNumber,
                LicenseNumber = agent.LicenseNumber,
                Motto = agent.Motto,
                YearsOfExeperience = agent.YearsOfExeperience,
                UserId = agent.UserId,
                Email = agent.Email,
                IsDeleted = agent.isDeleted,
                PropertyDto = agent.Properties?.Select(p => new PropertyDtoForAgentApi
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    MainImage = p.MainImage,
                    Price = p.Price,
                    FullArea = p.FullArea,
                    InsideArea = p.InsideArea,
                    BedroomCount = p.BedroomCount,
                    BathroomCount = p.BathroomCount,
                    Documentation = p.Documentation,
                    OtherImages = p.OtherImages,
                    IsFavorite = p.IsFavorite,
                    PreviousPrice = p.PreviousPrice,
                    Category = p.Category,
                    Status = p.Status,
                    City = p.City,
                    AgentId = p.AgentId,
                    PostedOn = p.PostedOn,
                    LastEdited = p.LastEdited,
                    IsSold = p.IsSold,
                    PriceChangedDate = p.PriceChangedDate,
                    IsDeleted = p.isDeleted
                }).ToList()
            };
    
            return Ok(agentDto);
        }
        
        [HttpGet]
        [Route("filtered")]
        public async Task<IActionResult> GetFilteredAgents([FromQuery]AgentObjectQuery query, int pageIndex, int pageSize)
        {
            // return Ok(await _agentService.GetFilteredAgents(query, pageIndex, pageSize));
            
            var agents = await _agentService.GetFilteredAgents(query, pageIndex, pageSize);
            
            foreach (var agent in agents)
            {
                var fileUrl = $"https://localhost:44314/files/agent/{agent.Id}/{agent.Image}";
                agent.Image = fileUrl;
            }
            
            return Ok(agents);
        }

        [HttpGet]
        [Route("byproperty/{propertyId}")]
        public async Task<IActionResult> GetAgentByPropertyId(int propertyId)
        {
            var property = await _propertyService.GetPropertyByIdAsync(propertyId);
            
            var agent = await _agentService.GetAgentWithPropertiesAsync(property.AgentId!.Value);
            if (agent == null)
            {
                return NotFound();
            }
            var fileUrl = $"https://localhost:44314/files/agent/{agent.Id}/{agent.Image}";
            var agentDto = new AgentDtoForAgentApi
            {
                Id = agent.Id,
                Name = agent.Name,
                Surname = agent.Surname,
                Image = fileUrl,
                PhoneNumber = agent.PhoneNumber,
                LicenseNumber = agent.LicenseNumber,
                Motto = agent.Motto,
                YearsOfExeperience = agent.YearsOfExeperience,
                UserId = agent.UserId,
                Email = agent.Email,
                IsDeleted = agent.isDeleted,
                PropertyDto = agent.Properties?.Select(p => new PropertyDtoForAgentApi
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    MainImage = p.MainImage,
                    Price = p.Price,
                    FullArea = p.FullArea,
                    InsideArea = p.InsideArea,
                    BedroomCount = p.BedroomCount,
                    BathroomCount = p.BathroomCount,
                    Documentation = p.Documentation,
                    OtherImages = p.OtherImages,
                    IsFavorite = p.IsFavorite,
                    PreviousPrice = p.PreviousPrice,
                    Category = p.Category,
                    Status = p.Status,
                    City = p.City,
                    AgentId = p.AgentId,
                    PostedOn = p.PostedOn,
                    LastEdited = p.LastEdited,
                    IsSold = p.IsSold,
                    PriceChangedDate = p.PriceChangedDate,
                    IsDeleted = p.isDeleted
                }).ToList()
            };

            return Ok(agentDto);
        }
        
    }
}

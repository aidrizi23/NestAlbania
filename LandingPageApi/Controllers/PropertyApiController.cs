using LandingPageApi.Models;
using Microsoft.AspNetCore.Mvc;
using NestAlbania.Services;
using NestAlbania.Data;
using NestAlbania.Data.Enums;
using NestAlbania.FilterHelpers;
using NestAlbania.Repositories.Pagination;

namespace LandingPageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyApiController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
   

        public PropertyApiController(IPropertyService propertyService, IConfiguration configuration)
        {
            _propertyService = propertyService;
   
            
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<PaginatedList<Property>>> GetProperties()
        {
            return Ok(await _propertyService.GetAllPaginatedPropertiesAsync());
        }

        [HttpGet]
        [Route("/one/{id}")]
        public async Task<ActionResult<Property>> GetProperty(int id)
        {
            var property = await _propertyService.GetPropertyByIdWithAgentAsync(id); // this gets the property by id without including the related entities.
            if (property == null)
            {
                return NotFound();
            }
            var fileUrl = $"https://localhost:44314/files/property/{property.Id}/{property.MainImage}";
            
            // we will make this using the dto se spo gjeja dot naj menyr tjt me t mir lol
            var propertyDto = new PropertyDto
            {
                Id = property.Id,
                Name = property.Name,
                Description = property.Description,
                Price = property.Price,
                Category = property.Category,
                IsSold = property.IsSold,
                PostedOn = property.PostedOn,
                AgentId = property.AgentId,
                FullArea = property.FullArea,
                InsideArea = property.InsideArea,
                Status = property.Status,
                BathroomCount = property.BathroomCount,
                MainImage =fileUrl,
                IsFavorite = property.IsFavorite,
                PriceChangedDate = property.PriceChangedDate,
                BedroomCount = property.BedroomCount,
                City = property.City,
                IsDeleted = property.isDeleted,
                // kjo do kontrolloje nqs ka nje agjent (ka disa property qe mund ta kene null) dhe nqs ka do e mapoje.
                AgentDto =property.Agent!=null ? new AgentDto
                {
                    Id = property.Agent.Id,
                    Name = property.Agent.Name,
                    Surname = property.Agent.Surname,
                    PhoneNumber = property.Agent.PhoneNumber,
                    Email = property.Agent.Email,
                    Image = property.Agent.Image,
                    YearsOfExeperience = property.Agent.YearsOfExeperience,
                    LicenseNumber = property.Agent.LicenseNumber,
                    Motto = property.Agent.Motto,
                    UserId = property.Agent.UserId,
                    IsDeleted = property.Agent.isDeleted,
                } : null
            };
            
            return Ok(propertyDto); 
        }

        [HttpGet("agent/{agentId}")]
        public async Task<ActionResult<PaginatedList<Property>>> GetPropertiesByAgentId(int agentId, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await _propertyService.GetAllPaginatedPropertiesByAgentIdAsync(agentId, pageIndex, pageSize));
        }

        [HttpGet("filters")]
        public async Task<ActionResult<IEnumerable<Property>>> GetAllFilteredProperties([FromQuery] PropertyObjectQuery filter)
        {
            return Ok(await _propertyService.GetAllFilteredPropertiesAsync(filter));
        }
        
        
    }
}
using Microsoft.AspNetCore.Mvc;
using NestAlbania.Data;
using NestAlbania.Models;
using NestAlbania.Services;

namespace NestAlbania.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var propeties = await _propertyService.GetAllPaginatedPropertiesAsync(pageIndex, pageSize);
            return View(propeties);
        }

        public async Task<IActionResult> Delete(int id)
        {

           
            var property =await  _propertyService.GetPropertyByIdAsync(id);
            await _propertyService.DeletePropertyAsync(property);
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            var dto = new PropertyForCreationDto();
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PropertyForCreationDto dto)
        {
            Property property = new Property()
            {
                Name = dto.Name,
                FullArea = dto.FullArea,
                InsideArea = dto.InsideArea,
                Status = dto.Status,
                BathroomCount = dto.BathroomCount,
                BedroomCount = dto.BedroomCount,
                Description = dto.Description,
                Documentation = dto.Documentation,
                MainImage = dto.MainImage,
                OtherImages = dto.OtherImages,
                Price = dto.Price,
                
            };
            await _propertyService.CreatePropertyAsync(property);
            
           
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var itemToShowDetails = await _propertyService.GetPropertyByIdAsync(id);
            return View(itemToShowDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var itemToEdit = await _propertyService.GetPropertyByIdAsync(id);
            return View(itemToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Property property)
        {
                await _propertyService.EditPropertyAsync(property);
                return RedirectToAction("Index");
         
            
        }


    }
}

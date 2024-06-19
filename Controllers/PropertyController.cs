using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NestAlbania.Data;
using NestAlbania.Data.Enums;
using NestAlbania.Models;
using NestAlbania.Services;
using NestAlbania.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestAlbania.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly IConfiguration _configuration;

        public PropertyController(IPropertyService propertyService, IFileHandlerService fileHandlerService, IConfiguration configuration)
        {
            _propertyService = propertyService;
            _fileHandlerService = fileHandlerService;
            _configuration = configuration;
        }


        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var properties = await _propertyService.GetAllPaginatedPropertiesAsync(pageIndex, pageSize);
            return View(properties);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            await _propertyService.DeletePropertyAsync(property);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            PopulateViewBags();
            var dto = new PropertyForCreationDto();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PropertyForCreationDto dto)
        {
            
        
               
                Property property = new Property()
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    FullArea = dto.FullArea,
                    InsideArea = dto.InsideArea,
                    BedroomCount = dto.BedroomCount,
                    BathroomCount = dto.BathroomCount,
                    Documentation = dto.Documentation,
                    Category = dto.Category,
                    Status = dto.Status,
                    City = dto.SelectedCity,
                    OtherImages = dto.OtherImages,
                    
                    
                };

                await _propertyService.CreatePropertyAsync(property);

                var files = HttpContext.Request.Form.Files;
            var uploadDir = _configuration["Uploads:PropertyOtherImages132"];
                //if (dto.OtherImages != null && dto.OtherImages.Count > 0)
                //{
                    var fileNames = await _fileHandlerService.UploadAsync(files, uploadDir );

                    for(int i = 0; i< fileNames.Count; i++)
                    {
                        property.OtherImages.Add(fileNames[i]);
                    }
                    await _propertyService.EditPropertyAsync(property);
                //}
            
                

            PopulateViewBags();
            return RedirectToAction("Index");


            
        }

        private void PopulateViewBags()
        {
            ViewBag.Categories = Enum.GetValues(typeof(Category))
                .Cast<Category>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();

            ViewBag.Statuses = Enum.GetValues(typeof(PropertyStatus))
                .Cast<PropertyStatus>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();
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
            var model = new Property
            {
                Category = itemToEdit.Category,
                Status = itemToEdit.Status,
                // Populate other fields if necessary
            };

            PopulateViewBags();
            return View(itemToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Property property)
        {
            if (ModelState.IsValid)
            {
                await _propertyService.EditPropertyAsync(property);
                return RedirectToAction("Index");
            }

            PopulateViewBags();
            return View(property);
        }

        // Additional filter actions...

    }
}

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

        public PropertyController(IPropertyService propertyService, IFileHandlerService fileHandlerService)
        {
            _propertyService = propertyService;
            _fileHandlerService = fileHandlerService;
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
             //string mainImagePath = null;
                //if (dto.MainImageFile != null)
                //{
                //    mainImagePath = await _fileHandlerService.UploadAndRenameFileAsync(dto.MainImageFile, "uploads/main_images", Guid.NewGuid().ToString());
                //}

                Property property = new Property()
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    MainImage = dto.MainImage,
                    Price = dto.Price,
                    FullArea = dto.FullArea,
                    InsideArea = dto.InsideArea,
                    BedroomCount = dto.BedroomCount,
                    BathroomCount = dto.BathroomCount,
                    Documentation = dto.Documentation,
                    OtherImages = dto.OtherImages,
                    Category = dto.Category,
                    Status = dto.Status,
                    City = dto.SelectedCity
                };

                await _propertyService.CreatePropertyAsync(property);
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NestAlbania.Data;
using NestAlbania.Data.Enums;
using NestAlbania.FilterHelpers;
using NestAlbania.Models;
using NestAlbania.Services;
using NestAlbania.Services.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NestAlbania.Controllers
{
    public class PropertyController : Controller
    {   
        private readonly IPropertyService _propertyService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PropertyController(IPropertyService propertyService, IFileHandlerService fileHandlerService, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _propertyService = propertyService;
            _fileHandlerService = fileHandlerService;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var properties = await _propertyService.GetAllPaginatedPropertiesAsync(pageIndex, pageSize);
            return View(properties);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            await _propertyService.DeletePropertyAsync(property);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            PopulateViewBags();
            return View(new PropertyForCreationDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //e padobishme per mom po le tjet
        public async Task<IActionResult> Create(PropertyForCreationDto dto)
        {
            if (ModelState.IsValid)
            {
                string mainImagePath = null;
                if (dto.MainImageFile != null)
                {
                    var fileName = Guid.NewGuid().ToString();
                     mainImagePath = await _fileHandlerService.UploadAndRenameFileAsync(dto.MainImageFile, "images/properties", fileName);
                }

                var property = new Property
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
                    MainImage = mainImagePath,
                    OtherImages = new List<string>()
                };
                await _propertyService.CreatePropertyAsync(property);
       
                var file1 = HttpContext.Request.Form.Files.FirstOrDefault();
                if (file1 != null)
                {
                    var upload = _configuration["Uploads:PropertyDocumentation"];
                    var fileName = property.Name + "_" + property.Id;
                    fileName = await _fileHandlerService.UploadAndRenameFileAsync(file1, upload, fileName);
                    property.Documentation = fileName;
                    await _propertyService.EditPropertyAsync(property);
                }

                var files = HttpContext.Request.Form.Files; //akseson filet qe ti ke ber upload 
                var uploadDir = _configuration["Uploads:PropertyOtherImages132"];
                 var fileNames = await _fileHandlerService.UploadAsync(files, uploadDir); //njeh uploadin
                property.OtherImages = fileNames; //e shton ne list
                await _propertyService.EditPropertyAsync(property);
                

               
                return RedirectToAction("Index");
            }
            
            PopulateViewBags();
            return View(dto);
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
           
            
            ViewBag.Cities = Enum.GetValues(typeof(City))
                   .Cast<City>()
                   .Select(e => new SelectListItem
                   {
                       Value = e.ToString(),
                       Text = e.ToString()
                   }).ToList();


        }

        public async Task<IActionResult> Details(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Property property)
        {
            //if (ModelState.IsValid)
            //{

                ////sherben per editimin e fotove 
                //if (property.MainImageFile != null)
                //{
                //    var fileName = Guid.NewGuid().ToString();
                //    property.MainImage = await _fileHandlerService.UploadAndRenameFileAsync(property.MainImageFile, "images/properties", fileName);
                //}

                //var files = HttpContext.Request.Form.Files;
                //if (files.Count > 0)
                //{
                //    var fileNames = await _fileHandlerService.UploadAsync(files, "images/properties");
                //    property.OtherImages.AddRange(fileNames);
                //}

                await _propertyService.EditPropertyAsync(property);
                return RedirectToAction("Index");
            //}

            //PopulateViewBags();
            //return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllFilteredProperties([FromQuery] PropertyObjectQuery query, int pageIndex = 1, int pageSize = 10)
        {
            // first lets get all the properties
            var properties = await _propertyService.GetAllFilteredPropertiesAsync(query, pageIndex, pageSize);

            ViewData["CurrentNameFilter"] = query.Name;
            ViewData["CurrentPriceFilter"] = query.Price;
            ViewData["CurrentFullAreaFilter"] = query.FullArea;
            ViewData["CurrentInsideAreaFilter"] = query.InsideArea;
            ViewData["CurrentBedroomCountFilter"] = query.BedroomCount;
            ViewData["CurrentBathroomCountFilter"] = query.BathroomCount;

            return View("Index", properties);
        }

    }
}

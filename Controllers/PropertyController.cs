using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NestAlbania.Areas;
using NestAlbania.Data;
using NestAlbania.Data.Enums;
using NestAlbania.FilterHelpers;
using NestAlbania.Models;
using NestAlbania.Repositories.Pagination;
using NestAlbania.Services;
using NestAlbania.Services.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NestAlbania.Controllers
{
    [Authorize]
    public class PropertyController : Controller
    {   
        private readonly IPropertyService _propertyService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAgentService _agentService;
        public PropertyController(IPropertyService propertyService, IAgentService agentService, IUserRepository userRepository, UserManager<ApplicationUser> userManager, IFileHandlerService fileHandlerService, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _propertyService = propertyService;
            _fileHandlerService = fileHandlerService;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _userRepository = userRepository;
            _agentService = agentService;

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
        

        [HttpGet]
        public IActionResult Create()
        {
            PopulateViewBags();
            return View(new PropertyForCreationDto());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropertyForCreationDto dto)
        {
            // Handle main image upload
            string mainImagePath = null;
            if (dto.MainImageFile != null && dto.MainImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                mainImagePath = await _fileHandlerService.UploadAndRenameFileAsync(dto.MainImageFile, "images/properties", fileName);
            }

            // Handle documentation file upload if provided
            string documentationFileName = null;
            var documentationFile = HttpContext.Request.Form.Files.FirstOrDefault();
            if (documentationFile != null && documentationFile.Length > 0)
            {
                var documentationUploadDir = _configuration["Uploads:PropertyDocumentation"];
                documentationFileName = await _fileHandlerService.UploadAndRenameFileAsync(documentationFile, documentationUploadDir, dto.Name + "_" + Guid.NewGuid().ToString());
            }

            // Retrieve agent based on current user
            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);
            var agent = await _agentService.GetAgentByUserIdAsync(userId);

            // Create property object
            var property = new Property()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                FullArea = dto.FullArea,
                InsideArea = dto.InsideArea,
                BedroomCount = dto.BedroomCount,
                BathroomCount = dto.BathroomCount,
                Documentation = documentationFileName,
                Category = dto.Category,
                Status = dto.Status,
                City = dto.SelectedCity,
                MainImage = mainImagePath,
                OtherImages = new List<string>(), // Initialize empty list for other images
                AgentId = agent?.Id, // Assign agent ID to property
                Agent = await _agentService.GetAgentById(agent.Id)
            };

            // Save the property
            await _propertyService.CreatePropertyAsync(property);

            // Handle additional images upload
            var otherFiles = HttpContext.Request.Form.Files.Where(f => f.Name.StartsWith("OtherImages")).ToList();
            if (otherFiles.Count > 0)
            {
                var uploadDir = _configuration["Uploads:PropertyOtherImages132"];
                var fileCollection = new FormFileCollection();
                foreach (var file in otherFiles)
                {
                    fileCollection.Add(file);
                }
                var fileNames = await _fileHandlerService.UploadAsync(fileCollection, uploadDir);
                property.OtherImages.AddRange(fileNames);
                await _propertyService.EditPropertyAsync(property); // Update property with additional images
            }

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

            if (property.AgentId.HasValue)
            {
                property.Agent = await _agentService.GetAgentById(property.AgentId.Value);
            }
            else
            {
                property.Agent = null; // Handle the case where AgentId is null as needed
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Property property)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing property from the database
                    var existingProperty = await _propertyService.GetPropertyByIdAsync(property.Id);
                    if (existingProperty == null)
                    {
                        return NotFound();
                    }

                    // Update editable fields
                    existingProperty.Name = property.Name;
                    existingProperty.Description = property.Description;
                    existingProperty.Price = property.Price;
                    existingProperty.FullArea = property.FullArea;
                    existingProperty.InsideArea = property.InsideArea;
                    existingProperty.BedroomCount = property.BedroomCount;
                    existingProperty.BathroomCount = property.BathroomCount;
                    existingProperty.Category = property.Category;
                    existingProperty.Status = property.Status;
                    existingProperty.City = property.City;
                    existingProperty.AgentId = property.AgentId;
                    

                    //// Check if new main image is provided
                    //if (property.MainImage != null && property.MainImage.Length > 0)
                    //{
                    //    var fileName = Guid.NewGuid().ToString();
                    //    existingProperty.MainImage = await _fileHandlerService.UploadAndRenameFileAsync(property.MainImage, "images/properties", fileName);
                    //}

                    // Check if new additional images are provided
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0)
                    {
                        var fileNames = await _fileHandlerService.UploadAsync(files, "images/properties");
                        existingProperty.OtherImages.AddRange(fileNames);
                    }

                    // Save the updated property
                    await _propertyService.EditPropertyAsync(existingProperty);
 
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Handle exception as needed
                    ModelState.AddModelError("", "Error editing property: " + ex.Message);
                }
            }

            // If ModelState is not valid or an error occurred, populate necessary view bags and return to view
            PopulateViewBags();
            return View(property);
        }



        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAllFilteredProperties([FromQuery] PropertyObjectQuery query, int pageIndex = 1, int pageSize = 10)
        {
            // Optionally, you can validate the query parameters here before passing to the service/repository

            // Get paginated properties based on filters
            var properties = await _propertyService.GetAllFilteredPropertiesAsync(query, pageIndex, pageSize);

            // Pass filters to view
            ViewData["CurrentNameFilter"] = query.Name;
        
            ViewData["CurrentFullAreaFilter"] = query.FullArea;
            ViewData["CurrentInsideAreaFilter"] = query.InsideArea;
            ViewData["CurrentBedroomCountFilter"] = query.BedroomCount;
            ViewData["CurrentBathroomCountFilter"] = query.BathroomCount;
            ViewData["CurrentMinPriceFilter"] = query.MinPrice;
            ViewData["CurrentMaxPriceFilter"] = query.MaxPrice;
            ViewData["CurrentAgentFilter"] = query.AgentName;

            return View("Index", properties);
        }

        public async Task<IActionResult> GetFilteredPropertiesFromLower()
        {
            var properties = await _propertyService.GetAllPaginatedPropertiesAsync();
            var properties1 =  properties.OrderByDescending(x => x.Price).ToList();
            return View("Index", properties1);
            
        }

        public async Task<IActionResult> GetFilteredPropertiesFromUpper()
        {
            var properties = await _propertyService.GetAllPaginatedPropertiesAsync();
            var properties1 = properties.OrderBy(x => x.Price).ToList();
            return View("Index", properties1);

        }
    }
}

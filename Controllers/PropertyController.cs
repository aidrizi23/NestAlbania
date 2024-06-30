using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NestAlbania.Areas;
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

        public IActionResult Create()
        {
            PopulateViewBags();
            return View(new PropertyForCreationDto());
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]  //e padobishme per mom po le tjet
        //public async Task<IActionResult> Create(PropertyForCreationDto dto)
        //{

        //        string mainImagePath = null;
        //        if (dto.MainImageFile != null)
        //        {
        //            var fileName = Guid.NewGuid().ToString();
        //             mainImagePath = await _fileHandlerService.UploadAndRenameFileAsync(dto.MainImageFile, "images/properties", fileName);
        //        }

        //        var property = new Property
        //        {
        //            Name = dto.Name,
        //            Description = dto.Description,
        //            Price = dto.Price,
        //            FullArea = dto.FullArea,
        //            InsideArea = dto.InsideArea,
        //            BedroomCount = dto.BedroomCount,
        //            BathroomCount = dto.BathroomCount,
        //            Documentation = dto.Documentation,
        //            Category = dto.Category,
        //            Status = dto.Status,
        //            City = dto.SelectedCity,
        //            MainImage = mainImagePath,
        //            OtherImages = new List<string>()
        //        };
        //        await _propertyService.CreatePropertyAsync(property);


        //    // Upload new documentation photo if provided
        //    var documentationFile = HttpContext.Request.Form.Files.FirstOrDefault();
        //    if (documentationFile != null && documentationFile.Length > 0)
        //    {
        //        var documentationUploadDir = _configuration["Uploads:PropertyDocumentation"]; // Unique variable name
        //        var documentationFileName = property.Name + "_" + property.Id;
        //        documentationFileName = await _fileHandlerService.UploadAndRenameFileAsync(documentationFile, documentationUploadDir, documentationFileName);
        //        property.Documentation = documentationFileName;
        //        await _propertyService.EditPropertyAsync(property); // Update property with new documentation photo
        //    }


        //    var files = HttpContext.Request.Form.Files; //akseson filet qe ti ke ber upload 
        //        var uploadDir = _configuration["Uploads:PropertyOtherImages132"];
        //         var fileNames = await _fileHandlerService.UploadAsync(files, uploadDir); //njeh uploadin
        //        property.OtherImages = fileNames; //e shton ne list
        //        await _propertyService.EditPropertyAsync(property);


        //    PopulateViewBags();
        //    return RedirectToAction("Index");

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(PropertyForCreationDto dto)
        //{
        //    // Handle main image upload
        //    string mainImagePath = null;
        //    if (dto.MainImageFile != null && dto.MainImageFile.Length > 0)
        //    {
        //        var fileName = Guid.NewGuid().ToString();
        //        mainImagePath = await _fileHandlerService.UploadAndRenameFileAsync(dto.MainImageFile, "images/properties", fileName);
        //    }

        //    // Handle documentation file upload if provided
        //    string documentationFileName = null;
        //    var documentationFile = HttpContext.Request.Form.Files.FirstOrDefault();
        //    if (documentationFile != null && documentationFile.Length > 0)
        //    {
        //        var documentationUploadDir = _configuration["Uploads:PropertyDocumentation"];
        //        documentationFileName = await _fileHandlerService.UploadAndRenameFileAsync(documentationFile, documentationUploadDir, dto.Name + "_" + Guid.NewGuid().ToString());
        //    }

        //    var user = await _userManager.GetUserAsync(User);
        //    var userId = await _userManager.GetUserIdAsync(user);
        //    var agent = await _agentService.GetAgentByUserIdAsync(userId);


        //    // Create property object
        //    var property = new Property
        //    {
        //        Name = dto.Name,
        //        Description = dto.Description,
        //        Price = dto.Price,
        //        FullArea = dto.FullArea,
        //        InsideArea = dto.InsideArea, 
        //        BedroomCount = dto.BedroomCount,
        //        BathroomCount = dto.BathroomCount,
        //        Documentation = documentationFileName,
        //        Category = dto.Category,
        //        Status = dto.Status,
        //        City = dto.SelectedCity,
        //        MainImage = mainImagePath,
        //        OtherImages = new List<string>(), 
        //        // Initialize empty list for other images
        //        AgentId = agent?.Id
        //    };

        //    // Save the property
        //    await _propertyService.CreatePropertyAsync(property);

        //    // Handle additional images upload
        //    var otherFiles = HttpContext.Request.Form.Files.Where(f => f.Name.StartsWith("OtherImages")).ToList();
        //    if (otherFiles.Count > 0)
        //    {
        //        var uploadDir = _configuration["Uploads:PropertyOtherImages132"];
        //        var fileCollection = new FormFileCollection();
        //        foreach (var file in otherFiles)
        //        {
        //            fileCollection.Add(file);
        //        }
        //        var fileNames = await _fileHandlerService.UploadAsync(fileCollection, uploadDir);
        //        property.OtherImages.AddRange(fileNames);
        //        await _propertyService.EditPropertyAsync(property); // Update property with additional images
        //    }


        //    PopulateViewBags();
        //    return RedirectToAction("Index");
        //}


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
            var property = new Property
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
                AgentId = agent.Id // Assign agent ID to property
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

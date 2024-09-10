﻿    using Microsoft.AspNetCore.Authorization;
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


namespace NestAlbania.Controllers
{
    [Authorize]
    [Route("property")]
    public class PropertyController : Controller
    {

        private readonly IPropertyService _propertyService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAgentService _agentService;
        private readonly IFavoriteService _favoriteService;

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

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);
            var agent = await _agentService.GetAgentByUserIdAsync(userId);

            PaginatedList<Property> properties;
            if (agent == null)
            {
                properties = await _propertyService.GetAllPaginatedPropertiesAsync(pageIndex, pageSize);
            }
            else
            {
                properties = await _propertyService.GetAllPaginatedPropertiesByAgentIdAsync(agent.Id, pageIndex, pageSize);
            }

            return View(properties);
        }



        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            await _propertyService.HardDeletePropertyAsync(property);
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("softdelete")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            try
            {
                await _propertyService.SoftDeletePropertyAsync(property);
                await _propertyService.EditPropertyAsync(property); // Assuming this is needed to update the property status after soft delete
            }
            catch (Exception ex)
            {
                // Log the exception
                // Consider adding a ViewBag or TempData message for the error
                return BadRequest("An error occurred while trying to soft delete the property: " + ex.Message);
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        [Route("undelete")]
        public async Task<IActionResult> UnDelete(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            await _propertyService.UnDeletePropertyAsync(property);
            await _propertyService.EditPropertyAsync(property);
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("sell")]
        public async Task<IActionResult> Sell(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            try
            {
                await _propertyService.SellPropertyAsync(property);
                await _propertyService.EditPropertyAsync(property);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while trying to sell the property: " + ex.Message);
            }

            return RedirectToAction("Index");
        }



        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            PopulateViewBags();
            return View(new PropertyForCreationDto());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(PropertyForCreationDto dto)
        {

            // Handle main image upload
            string? mainImagePath = null;
            if (dto.MainImageFile != null && dto.MainImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                mainImagePath = await _fileHandlerService.UploadAndRenameFileAsync(dto.MainImageFile, "images/properties", fileName);
            }

            // Handle documentation file upload if provided
            string? documentationFileName = null;
            var documentationFile = HttpContext.Request.Form.Files.FirstOrDefault();
            if (documentationFile != null && documentationFile.Length > 0)
            {
                var documentationUploadDir = _configuration["Uploads:PropertyDocumentation"];
                documentationFileName = await _fileHandlerService.UploadAndRenameFileAsync(documentationFile, documentationUploadDir, dto.Name + "_" + Guid.NewGuid().ToString());
            }

            // Retrieve agent based on current user
            ApplicationUser? user = await _userManager.GetUserAsync(User);
            string? userId = await _userManager.GetUserIdAsync(user);
            Agent? agent = await _agentService.GetAgentByUserIdAsync(userId);

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
                Agent = await _agentService.GetAgentById(agent.Id),
                PostedOn = DateTime.Now.Date,
                isSold = false,
                isDeleted = false,

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

        //[HttpPost]
        //[Route("togglefavorite")]
        //public async Task<IActionResult> ToggleFavorite(int id)
        //{
        //    var property = await _propertyService.GetPropertyByIdAsync(id);

        //    if (property == null)
        //    {
        //        return NotFound();
        //    }

        //    property.IsFavorite = !property.IsFavorite;
        //    await _propertyService.EditPropertyAsync(property);

        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        [Route("details/{id}")]
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
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            PopulateViewBags();
            return View(property);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(Property property)
        {
            if (property != null)
            {
                try
                {

                    var existingProperty = await _propertyService.GetPropertyByIdAsync(property.Id);
                    if (existingProperty == null)
                    {

                        return NotFound();
                    }

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
                    //existingProperty.AgentId = property.AgentId;

                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0)
                    {
                        var fileNames = await _fileHandlerService.UploadAsync(files, "images/properties");
                        existingProperty.OtherImages.AddRange(fileNames);
                    }

                    await _propertyService.EditPropertyAsync(existingProperty);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error editing property: " + ex.Message);
                }
            }

            PopulateViewBags();
            return View(property);
        }


        // [HttpGet]
        // public async Task<IActionResult> GetAllFilteredProperties([FromQuery] PropertyObjectQuery query, int pageIndex = 1, int pageSize = 10, string sortOrder = "")
        // {
        //     var properties = await _propertyService.GetAllFilteredPropertiesAsync(query, pageIndex, pageSize, sortOrder);
        //     
        //     ViewData["CurrentNameFilter"] = query.Name ?? "";
        //     ViewData["CurrentFullAreaFilter"] = query.FullArea;
        //     ViewData["CurrentInsideAreaFilter"] = query.InsideArea;
        //     ViewData["CurrentBedroomCountFilter"] = query.BedroomCount;
        //     ViewData["CurrentBathroomCountFilter"] = query.BathroomCount;
        //     ViewData["CurrentMinPriceFilter"] = query.MinPrice;
        //     ViewData["CurrentMaxPriceFilter"] = query.MaxPrice;
        //     ViewData["CurrentAgentFilter"] = query.AgentName ?? "";
        //     ViewData["CurrentSortOrder"] = sortOrder;
        //
        //     return View("Index", properties);
        // }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> GetAllFilteredProperties([FromQuery] PropertyObjectQuery query, int pageIndex = 1, int pageSize = 10, string sortOrder = "", bool? ShowAdditionalFilters = null)
        {
            var properties = await _propertyService.GetAllFilteredPropertiesAsync(query, pageIndex, pageSize, sortOrder);

            ViewData["CurrentNameFilter"] = query.Name ?? "";
            ViewData["CurrentFullAreaFilter"] = query.FullArea;
            ViewData["CurrentInsideAreaFilter"] = query.InsideArea;
            ViewData["CurrentBedroomCountFilter"] = query.BedroomCount;
            ViewData["CurrentBathroomCountFilter"] = query.BathroomCount;
            ViewData["CurrentMinPriceFilter"] = query.MinPrice;
            ViewData["CurrentMaxPriceFilter"] = query.MaxPrice;
            ViewData["CurrentAgentFilter"] = query.AgentName ?? "";
            ViewData["CurrentSortOrder"] = sortOrder;

            // Add this line to handle the ShowAdditionalFilters state
            ViewData["ShowAdditionalFilters"] = ShowAdditionalFilters ?? false;

            return View("Index", properties);
        }
    }

}
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NestAlbania.Data;
using NestAlbania.Data.Enums;
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
                    //krijohet pathi per foton e caktuar
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

                var files = HttpContext.Request.Form.Files; //akseson filet qe ti ke ber upload 
                if (files.Count > 0)
                {
                    var fileNames = await _fileHandlerService.UploadAsync(files, "images/properties"); //njeh uploadin
                    property.OtherImages.AddRange(fileNames); //e shton ne list
                }

                await _propertyService.CreatePropertyAsync(property);
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

            var dto = new PropertyForCreationDto
            {
                Name = property.Name,
                Description = property.Description,
                Price = property.Price,
                FullArea = property.FullArea,
                InsideArea = property.InsideArea,
                BedroomCount = property.BedroomCount,
                BathroomCount = property.BathroomCount,
                Documentation = property.Documentation,
                Category = property.Category,
                Status = property.Status,
                SelectedCity = property.City,
                OtherImages = property.OtherImages
            };

            PopulateViewBags();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PropertyForCreationDto dto)
        {
            if (ModelState.IsValid)
            {
                var property = await _propertyService.GetPropertyByIdAsync(id);
                if (property == null)
                {
                    return NotFound();
                }

                property.Name = dto.Name;
                property.Description = dto.Description;
                property.Price = dto.Price;
                property.FullArea = dto.FullArea;
                property.InsideArea = dto.InsideArea;
                property.BedroomCount = dto.BedroomCount;
                property.BathroomCount = dto.BathroomCount;
                property.Documentation = dto.Documentation;
                property.Category = dto.Category;
                property.Status = dto.Status;
                property.City = dto.SelectedCity;

                //sherben per editimin e fotove 
                if (dto.MainImageFile != null)
                {
                    var fileName = Guid.NewGuid().ToString();
                    property.MainImage = await _fileHandlerService.UploadAndRenameFileAsync(dto.MainImageFile, "images/properties", fileName);
                }

                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var fileNames = await _fileHandlerService.UploadAsync(files, "images/properties");
                    property.OtherImages.AddRange(fileNames);
                }

                await _propertyService.EditPropertyAsync(property);
                return RedirectToAction("Index");
            }

            PopulateViewBags();
            return View(dto);
        }
    }
}

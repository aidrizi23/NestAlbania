using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NestAlbania.Models;
using System.Diagnostics;
using NestAlbania.Services;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using NestAlbania.Data.Enums;
using NestAlbania.Data;
using Microsoft.EntityFrameworkCore;

namespace NestAlbania.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPropertyService _propertyService;
        private readonly IAgentService _agentService;

        public HomeController(ILogger<HomeController> logger, IPropertyService propertyService, IAgentService agentService)
        {
            _logger = logger;
            _propertyService = propertyService;
            _agentService = agentService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var properties = await _propertyService.GetAllPropertiesAsync();
                var soldProperties = properties
                    .Where(p => p.isSold.HasValue && p.isSold.Value)
                    .OrderByDescending(p => p.PostedOn) 
                    .ToList();

                var latestSoldProperties = soldProperties.Take(4).ToList();

                var monthlySoldProperties = soldProperties
                    .GroupBy(p => p.PostedOn.ToString("MMMM yyyy"))
                    .ToDictionary(g => g.Key, g => g.Count());

                _logger.LogInformation("Monthly Sold Properties: {MonthlySoldProperties}", string.Join(", ", monthlySoldProperties.Select(kvp => $"{kvp.Key}: {kvp.Value}")));

                ViewBag.MonthlySoldProperties = monthlySoldProperties;

                var groupedProperties = properties
                    .Where(p => !p.isSold.HasValue || !p.isSold.Value)
                    .GroupBy(p => p.Category)
                    .Select(g => new GroupedProperty
                    {
                        Category = g.Key,
                        LatestProperty = g.OrderByDescending(p => p.PostedOn).FirstOrDefault()
                    })
                    .ToList();

                ViewBag.GroupedProperties = groupedProperties;

                if (soldProperties.Any())
                {
                    var topSellingAgent = await _agentService.GetTopSellingAgentAsync();
                    ViewBag.TopSellingAgent = topSellingAgent;
                }
                else
                {
                    ViewBag.TopSellingAgent = null; 
                }

                var totalProperties = properties.Count();

                var total = await _agentService.GetAllAgents();
                var totalAgents = total.Count();

                var mostSoldCategory = soldProperties
                    .GroupBy(p => p.Category)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefault();
                ViewBag.TotalProperties = totalProperties;
                ViewBag.TotalAgents = totalAgents;
                ViewBag.MostSoldCategory = mostSoldCategory;

                ViewBag.SoldProperties = latestSoldProperties;

                ViewData["ActivePage"] = "dashboardIndex";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while loading the home page.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPropertyStatusData()
        {
            try
            {
                var allProperties = await _propertyService.GetAllPropertiesAsync();

                int totalProperties = allProperties.Count();
                int soldProperties = allProperties.Count(p => p.isSold.HasValue && p.isSold.Value);

                var salesByDay = allProperties
                    .Where(p => p.isSold.HasValue && p.isSold.Value)
                    .GroupBy(p => p.PostedOn.Date)
                    .ToDictionary(g => g.Key.ToString("yyyy-MM-dd"), g => g.Count());

                var model = new PropertyStatusModel
                {
                    Total = totalProperties,
                    Sold = soldProperties,
                    Available = totalProperties - soldProperties,
                    SalesByDay = salesByDay
                };

                ViewData["ActivePage"] = "propertyOverviewIndex";
                return View("Index1", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching property status data.");
                return StatusCode(500, "Internal server error.");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    internal class GroupedProperty
    {
        public Category Category { get; set; }
        public Property LatestProperty { get; set; }
    }
}

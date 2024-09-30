using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NestAlbania.Models;
using System.Diagnostics;
using NestAlbania.Services;
using NestAlbania.Data.Enums;
using NestAlbania.Data;
using Microsoft.EntityFrameworkCore;

namespace NestAlbania.Controllers
{
    [Authorize]
    [Route("[controller]")]

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

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("dashboard")]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Get all properties excluding soft-deleted ones
                var properties = await _propertyService.GetAllPropertiesAsync();
                var nonDeletedProperties = properties.Where(p => !p.isDeleted).ToList();

                var soldProperties = nonDeletedProperties
                    .Where(p => p.IsSold == true)
                    .OrderByDescending(p => p.PostedOn)
                    .ToList();

                var latestSoldProperties = soldProperties.Take(4).ToList().OrderByDescending(p => p.PostedOn);

                var monthlySoldProperties = soldProperties
                    .GroupBy(p => p.PostedOn.ToString("MMMM yyyy"))
                    .ToDictionary(g => g.Key, g => g.Count());

                _logger.LogInformation("Monthly Sold Properties: {MonthlySoldProperties}", string.Join(", ", monthlySoldProperties.Select(kvp => $"{kvp.Key}: {kvp.Value}")));

                ViewBag.MonthlySoldProperties = monthlySoldProperties;

                var groupedProperties = nonDeletedProperties
                    .Where(p => !p.IsSold == false)
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

                // Total properties excluding soft-deleted
                var totalProperties = nonDeletedProperties.Count();

                // Total agents excluding soft-deleted properties
                var total = await _agentService.GetAllAgents();
                var totalAgents = total.Count();

                // Most sold category excluding soft-deleted properties
                var mostSoldCategory = soldProperties
                    .GroupBy(p => p.Category)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefault();

                // Calculate available properties
                var availableProperties = nonDeletedProperties.Count(p => !p.IsSold);

                // Pass the calculated data to the view
                ViewBag.TotalProperties = totalProperties;
                ViewBag.TotalAgents = totalAgents;
                ViewBag.MostSoldCategory = mostSoldCategory;
                ViewBag.AvailableProperties = availableProperties; // New line for available properties

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
        [Route("overview")]
        public async Task<IActionResult> GetPropertyStatusData()
        {
            try
            {
                // Get all properties, but exclude soft-deleted ones
                var allProperties = await _propertyService.GetAllPropertiesAsync();
                var nonDeletedProperties = allProperties.Where(p => !p.isDeleted).ToList(); // Filter out soft-deleted properties

                int totalProperties = nonDeletedProperties.Count();
                int soldProperties = nonDeletedProperties.Count(p => p.IsSold == true);

                var salesByDay = nonDeletedProperties
                    .Where(p => p.IsSold == true)
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
        [HttpGet]
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

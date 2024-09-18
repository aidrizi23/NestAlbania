using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NestAlbania.Models;
using System.Diagnostics;
using NestAlbania.Services;
using NestAlbania.Data.Enums;
using NestAlbania.Data;

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

        [HttpGet]
         public async Task<IActionResult> Index()
{
           try{
        var properties = await _propertyService.GetAllPropertiesAsync();
        var soldProperties = properties.Where(p => p.IsSold == true).ToList();

        var monthlySoldProperties = soldProperties
            .GroupBy(p => p.PostedOn.ToString("MMMM yyyy")) 
            .ToDictionary(g => g.Key, g => g.Count());

        _logger.LogInformation("Monthly Sold Properties: {MonthlySoldProperties}", string.Join(", ", monthlySoldProperties.Select(kvp => $"{kvp.Key}: {kvp.Value}")));

       
        ViewBag.MonthlySoldProperties = monthlySoldProperties;

        var groupedProperties = properties
            .Where(p => p.IsSold == false)
            .GroupBy(p => p.Category)
            .Select(g => new GroupedProperty
            {
                Category = g.Key,
                LatestProperty = g.OrderByDescending(p => p.PostedOn).FirstOrDefault()
            })
            .ToList();

        ViewBag.GroupedProperties = groupedProperties;

        var topSellingAgent = await _agentService.GetTopSellingAgentAsync();

        ViewBag.SoldProperties = soldProperties;
        ViewBag.TopSellingAgent = topSellingAgent;

        return View();
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "An error occurred while loading the home page.");
        return StatusCode(500, "Internal server error.");
    }
}



         [HttpGet]
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

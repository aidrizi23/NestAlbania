using Microsoft.AspNetCore.Mvc;
using NestAlbania.Services;

namespace NestAlbania.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}

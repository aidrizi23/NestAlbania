using Microsoft.AspNetCore.Mvc;

namespace NestAlbania.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

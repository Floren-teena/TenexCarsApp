using Microsoft.AspNetCore.Mvc;

namespace TenexCars.Controllers.Admin_Controller
{
    public class AdminController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }
    }
}

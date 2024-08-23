using Microsoft.AspNetCore.Mvc;

namespace TenexCars.Controllers.Vehicle_Inventory_Controller
{
    public class VehicleInventoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TenexCars.DataAccess.DTOs;
using TenexCars.DataAccess.Enums;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Implementations;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.DataAccess.ViewModels;
using TenexCars.Interfaces;
using TenexCars.Models;
using TenexCars.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TenexCars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, IVehicleRepository vehicleRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signManager, ISubscriberRepository subscriberRepository, IEmailService emailService)
        {
            _logger = logger;
            _vehicleRepository = vehicleRepository;
            _userManager = userManager;
            _subscriberRepository = subscriberRepository;
            _emailService = emailService;
            _signManager = signManager;
        }

        public async Task<IActionResult> Index()
        {
            var topUniqueVehicles = await _vehicleRepository.GetTopUniqueVehiclesAsync();
            return View(topUniqueVehicles);
        }
        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Operator()
        {
            return View();
        }

        public IActionResult Subscriber()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactPageViewModel contactPageViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(contactPageViewModel);
                }

                var emailContent = new EmailDto
                {

                    From = contactPageViewModel.Email,
                    Subject = contactPageViewModel.Subject,
                    Body = $"Name: {contactPageViewModel.FullName}\n\n Message:\n{contactPageViewModel.Body}"
                };

                await _emailService.SendEmailToCompany(emailContent);
                TempData["success"] = "Email sent successfully!";
                return View(contactPageViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Something went wrong when sending email");
                TempData["error"] = "Something went wrong when sending email";
                return View(contactPageViewModel);
            }

        }

        public IActionResult Admin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Help()
        {
            return View();
        }
        public IActionResult Testimonies()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TenexCars.DataAccess.Enums;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.Models.ViewModels;

namespace TenexCars.Controllers.Account_Controller
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepository _accountRepository;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<AccountController> logger,
            ISubscriptionRepository subscriptionRepository, ISubscriberRepository subscriberRepository, IAccountRepository accountRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _subscriptionRepository = subscriptionRepository;
            _subscriberRepository = subscriberRepository;
            _accountRepository = accountRepository; 
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateNewPassword()
        {
            return View();
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            var user = await _userManager.FindByEmailAsync(loginVm.Username);
            if (user == null)
            {
                _logger.LogWarning("User not found: {Email}", loginVm.Username);
                TempData["Error"] = "Invalid credentials";
                return View(loginVm);
            }

            _logger.LogInformation("User found: {Email}", loginVm.Username);

            var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                _logger.LogInformation("User roles: {Roles}", string.Join(", ", roles));

                if (roles.Contains("Main_Subscriber"))
                {
                    var subscriber = await _subscriberRepository.GetSubscriberByUserId(user.Id);

                    var subscription = subscriber is not null ? await _subscriptionRepository.GetSubscriptionBySubcriber(subscriber.Id) : null;
                    if (subscription is not null && subscription.SubscriptionStatus == SubscriptionStatus.DLNeeded)
                    {
                        _logger.LogInformation("Redirecting to Complete Reservation Page ...");
                        return RedirectToAction("CompleteReservation", "Subscriber");
                    }
                    _logger.LogInformation("Redirecting to Subscriber page");
                    return RedirectToAction("Profile", "Subscriber");
                }
                else if (roles.Contains("Main_Operator"))
                {
                    _logger.LogInformation("Redirecting to Operator page");
                    return RedirectToAction("OperatorDashboard", "Operator");
                }
                else if (roles.Contains("Operator_Team_Member"))
                {
                    _logger.LogInformation("Redirecting to Operator page");
                    return RedirectToAction("OperatorSubscription", "Subscription");
                }
                else if (roles.Contains("Tenex_Admin"))
                {
                    _logger.LogInformation("Redirecting to Admin page");
                    return RedirectToAction("Admin", "Admin");
                }
                else
                {
                    _logger.LogWarning("User does not have the required role");
                    TempData["Error"] = "You do not have permission to access this resource.";
                    await _signInManager.SignOutAsync();
                    return View(loginVm);
                }
            }

            _logger.LogWarning("Invalid login attempt for user: {Email}", loginVm.Username);
            TempData["Error"] = "Invalid credentials";
            return View(loginVm);
        }

        [HttpGet]
        // [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult SetNewPassword(string userId, string token)
        {
            var model = new SetNewPasswordViewModel
            {
                UserId = userId,
                Token = token
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetNewPassword(SetNewPasswordViewModel model)
        {
            _logger.LogInformation("Attempting to set initial password for user: {UserId}", model.UserId);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid for user: {UserId}", model.UserId);
                return View(model);
            }

            var result = await _accountRepository.SetInitialPasswordAsync(model.UserId, model.NewPassword);

            if (result)
            {
                _logger.LogInformation("Initial password set succeeded for user: {UserId}", model.UserId);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                _logger.LogError("Failed to set initial password for user: {UserId}", model.UserId);
                ModelState.AddModelError("", "Failed to set new password or password is already set.");
            }

            return View(model);
        }
        /*    [HttpPost]
            public ActionResult CreateNewPassword(string Password, string ConfirmPassword)
            {
                if (Password == ConfirmPassword && IsValidPassword(Password))
                {
                    // Handle successful password creation
                    ViewBag.Message = "Password creation successful, please login with your email and new password";
                    return View();
                }
                else
                {
                    // Handle validation errors
                    ViewBag.Message = "Password creation failed. Please check the requirements and try again.";
                    return View();
                }
            }

            private bool IsValidPassword(string password)
            {
                // Implement your password validation logic here
                return password.Length >= 8 && password.Any(char.IsDigit) && password.Any(char.IsSymbol);
            }
    */

    }
}

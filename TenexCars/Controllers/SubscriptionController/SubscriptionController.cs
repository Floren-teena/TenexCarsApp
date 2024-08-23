using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TenexCars.DataAccess.DTOs;
using TenexCars.DataAccess.Enums;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.DataAccess.ViewModels;
using TenexCars.Interfaces;
using TenexCars.Models.ViewModels;

public class SubscriptionController : Controller
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IOperatorRepository _operatorRepository;
    private readonly ILogger<SubscriptionController> _logger;
    private readonly ISubscriberRepository _subscriberRepository;

    public SubscriptionController(ISubscriptionRepository subscriptionRepository, UserManager<AppUser> userManager, IEmailService emailService, IOperatorRepository operatorRepository, ILogger<SubscriptionController> logger, ISubscriberRepository subscriberRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _userManager = userManager;
        _emailService = emailService;
        _operatorRepository = operatorRepository;
        _logger = logger;
        _subscriberRepository = subscriberRepository;
    }


    [HttpGet]
    public async Task<IActionResult> Subscribers()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        Operator existingOperator = null;

        if (user.Type == "Main_Operator")
        {
            existingOperator = await _operatorRepository.GetOperatorByUserId(user.Id);
        }
        else if (user.Type == "Operator_Team_Member")
        {
            var operatorMember = await _operatorRepository.GetOperatorMemberByUserId(user.Id);
            existingOperator = await _operatorRepository.GetOperatorById(operatorMember.OperatorId);
            ViewBag.CompanyName = existingOperator.CompanyName;
        }
        else
        {
            return BadRequest("Unauthorized user!.");
        }

        if (existingOperator == null)
        {
            _logger.LogInformation("Operator ID is required.");
            return BadRequest();
        }

        var subscriptions = await _subscriptionRepository.GetSubscriptionsByOperatorAsync(existingOperator.Id);
        if (subscriptions == null || !subscriptions.Any())
        {
            return NotFound("No subscriptions found for the operator.");
        }

        
        foreach (var subscription in subscriptions)
        {
            var subscriber = await _subscriberRepository.GetSubscriberByIdAsync(subscription.SubscriberId);
            subscription.Subscriber = subscriber;
        }

       
        var operatorSubscriptionViewModel = new OperatorSubscriptionsViewModel
        {
            Subscriptions = subscriptions
        };

        return View(operatorSubscriptionViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> OperatorSubscription(OperatorSubscriptionViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        Operator existingOperator = null;

        if (user.Type == "Main_Operator")
        {
            existingOperator = await _operatorRepository.GetOperatorByUserId(user.Id);
        }
        else if (user.Type == "Operator_Team_Member")
        {
            var operatorMember = await _operatorRepository.GetOperatorMemberByUserId(user.Id);
            existingOperator = await _operatorRepository.GetOperatorById(operatorMember.OperatorId);
            ViewBag.CompanyName = existingOperator.CompanyName;
        }
        else
        {
            return BadRequest("Invalid user type.");
        }

        if (existingOperator == null)
        {
            _logger.LogInformation("Operator ID is required.");
            return BadRequest();
        }

        var subscription = await _subscriptionRepository.GetSubscriptionForOperator(existingOperator.Id);
        if (subscription == null)
        {
            TempData["error"] = "Operator does not have an existing subscription";
            return View(new List<OperatorSubscriptionViewModel> { model });
        }

        var subscriptions = await _subscriptionRepository.GetAllSubscriptionsForOperator(existingOperator.Id);
        if (subscriptions == null || !subscriptions.Any())
        {
            _logger.LogInformation("No subscriptions found for the given operator.");
            return NotFound();
        }

        var subscriptionForOperator = subscriptions.Select(sub => new OperatorSubscriptionViewModel
        {
            Id = sub.SubscriberId,
            Customer = $"{sub.Subscriber.FirstName} {sub.Subscriber.LastName}",
            VehicleRequest = $"{sub.Vehicle.Make} {sub.Vehicle.Model}",
            RequestDate = sub.RequestDate,
            TermStart = sub.TermStart,
            TermEnd = sub.TermEnd,
            PickupLocation = sub.PickUpLocation,
            VehicleInfo = sub.Vehicle.VIN,
            DriversLicenseUrlFront = sub.DLUrlFront,
            DriversLicenseUrlBack = sub.DLUrlBack,
            SubscriptionStatus = sub.SubscriptionStatus,
            VehicleColor = sub.Vehicle.Color,
            Email = sub.Subscriber.Email
        }).ToList();

       
        return View(subscriptionForOperator);
    }


    [HttpPost]
    public async Task<IActionResult> Approve()
    {
        if (!ModelState.IsValid)
        {
            TempData["error"] = "Invalid request!";
            return RedirectToAction("OperatorSubscription");
        }
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var existingOperator = await _operatorRepository.GetOperatorByUserId(user.Id);
            var subscription = await _subscriptionRepository.GetSubscriptionForOperator(existingOperator.Id);

            subscription.SubscriptionStatus = SubscriptionStatus.Approve;
            await _subscriptionRepository.UpdateSubscription(subscription);

            TempData["success"] = "Subscription approved successfully!";
            return RedirectToAction("OperatorSubscription");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Something went wrong while approving subscription");
            TempData["error"] = "Something went wrong while approving subscription";
            return RedirectToAction("OperatorSubscription");
        }

    }

    [HttpPost]
    public async Task<IActionResult> Contact(OperatorSubscriptionViewModel operatorSubscriptionViewModel)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var existingOperator = await _operatorRepository.GetOperatorByUserId(user.Id);
            var subscription = await _subscriptionRepository.GetSubscriptionForOperator(existingOperator.Id);

            var emailContent = new EmailDto
            {
                To = operatorSubscriptionViewModel.Email,
                Subject = operatorSubscriptionViewModel.Subject,
                Body = operatorSubscriptionViewModel.Body
            };

            await _emailService.ContactApplicantEmailAsync(emailContent);
            TempData["success"] = "Email sent successfully!";
            return RedirectToAction("OperatorSubscription");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Something went wrong when sending email");
            TempData["error"] = "Something went wrong when sending email. Contact Support!";
            return RedirectToAction("OperatorSubscription");
        }
    }


    [HttpPost]
    public async Task<IActionResult> AssignVehicle()
    {
        if (!ModelState.IsValid)
        {
            TempData["error"] = "Invalid request!";
            return RedirectToAction("OperatorSubscription");
        }
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var existingOperator = await _operatorRepository.GetOperatorByUserId(user.Id);
            var subscription = await _subscriptionRepository.GetSubscriptionForOperator(existingOperator.Id);
            if (subscription is not null && subscription.SubscriptionStatus == SubscriptionStatus.Awaiting)
            {
                subscription.SubscriptionStatus = SubscriptionStatus.Active;
                await _subscriptionRepository.UpdateSubscription(subscription);

                /*subscription.SubscriptionStatus = SubscriptionStatus.Assigned;
                await _subscriptionRepository.UpdateSubscription(subscription);*/

                TempData["success"] = "Vehicle assigned successfully!";

                var currentSub = subscription.Subscriber;
                _logger.LogInformation("Preparing to send email to {Email}", currentSub.Email);

                var emailBody = $"<div style=\"color: black;\">" +
                                $"Hello {currentSub.FirstName},<br><br>" +
                                $"You have successfully subscribed to <b>{subscription.Operator.CompanyName}</b> on  Tenex cars." +
                                $"Your <b>{subscription.Vehicle.Make} {subscription.Vehicle.Model}</b> is on its way to you.<br>" +
                                "We hope you enjoy the ride!" +
                                "<br><br>" +
                                "Best regards,<br>" +
                                "The Tenex Team" +
                                "</div>";

                var emailContent = new EmailDto
                {
                    To = currentSub.Email,
                    Subject = "Congratulations! Your Vehicle is on its way to you",
                    Body = emailBody
                };

                await _emailService.ApproveSubscriptionEmail(emailContent);
                _logger.LogInformation("Email sent successfully to {Email}", currentSub.Email);
                return RedirectToAction("OperatorSubscription");
            }
            TempData["error"] = "You can't assign this car yet!";
            return RedirectToAction("OperatorSubscription");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while assigning vehicle");
            TempData["error"] = "Something went wrong while assigning vehicle";
            return RedirectToAction("OperatorSubscription");
        }
    }
}

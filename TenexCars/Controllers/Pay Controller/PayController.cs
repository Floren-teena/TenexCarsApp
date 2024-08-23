using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayStack.Net;
using TenexCars.Models.ViewModels;
using TenexCars.DataAccess.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TenexCars.DataAccess;

namespace TenexCars.Controllers.Pay_Controller
{
    public class PayController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly string token;
        private PayStackApi Paystack { get; set; }
        public PayController(IConfiguration configuration, ApplicationDbContext context) 
        {
            _configuration = configuration;
            _context = context;
            token = _configuration["Payment:PaystackSK"];
            Paystack = new PayStackApi(token);
        }

        [HttpGet]
        public IActionResult Index(string vehicleId)
        {
            if (string.IsNullOrEmpty(vehicleId))
            {
                return NotFound();
            }
            var vehicleRequest = _context.VehicleRequests
                                         .Include(vr => vr.Subscriber)
                                         .Include(vr => vr.Vehicle)
                                         .ThenInclude(v => v.Operator)
                                         .FirstOrDefault(vr => vr.VehicleId == vehicleId);

            if (vehicleRequest == null || vehicleRequest.Subscriber == null || vehicleRequest.Vehicle == null || vehicleRequest.Vehicle.Operator == null)
            {
                return NotFound();
            }

            var payViewModel = new PayViewModel
            {
                Name = $"{vehicleRequest.Subscriber.FirstName} {vehicleRequest.Subscriber.LastName}",
                Email = vehicleRequest.Subscriber.Email, // Default email
                VehicleId = vehicleId,
                SubscriberId = vehicleRequest.SubscriberId,
                OperatorId = vehicleRequest.Vehicle.OperatorId,
                Amount = vehicleRequest.Vehicle.ReservationFee
            };

            return View(payViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PayViewModel pay)
        {
            string reference = Generate().ToString();
            TransactionInitializeRequest request = new()
            {
                AmountInKobo = (int)pay.Amount * 100,
                Email = pay.Email,
                Reference = reference,
                Currency = "NGN",
                CallbackUrl = /*"http://localhost:7127/pay/verify"*/ Url.Action("Verify", "Pay", new { reference }, protocol: HttpContext.Request.Scheme)
            };

            TransactionInitializeResponse response = Paystack.Transactions.Initialize(request);
            if (response.Status)
            {
                var vehicleRequest = _context.VehicleRequests.FirstOrDefault(vr => vr.VehicleId == pay.VehicleId);
                if (vehicleRequest != null)
                {
                    var transaction = new Transaction()
                    {
                        Amount = (decimal)pay.Amount,
                        Email = pay.Email,
                        TrxRef = request.Reference,
                        Name = pay.Name,
                        SubscriberId = pay.SubscriberId, 
                        VehicleId = pay.VehicleId,  
                        OperatorId = pay.OperatorId,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Status = response.Status,
                        PaymentStatus = "PAID"
                        
                                                         
                    };

                    await _context.Transactions.AddAsync(transaction);
                    await _context.SaveChangesAsync();
                    return Redirect(response.Data.AuthorizationUrl);
                }
            }

            ViewData["error"] = response.Message;
            return View(pay);
        }

        [HttpGet]
        public IActionResult Payments()
        {
            var transactions = _context.Transactions
                                .Include(t => t.Vehicle)
                                .Where(t => t.Status == true)
                                .ToList();
            ViewData["transactions"] = transactions;
            return View(); 
        }

        [HttpGet]
        public async Task<IActionResult> Verify(string reference)
        {
            try
            {
                TransactionVerifyResponse response = Paystack.Transactions.Verify(reference);
                Console.WriteLine($"Response: {Newtonsoft.Json.JsonConvert.SerializeObject(response)}");

                if (response.Status && response.Data.Status == "success")
                {
                    var transaction = _context.Transactions.FirstOrDefault(x => x.TrxRef == reference);
                    if (transaction != null)
                    {
                        transaction.Status = true; // Mark the transaction as successful
                        _context.Transactions.Update(transaction);
                        await _context.SaveChangesAsync();
                        ViewData["successMessage"] = "Your payment was successful!";
                        return View();
                    }
                    else
                    {
                        ViewData["error"] = "Transaction not found in database.";
                    }
                }
                else
                {
                    ViewData["error"] = response.Message ?? "Transaction verification failed.";
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception occurred during payment verification: {ex.Message}");
                ViewData["error"] = "An error occurred while verifying the transaction.";
            }

            return View(); // Return the Verify view with appropriate ViewData set
        }


        public static int Generate()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            return rand.Next(180000000, 999999999);
        }

    }
}
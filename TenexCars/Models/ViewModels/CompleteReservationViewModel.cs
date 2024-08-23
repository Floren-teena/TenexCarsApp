using TenexCars.DataAccess.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TenexCars.Models.ViewModels
{
    public class CompleteReservationViewModel
    {
        public string AppUserId { get; set; }
        public string FirstName { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public IFormFile DriversLicenseFront { get; set; }

        [Required]
        public IFormFile DriversLicenseBack { get; set; }

        [Required]
        public string SubscriptionDuration { get; set; }

        [Required]
        public string PickupDate { get; set; }
        public string? VehicleName { get; set; }
        public bool? AdditionalDrivers { get; set; }
        public string? ReservationFee { get; set; }
        public string? DeliveryCost { get; set; }
        public string? MonthlyCost { get; set; }
        public string? TotalCost { get; set; }
        public List<CoSubscriberViewModel> CoSubscribers { get; set; } = new List<CoSubscriberViewModel>();
    }

    public class CoSubscriberViewModel
    {
        public string? CoSubFirstName { get; set; }

        public string? CoSubLastName { get; set; }

        [EmailAddress]
        public string? CoSubEmail { get; set; }

        [Phone]
        public string? CoSubPhone { get; set; }
    }
}

using TenexCars.DataAccess.Models;

namespace TenexCars.Models.ViewModels
{
    public class OperatorInventoryViewModel
    {
        public string? VehicleId { get; set; }
        public string? VehicleName { get; set; }
        public string? Status { get; set; }
        public string? TrackerStatus { get; set; }

        public string? SubscriberId { get; set; }
        public string? SubscriberName { get; set; }

    }
}

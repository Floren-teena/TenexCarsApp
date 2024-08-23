using TenexCars.DataAccess.Models;

namespace TenexCars.Models.ViewModels
{
    public class PayViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public double? Amount { get; set; }
        public string? VehicleId { get; set; }
        //public Vehicle? Vehicle { get; set; }
        public string? SubscriberId { get; set; }
        //public Subscriber? Subscriber { get; set; }
        public string? OperatorId { get; set; }
        //public string? ReservationFee { get; set; }
        //public Operator? Operator { get; set; }
    }
}

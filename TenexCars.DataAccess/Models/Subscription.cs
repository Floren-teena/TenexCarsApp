using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Enums;

namespace TenexCars.DataAccess.Models
{
    public class Subscription
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? SubscriberId { get; set; }
        public Subscriber? Subscriber { get; set; }
        public string? DLUrlFront { get; set; }
        public string? DLUrlBack { get; set; }
        public string? VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
        public string? OperatorId { get; set; }
        public Operator? Operator { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime? TermStart { get; set; }
        public DateTime? TermEnd { get; set; }
        public string? Duration { get; set; }
        public string? PickUpLocation { get; set; }
        public bool SoftCredit { get; set; } = false;
        public string? SubscriptionStatus { get; set; }
        //public StatusActions? Actions { get; set; }
        //public TrackerStatus TrackerStatus { get; set; }
    }
}


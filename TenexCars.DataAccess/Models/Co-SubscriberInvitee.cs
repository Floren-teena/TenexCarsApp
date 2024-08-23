using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenexCars.DataAccess.Models
{
    public class Co_SubscriberInvitee : BaseEntity
    {
        public string? SubscriberId { get; set; }
        public Subscriber? Subscriber { get; set; }

        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public string? VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}

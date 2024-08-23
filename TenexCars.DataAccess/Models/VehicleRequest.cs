using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenexCars.DataAccess.Models
{
    public class VehicleRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? VehicleId { get; set; }
        public string? SubscriberId { get; set; }
        public Subscriber? Subscriber { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}

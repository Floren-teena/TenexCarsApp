using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Enums;

namespace TenexCars.DataAccess.Models
{
    public class Subscriber: BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string HomeAddress { get; set; }
        public string Country { get; set; }
        public State State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public List<Transaction>? Transactions { get; set; }

    }
}

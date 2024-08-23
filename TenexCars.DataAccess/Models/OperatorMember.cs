using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenexCars.DataAccess.Models
{
    public class OperatorMember : BaseEntity
    {
        public  string? OperatorId { get; set; }
        public Operator? Operator { get; set; }

        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public string? Role { get; set; }


    }
}

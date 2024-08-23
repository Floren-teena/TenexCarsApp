 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenexCars.DataAccess.Enums
{
    public  class SubscriptionStatus
    {
        
        public const string Credit = "CreditFailed";
        public const string DLNeeded = "DLNeeded";
        public const string Pending = "Pending";
        public const string Approve = "Approved";
        public const string Awaiting = "AwaitingAssignment";
        public const string Assigned = "Assigned";
        public const string Delivered = "Delivered";
        public const string Active = "Active";
        public const string Reupload = "Re-Upload";
        public const string Cancelled = "Cancelled";
        public const string Completed = "Completed";
        public const string Alternate = "Alternative";
    }
}

using TenexCars.DataAccess.Enums;
using TenexCars.DataAccess.Models;

namespace TenexCars.Models.ViewModels
{
    public class OperatorCompleteprofileViewModel
    {
        public string? PhoneNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }
        public DateTime ContactDOB { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public string? IdentificationDocument { get; set; }
        public string? CompanyRegistrationDocument { get; set; }
        public string? OperatorSubscriptionDuration { get; set; }
        public string? BusinessName { get; set; }
        public InsuranceOffering InsuranceOffering { get; set; }
        public MultipleSubscribers MultipleSubscribers { get; set; }
        public string? CompanyLogo { get; set; }
        public string? HeroGraphics { get; set; }
        public string? FAQLink { get; set; }
        public string? ContactLink { get; set; }
        public string? SupportContact1 { get; set; }
        public string? SupportContact2 { get; set; }

    }
}

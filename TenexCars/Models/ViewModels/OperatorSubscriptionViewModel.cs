namespace TenexCars.Models.ViewModels
{
    public class OperatorSubscriptionViewModel
    {
        public string Id { get; set; }
        public string Customer { get; set; }
        public string VehicleRequest { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? TermStart { get; set; }
        public DateTime? TermEnd { get; set; }
        public string? PickupLocation { get; set; }
        public string VehicleInfo { get; set; }
        public string? DriversLicenseUrlFront { get; set; }
        public string? DriversLicenseUrlBack { get; set; }
        public string? SubscriptionStatus { get; set; }
        public string? Email { get; set; }
        public string? VehicleColor { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }

    public class OperatorSubscriptionViewModelList
    {
        public List<OperatorSubscriptionViewModel> Items { get; set; }
    }
}

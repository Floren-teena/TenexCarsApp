namespace TenexCars.Models.ViewModels
{
    public class OperatorDashboardViewModel
    {
        public string OperatorId { get; set; }
        public string OperatorLogo { get; set; }
        public string? CompanyLogo { get; set; }
        public string OperatorImage { get; set; }
        public int TotalNumberOfVehicles { get; set; }
        public int TotalNumberOfSubscribers { get; set; }
        public int TotalNumberOfActiveCars { get; set; }
        public int TotalNumberOfReservedCars { get; set; }
        public List<int> CurrentMonthStats { get; set; }
        public List<int> PastMonthStats { get; set; }
    }
}
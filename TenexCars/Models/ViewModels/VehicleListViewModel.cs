using TenexCars.DataAccess.Enums;
using TenexCars.DataAccess.Models;

namespace TenexCars.Models.ViewModels
{
    public class VehicleListViewModel
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public double? MonthlyCost { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public CarType? Cartype { get; set; }
        public string? Color { get; set; }
        public string? SeatNumbers { get; set; }
        public State? State { get; set; }
        public string? CompanyName { get; set; }
        public IEnumerable<Vehicle>? Vehicles { get; set; }
    }
}

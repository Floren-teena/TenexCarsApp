using TenexCars.DataAccess.Enums;

namespace TenexCars.Models.ViewModels
{
	public class ReserveCarViewModel
	{
        public string VehicleId { get; set; }
        public string? ImageUrl { get; set; }

        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? HomeAddress { get; set; }
		public string? Country { get; set; }
		public State State { get; set; }
		public string? City { get; set; }

		public string? ZipCode { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }


	}
}

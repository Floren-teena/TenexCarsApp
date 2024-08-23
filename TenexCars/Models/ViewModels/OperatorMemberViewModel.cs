using TenexCars.DataAccess.Models;

namespace TenexCars.Models.ViewModels
{
	public class OperatorMemberViewModel
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public string Role { get; set; } = string.Empty;
		public List<OperatorMember>? OperatorMembers { get; set; }
		//public List<Operator>?
	}
}

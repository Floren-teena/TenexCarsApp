using System.Threading.Tasks;
using TenexCars.DataAccess.Models;

namespace TenexCars.DataAccess.Repositories.Interfaces
{
	public interface IAccountRepository
	{
		Task<Operator> GetOperatorById(string Id);
		Task<bool> SetInitialPasswordAsync(string userId, string newPassword);
	
		
		//Task<string> GeneratePasswordResetTokenAsync(string userId);
	}
}

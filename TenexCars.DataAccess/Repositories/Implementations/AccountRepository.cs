using Microsoft.AspNetCore.Identity;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.DataAccess;
using Microsoft.EntityFrameworkCore;

public class AccountRepository : IAccountRepository
{

	private readonly ApplicationDbContext _context;
	private readonly UserManager<AppUser> _userManager;


	public AccountRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
	{
		_context = context;
		_userManager = userManager;
	}

	public async Task<Operator> GetOperatorById(string Id)
	{
		return await _context.Operators.FirstOrDefaultAsync(x => x.Id == Id);
	}

	public async Task<bool> SetInitialPasswordAsync(string userId, string newPassword)
	{
		var user = await _userManager.FindByIdAsync(userId);
		if (user == null)
		{
			throw new Exception("User not found");
		}

		var token = await _userManager.GeneratePasswordResetTokenAsync(user);
		var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

		return result.Succeeded;
	}
}
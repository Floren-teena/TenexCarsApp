using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;

namespace TenexCars.DataAccess.Repositories.Implementations
{
	public class VehicleRequestRepository : IVehicleRequestRepository
	{
		private readonly ApplicationDbContext _context;
		public VehicleRequestRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<VehicleRequest> AddVehicleRequestLog(VehicleRequest request)
		{
			var newRequest = await _context.VehicleRequests.AddAsync(request);
			await _context.SaveChangesAsync();
			return newRequest.Entity;
		}

        public async Task<VehicleRequest> GetVehicleRequestBySubscriberId(string id)
        {
            return await _context.VehicleRequests.FirstOrDefaultAsync(r => r.VehicleId == id);
        }
    }
}

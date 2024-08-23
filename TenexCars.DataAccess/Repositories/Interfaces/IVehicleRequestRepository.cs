using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;

namespace TenexCars.DataAccess.Repositories.Interfaces
{
	public interface IVehicleRequestRepository
	{
		Task<VehicleRequest> AddVehicleRequestLog(VehicleRequest request);
		Task<VehicleRequest> GetVehicleRequestBySubscriberId(string id);
	}
}

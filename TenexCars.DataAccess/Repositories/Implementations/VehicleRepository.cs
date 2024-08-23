using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Enums;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.DataAccess.ViewModels;

namespace TenexCars.DataAccess.Repositories.Implementations
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;
        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Vehicle> GetVehicleById(string Id)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicles()
        {
            var veh = await _context.Vehicles.Include(o => o.Operator).ToListAsync();
            var availableVehicles = new List<Vehicle>();
            foreach (var vehicle in veh)
            {
                var subscription = await _context.Subscriptions
                             .Where(s => s.VehicleId == vehicle.Id && s.SubscriptionStatus == "Active")
                             .FirstOrDefaultAsync();

                if (subscription == null)
                {
                    availableVehicles.Add(vehicle);
                }
                }
            return availableVehicles;
        }

        public async Task<List<Vehicle>> GetAllInActiveVehicles(IEnumerable<Vehicle> vehicles)
        {
            var activeSubscriptions = await _context.Subscriptions
                .Where(s => s.SubscriptionStatus == "Active")
                .Select(s => s.VehicleId)
                .ToListAsync();

            var vehiclesWithoutActiveSubscriptions = vehicles
                .Where(v => !activeSubscriptions.Contains(v.Id))
                .ToList();

            return vehiclesWithoutActiveSubscriptions;
        }

        public async Task<IEnumerable<Vehicle>> GetAll(QueryObject query)
        {
            var vehicles = _context.Vehicles.Include(o => o.Operator).AsQueryable();

            if (query.CarModel != null)
            {
                vehicles = vehicles.Where(v => v.Model == query.CarModel);
            }
            if (query.CarMake != null)
            {
                vehicles = vehicles.Where(v => v.Make == query.CarMake);
            }
            if (query.CarType != null)
            {
                vehicles = vehicles.Where(v => v.Cartype == query.CarType);
            }
            if (query.CompanyName != null)
            {
                vehicles = vehicles.Where(v => v.Operator.CompanyName == query.CompanyName);
            }
         
            return await vehicles.ToListAsync();
        }

        public async Task<List<Vehicle>> GetAllVehiclesByOperator(string operatorId)
        {
            var vehicles = _context.Vehicles.Include(v => v.Operator).Where(v => v.OperatorId == operatorId).ToList();
            var availableVehicles = new List<Vehicle>();
            foreach (var vehicle in vehicles)
            {
                var subscription = await _context.Subscriptions
                             .Where(s => s.VehicleId == vehicle.Id && s.SubscriptionStatus == "Active")
                             .FirstOrDefaultAsync();

                if (subscription == null)
                {
                    availableVehicles.Add(vehicle);
                }
            }

            return availableVehicles;
        }

        public async Task<List<Vehicle>> GetAllVehicleByOperator(string operatorId)
        {
            var vehicles = _context.Vehicles.Include(v => v.Operator).Where(v => v.OperatorId == operatorId).ToList();
            return vehicles;
        }

        public async Task<List<Vehicle>> GetAllVehiclesByOperatorFilter(string operatorId, QueryObject query)
        {
            var vehicles = _context.Vehicles.AsQueryable();
            if (!string.IsNullOrEmpty(operatorId))
            {
                vehicles = vehicles.Where(v => v.OperatorId == operatorId);
            }

            if (!string.IsNullOrEmpty(query.CarModel))
            {
                vehicles = vehicles.Where(v => v.Model == query.CarModel);
            }
            if (!string.IsNullOrEmpty(query.CarMake))
            {
                vehicles = vehicles.Where(v => v.Make == query.CarMake);
            }
            if (query.CarType.HasValue)
            {
                vehicles = vehicles.Where(v => v.Cartype == query.CarType.Value);
            }
         
            return await vehicles.ToListAsync();
        }

        public async Task<List<Vehicle>> GetVehiclesByOperator(string operatorId)
        {
            return await _context.Vehicles.Where(v => v.OperatorId == operatorId).ToListAsync();
        }


        public async Task CreateVehicleAsync(CreateVehicleViewModel vehicleViewModel)
        {
            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid().ToString(),
                Make = vehicleViewModel.Make,
                Model = vehicleViewModel.Model,
                PlateNumber = vehicleViewModel.PlateNumber,
                CarDescription = vehicleViewModel.CarDescription,
                ChasisNumber = vehicleViewModel.ChasisNumber,
                SeatNumbers = vehicleViewModel.SeatNumbers,
                Mileage = vehicleViewModel.Mileage,
                TrunkSize = vehicleViewModel.TrunkSize,
                Color = vehicleViewModel.Color,
                VIN = vehicleViewModel.VIN,
                PickupAddress = vehicleViewModel.PickupAddress,
                City = vehicleViewModel.City,
                ZIP = vehicleViewModel.ZIP,
                OperatorId = vehicleViewModel.OperatorId,
                CarDealerName = vehicleViewModel.CarDealerName,
                FinacialStartDate = vehicleViewModel.FinacialStartDate,
                FinacialEndDate = vehicleViewModel.FinacialEndDate,
                CarWarrantyOverview = vehicleViewModel.CarWarrantyOverview,
                Torque = vehicleViewModel.Torque,
                TransmissionType = vehicleViewModel.TransmissionType,
                Horsepower = vehicleViewModel.Horsepower,
                TurningDiameter = vehicleViewModel.TurningDiameter,
                CurbWeight = vehicleViewModel.CurbWeight,
                DiscBrakes = vehicleViewModel.DiscBrakes,
                TransmissionSpeed = vehicleViewModel.TransmissionSpeed,
                DriveAxleRatio = vehicleViewModel.DriveAxleRatio,
                StabilityControl = vehicleViewModel.StabilityControl,
                RangeOfFullCharge = vehicleViewModel.RangeOfFullCharge,
                CargoSpace = vehicleViewModel.CargoSpace,
                TouchScreenDisplay = vehicleViewModel.TouchScreenDisplay,
                DriverLumbarSupport = vehicleViewModel.DriverLumbarSupport,
                NumberOfSpeakers = vehicleViewModel.NumberOfSpeakers,
                Radio = vehicleViewModel.Radio,
                AndroidAuto_AppleCarPlay = vehicleViewModel.AndroidAuto_AppleCarPlay,
                TouchScreenNavSystem = vehicleViewModel.TouchScreenNavSystem,
                ProjectorBeamLedHeadlight = vehicleViewModel.ProjectorBeamLedHeadlight,
                RemoteKeylessEntry = vehicleViewModel.RemoteKeylessEntry,
                StandardLowTirePressureWarning = vehicleViewModel.StandardLowTirePressureWarning,
                BluetoothSystem = vehicleViewModel.BluetoothSystem,
                WheelDrive = vehicleViewModel.WheelDrive,
                EngineType = vehicleViewModel.EngineType,
                //CarName = vehicleViewModel.CarName,
                //CarModel = vehicleViewModel.CarModel,
                State = vehicleViewModel.State,
                DcFastCharging = vehicleViewModel.DcFastCharging,
                HomeCharger = vehicleViewModel.HomeCharger,
                SeatHeater = vehicleViewModel.SeatHeater,
                Cartype = vehicleViewModel.Cartype,
                ReservationFee = vehicleViewModel.ReservationFee,
                TotalCostOfCar = vehicleViewModel.TotalCostOfCar,
                ActivationFee = vehicleViewModel.ActivationFee,
                MonthlyCost = vehicleViewModel.MonthlyCost,
                MarketValue = vehicleViewModel.MarketValue,
                DecrementMarketValue = vehicleViewModel.DecrementMarketValue,
                //ImageUrl = vehicleViewModel.ImageUrl,
                //PublicId = vehicleViewModel.PublicId
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
        }
        public void UpdateVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
        }
        public bool VehicleExists(string id)
        {
            return _context.Vehicles.Any(v => v.Id == id);
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            var newVehicle = await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            return newVehicle.Entity;
        }

        public async Task<IEnumerable<Vehicle>> GetTopUniqueVehiclesAsync(int count = 8)
        {
            return await _context.Vehicles
                .GroupBy(v => new { v.Make, v.Model })
                .Select(g => g.FirstOrDefault())
                .Take(count)
                .ToListAsync();
        }
    }
}
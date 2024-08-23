using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using TenexCars.DataAccess.Enums;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Implementations;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.DataAccess.ViewModels;
using TenexCars.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace TenexCars.Controllers.Vehicle_Controller
{

    public class VehicleController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> FleetOfCars()
        {
            var vehicles = await _vehicleRepository.GetAllVehicles();

            var vehiclesWithoutActiveSubscriptions = await _vehicleRepository.GetAllInActiveVehicles(vehicles);

            var type = Enum.GetValues(typeof(CarType)).Cast<CarType>();
            var allMakes = vehiclesWithoutActiveSubscriptions.Select(x => x.Make).Distinct().ToList();
            var allModels = vehiclesWithoutActiveSubscriptions.Select(x => x.Model).Distinct().ToList();
            var allCompanyNames = vehiclesWithoutActiveSubscriptions.Select(x => x.Operator.CompanyName).Distinct().ToList();

            ViewBag.CarType = type;
            ViewBag.Make = allMakes;
            ViewBag.CarModels = allModels;
            ViewBag.CompanyName = allCompanyNames;

            var vehicleProperties = vehiclesWithoutActiveSubscriptions.Select(props => new VehicleListViewModel
            {
                MonthlyCost = props.MonthlyCost,
                Make = props.Make,
                Model = props.Model,
                Cartype = props.Cartype,
                Color = props.Color,
                SeatNumbers = props.SeatNumbers,
                State = props.State,
                CompanyName = props.Operator.CompanyName,
                ImageUrl = props.ImageUrl,
                Id = props.Id
            }).ToList();

            return View(vehicleProperties);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FleetOfCars(QueryObject query)
        {
            var vehicles = await _vehicleRepository.GetAll(query);

            var filteredVehicles = await _vehicleRepository.GetAllInActiveVehicles(vehicles);

            var allVehicles = await _vehicleRepository.GetAllVehicles();
            var type = Enum.GetValues(typeof(CarType)).Cast<CarType>();
            var allMakes = allVehicles.Select(x => x.Make).Distinct().ToList();
            var allModels = allVehicles.Select(x => x.Model).Distinct().ToList();
            var allCompanyNames = allVehicles.Select(x => x.Operator.CompanyName).Distinct().ToList();

            ViewBag.SelectedCarType = query.CarType;
            ViewBag.SelectedCarMake = query.CarMake;
            ViewBag.SelectedCarModel = query.CarModel;
            ViewBag.SelectedCompanyName = query.CompanyName;

            ViewBag.CarType = type;
            ViewBag.Make = allMakes;
            ViewBag.CarModels = allModels;
            ViewBag.CompanyName = allCompanyNames;

            var vehicleProperties = filteredVehicles.Select(props => new VehicleListViewModel
            {
                MonthlyCost = props.MonthlyCost,
                Make = props.Make,
                Model = props.Model,
                Cartype = props.Cartype,
                Color = props.Color,
                SeatNumbers = props.SeatNumbers,
                State = props.State,
                CompanyName = props.Operator.CompanyName,
                ImageUrl = props.ImageUrl,
                Id = props.Id
            }).ToList();

            return View(vehicleProperties);
        }
    }

}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Enums;

namespace TenexCars.DataAccess.ViewModels
{
    public class CreateVehicleViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string? Make { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Model { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? PlateNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? CarDescription { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? ChasisNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? SeatNumbers { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Mileage { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? TrunkSize { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Color { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? VIN { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? PickupAddress { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? City { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? ZIP { get; set; }
        public string? OperatorId { get; set; }
        public string? CarDealerName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? FinacialStartDate { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? FinacialEndDate { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? CarWarrantyOverview { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Torque { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? TransmissionType { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Horsepower { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? TurningDiameter { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? CurbWeight { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? DiscBrakes { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? TransmissionSpeed { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? DriveAxleRatio { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? StabilityControl { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? RangeOfFullCharge { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? CargoSpace { get; set; }
        public bool TouchScreenDisplay { get; set; }
        public bool DriverLumbarSupport { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? NumberOfSpeakers { get; set; }
        public bool Radio { get; set; }
        public bool AndroidAuto_AppleCarPlay { get; set; }
        public bool TouchScreenNavSystem { get; set; }
        public bool ProjectorBeamLedHeadlight { get; set; }
        public bool RemoteKeylessEntry { get; set; }
        public bool StandardLowTirePressureWarning { get; set; }
        public bool BluetoothSystem { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public WheelDrive? WheelDrive { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public EngineType? EngineType { get; set; }
        //public CarName CarName { get; set; }
        //public CarModel CarModel { get; set; }
        public State State { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DcFastCharging DcFastCharging { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public HomeCharger HomeCharger { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public SeatHeater SeatHeater { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public CarType Cartype { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public double? ReservationFee { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public double? TotalCostOfCar { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public double? ActivationFee { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public double? MonthlyCost { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public double? MarketValue { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public double? DecrementMarketValue { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public IFormFile? ImageUrl { get; set; }
        //public string? PublicId { get; set; }
        public string? OperatorLogoUrl { get; set; }
    }
}

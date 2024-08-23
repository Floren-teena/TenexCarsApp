using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TenexCars.DataAccess.Models
{
    public class Vehicle
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();     
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? PlateNumber { get; set; }
        public string? CarDescription { get; set; }
        public string? ChasisNumber { get; set; }
        public string? SeatNumbers { get; set; }
        public string? Mileage { get; set; }
        public string? TrunkSize { get; set; }
        public string? Color { get; set; }
        public string? VIN { get; set; }
        public string? PickupAddress { get; set; }


        public string City { get; set; }
        public string ZIP { get; set; }


        public string? OperatorId { get; set; }
        public Operator? Operator { get; set; }
        public string? CarDealerName { get; set; }
        public string? FinacialStartDate { get; set; }
        public string? FinacialEndDate { get; set; }
        public string? CarWarrantyOverview { get; set; }
        public string? Torque { get; set; }
        public string? TransmissionType { get; set; }
        public string? Horsepower { get; set; }

        public string? TurningDiameter { get; set; }
        public string? CurbWeight { get; set; }
        public string? DiscBrakes { get; set; }
       
        
        public string? TransmissionSpeed { get; set; }
        public string? DriveAxleRatio { get; set; }
        public string? StabilityControl { get; set; }
        public string? RangeOfFullCharge { get; set; }
        public string? CargoSpace { get; set; }
        public bool? TouchScreenDisplay { get; set; }

        public bool? DriverLumbarSupport { get; set; }
        public string? NumberOfSpeakers { get; set; }
        public bool? Radio { get; set; }
        public bool? AndroidAuto_AppleCarPlay { get; set; }
        public bool? TouchScreenNavSystem { get; set; }
        public bool? ProjectorBeamLedHeadlight { get; set; }
        public bool? RemoteKeylessEntry { get; set; }
        public bool? StandardLowTirePressureWarning { get; set; }
        public bool? BluetoothSystem { get; set; }
        public WheelDrive? WheelDrive { get; set; }
        public EngineType? EngineType { get; set; }
        public State? State { get; set; }
        public DcFastCharging? DcFastCharging { get; set; }
        public HomeCharger? HomeCharger { get; set; }
        public SeatHeater? SeatHeater { get; set; }
        public CarType? Cartype { get; set; }
        public double? ReservationFee { get; set; }
        public double? TotalCostOfCar { get; set; }
        public double? ActivationFee { get; set; }
        public double? MonthlyCost { get; set; }
        public double? MarketValue { get; set; }
        public double? DecrementMarketValue { get; set; }
        public string? ImageUrl  { get; set; }
        public string? PublicId { get; set; }
        public List<Transaction>? Transactions { get; set; }
        public string? InsuranceDocument { get; set; }

        public string VehicleStatus { get; set; } = Enums.VehicleStatus.Unassigned;
    }
}


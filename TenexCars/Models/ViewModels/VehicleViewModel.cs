using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TenexCars.DataAccess.Enums;

public class VehicleViewModel
{
    public string Id { get; set; }
    public CarType? Cartype { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? ChasisNumber { get; set; }
    public string? SeatNumbers { get; set; }
    public string? Mileage { get; set; }
    public string? TrunkSize { get; set; }
    /*public string? InsuranceStartDate { get; set; }
    public string? InsuranceEndDate { get; set; }*/
    public string? Color { get; set; }
    public string? VIN { get; set; }
    public string? PickupAddress { get; set; }
    public State? State { get; set; }
    public string? City { get; set; }
    public string? ZIP { get; set; }
    public string? CarDealerName { get; set; }
    public string? FinacialStartDate { get; set; }
    public string? FinacialEndDate { get; set; }
    //public IFormFile InsuranceDocument { get; set; }
    public string? Torque { get; set; }
    public string? TransmissionType { get; set; }
    public string? Horsepower { get; set; }
    public string? TurningDiameter { get; set; }
    public string? CurbWeight { get; set; }
    public string? DiscBrakes { get; set; }
    public string? TransmissionSpeed { get; set; }
    public WheelDrive? WheelDrive { get; set; }
    public EngineType? EngineType { get; set; }
    public string? CargoSpace { get; set; }
    public DcFastCharging? DcFastCharging { get; set; }
    public HomeCharger? HomeCharger { get; set; }
    public SeatHeater? SeatHeater { get; set; }
    public string? NumberOfSpeakers { get; set; }
    public string? RangeOfFullCharge { get; set; }
    public bool Radio { get; set; }
    public bool AndroidAuto_AppleCarPlay { get; set; }
    public bool TouchScreenNavSystem { get; set; }
    public bool ProjectorBeamLedHeadlight { get; set; }
    public bool DriverLumbarSupport { get; set; }
    public bool TouchScreenDisplay { get; set; }
    public bool RemoteKeylessEntry { get; set; }
    public bool StandardLowTirePressureWarning { get; set; }
    public bool BluetoothSystem { get; set; }
    public string? CarPictures { get; set; }
    public IFormFile? InsuranceDocument { get; set; }
}
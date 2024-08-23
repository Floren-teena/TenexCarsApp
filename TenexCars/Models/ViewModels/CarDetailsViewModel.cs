using TenexCars.DataAccess.Enums;

namespace TenexCars.Models.ViewModels
{
    public class CarDetailsViewModel
    {
        public string Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? MarketValue { get; set; }
        public string? ImageUrl { get; set; }
        public string? Color { get; set; }
        public string? SeatNumbers { get; set; }
        public string? TrunkSize { get; set; }
        public DcFastCharging? DcFastCharging { get; set; }
        public HomeCharger? HomeCharger { get; set; }
        public string? RangeOfFullCharge { get; set; }
        public string? CarDescription { get; set; }
        public bool? TouchScreenDisplay { get; set; }
        public WheelDrive? WheelDrive { get; set; }
        public bool? DriverLumbarSupport { get; set; }
        public SeatHeater? SeatHeater { get; set; }
        public bool? Radio { get; set; }
        public bool? AndroidAuto_AppleCarPlay { get; set; }
        public string? NumberOfSpeakers { get; set; }
        public bool? TouchScreenNavSystem { get; set; }
        public bool? BluetoothSystem { get; set; }
        public bool? ProjectorBeamLedHeadlight { get; set; }
        public bool? RemoteKeylessEntry { get; set; }
        public bool? StandardLowTirePressureWarning { get; set; }
        public string? Torque { get; set; }
        public string? Horsepower { get; set; }
        public EngineType? EngineType { get; set; }
        public string? DiscBrakes { get; set; }
        public string? StabilityControl { get; set; }
        public string? TransmissionSpeed { get; set; }
        public string? TurningDiameter { get; set; }
        public string? CurbWeight { get; set; }
        public string? TransmissionType { get; set; }
        public string? DriveAxleRatio { get; set; }
        public string? CarWarrantyOverview { get; set; }
    }

}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Enums;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;

namespace TenexCars.DataAccess
{
    public class SeedData
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IOperatorRepository _operatorRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ISubscriberRepository _subscriberRepository;

        public SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext, 
            IOperatorRepository operatorRepository, IVehicleRepository vehicleRepository, ISubscriberRepository subscriberRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _applicationDbContext = applicationDbContext;
            _operatorRepository = operatorRepository;
            _vehicleRepository = vehicleRepository;
            _subscriberRepository = subscriberRepository;
        }

        public async Task SeedAsync()
        {
            await SeedRolesAsync();
            await SeedUsersOperatorVehicleAsync();
           
        }

        private async Task SeedRolesAsync()
        {

            string[] roles = { "Main_Subscriber", "Co_Subscriber", "Main_Operator", "Operator_Team_Member", "Tenex_Admin", "Tenex_Member" };
            foreach (var roleName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private async Task SeedUsersOperatorVehicleAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            //if (!_userManager.Users.Any())
            //{
            //    var adminUser = new AppUser
            //    {
            //        UserName = "adminprime@mailinator.com",
            //        Email = "adminprime@mailinator.com",
            //        FirstName = "Admin",
            //        LastName = "Prime",
            //        Type = "Tenex_Admin"
            //    };

            //    await _userManager.CreateAsync(adminUser, "199026_Ll");


            //    await _userManager.AddToRoleAsync(adminUser, "Tenex_Admin");
            //}

            if (!users.Any(u => _userManager.IsInRoleAsync(u, "Main_Operator").Result))
            {
                var operatorUser = new AppUser
                {
                    UserName = "alice.smith@example.com",
                    Email = "alice.smith@example.com",
                    FirstName = "Alice",
                    LastName = "Smith",
                    Type = "Main_Operator",

                };

                await _userManager.CreateAsync(operatorUser, "199026_Ll");
                await _userManager.AddToRoleAsync(operatorUser, "Main_Operator");

                var operatorUser2 = new AppUser
                {
                    UserName = "j.berg@mailinator.com",
                    Email = "j.berg@mailinator.com",
                    FirstName = "John",
                    LastName = "Bergkamp",
                    Type = "Main_Operator",

                };

                await _userManager.CreateAsync(operatorUser2, "199026_Ll");
                await _userManager.AddToRoleAsync(operatorUser2, "Main_Operator");


                var operatorUser3 = new AppUser
                {
                    UserName = "manor@mailinator.com",
                    FirstName = "Manuel",
                    LastName = "Ortega",
                    Email = "manor@mailinator.com",
                    Type = "Main_Operator",

                };

                await _userManager.CreateAsync(operatorUser3, "199026_Ll");
                await _userManager.AddToRoleAsync(operatorUser3, "Main_Operator");

                var firstOperator = new Operator
                {
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "alice.smith@example.com",
                    PhoneNumber = "987-654-3210",
                    CompanyName = "Alice Smith Cars",
                    CompanyAddress = "456 Oak St",
                    Country = "Canada",
                    State = "Ontario",
                    City = "Toronto",
                    Zip = "12345",
                    ContactDOB = new DateTime(1990, 10, 25),
                    SupportContact1 = "help@xyzcorp.com",
                    SupportContact2 = "help@xyzcorpz.com",
                    InsuranceOffering = 0,
                    OperatorSubscriptionDuration = "3 months",
                    MultipleSubscribers = 0,
                    IdentificationDocument = "ID987654",
                    CompanyRegistrationDocument = "documents.docx",
                    CompanyLogo = "https://res.cloudinary.com/dujlhcson/image/upload/v1721435894/alicesmithlogo_kfqcpw.jpg",
                    HeroGraphics = "hero.png",
                    BusinessName = "Alice Smith Cars",
                    AppUserId = operatorUser.Id,
                    AppUser = operatorUser,
                    


                };

                await _operatorRepository.AddOperatorAsync(firstOperator);

                var secondOperator = new Operator
                {
                    
                    FirstName = "John",
                    LastName = "Bergkamp",
                    Email = "j.berg@mailinator.com",
                    PhoneNumber = "987-654-3210",
                    CompanyName = "BergKamp Cars",
                    CompanyAddress = "456 Maple St",
                    Country = "United States of America",
                    State = "Texas",
                    City = "Austin",
                    Zip = "12345",
                    ContactDOB = new DateTime(1988, 10, 25),
                    SupportContact1 = "help@xyzcorp.com",
                    SupportContact2 = "help@xyzcorpz.com",
                    InsuranceOffering = 0,
                    OperatorSubscriptionDuration = "6 months",
                    MultipleSubscribers = 0,
                    IdentificationDocument = "ID987654",
                    CompanyRegistrationDocument = "documents.docx",
                    CompanyLogo = "https://res.cloudinary.com/dujlhcson/image/upload/v1721435961/johnbergkamplogo_rcmlcn.jpg",
                    HeroGraphics = "hero.png",
                    BusinessName = "BergKamp Cars",
                    AppUserId = operatorUser2.Id,
                    AppUser = operatorUser2

                };
                await _operatorRepository.AddOperatorAsync(secondOperator);

                var thirdOperator = new Operator
                {
                    
                    FirstName = "Manuel",
                    LastName = "Ortega",
                    Email = "manor@mailinator.com",
                    PhoneNumber = "987-654-3210",
                    CompanyName = "RoadRash Autos",
                    CompanyAddress = "456 Acorn Lane",
                    Country = "United States of America",
                    State = "Michigan",
                    City = "Detroit",
                    Zip = "12345",
                    ContactDOB = new DateTime(1970, 10, 25),
                    SupportContact1 = "help@xyzcorp.com",
                    SupportContact2 = "help@xyzcorpz.com",
                    InsuranceOffering = 0,
                    OperatorSubscriptionDuration = "9 months",
                    MultipleSubscribers = 0,
                    IdentificationDocument = "ID987654",
                    CompanyRegistrationDocument = "documents.docx",
                    CompanyLogo = "https://res.cloudinary.com/dujlhcson/image/upload/v1721435518/BERGK_yop85k.jpg",
                    HeroGraphics = "hero.png",
                    BusinessName = "RoadRash Autos",
                    AppUser = operatorUser3,
                    AppUserId = operatorUser3.Id

                };
                await _operatorRepository.AddOperatorAsync(thirdOperator);


                var firstVehicle = new Vehicle
                {
                    
                    Make = "Toyota",
                    Model = "Camry",
                    PlateNumber = "ABC123",
                    CarDescription = "A reliable and fuel-efficient sedan.",
                    ChasisNumber = "1HGBH41JXMN109186",
                    SeatNumbers = "5",
                    Mileage = "15000",
                    TrunkSize = "13 cubic feet",
                    Color = "White",
                    VIN = "1HGCM82633A123456",
                    PickupAddress = "123 Main St, Anytown, USA",
                    City = "Anytown",
                    ZIP = "12345",
                    OperatorId = firstOperator.Id,
                    CarDealerName = firstOperator.BusinessName,
                    FinacialStartDate = "2021-01-01",
                    FinacialEndDate = "2026-01-01",
                    CarWarrantyOverview = "5 years / 60,000 miles",
                    Torque = "126 lb-ft",
                    TransmissionType = "Automatic",
                    Horsepower = "132 HP",
                    TurningDiameter = "35.6 ft",
                    CurbWeight = "2900 lbs",
                    DiscBrakes = "Yes",
                    TransmissionSpeed = "6",
                    DriveAxleRatio = "3.24",
                    StabilityControl = "Yes",
                    RangeOfFullCharge = null,
                    CargoSpace = "13 cubic feet",
                    TouchScreenDisplay = true,
                    DriverLumbarSupport = true,
                    NumberOfSpeakers = "6",
                    Radio = true,
                    AndroidAuto_AppleCarPlay = true,
                    TouchScreenNavSystem = true,
                    ProjectorBeamLedHeadlight = true,
                    RemoteKeylessEntry = true,
                    StandardLowTirePressureWarning = true,
                    BluetoothSystem = true,
                    WheelDrive = WheelDrive.Front_Wheel_Drive,
                    EngineType = EngineType.Gas,
                    State = State.California,
                    DcFastCharging = DcFastCharging.Available,
                    HomeCharger = HomeCharger.Type2,
                    SeatHeater = SeatHeater.FrontSeats,
                    Cartype = CarType.Gas,
                    ReservationFee = 100.00,
                    TotalCostOfCar = 20000.00,
                    ActivationFee = 200.00,
                    MonthlyCost = 300.00,
                    MarketValue = 22000.00,
                    DecrementMarketValue = 1000.00,
                    ImageUrl = "https://www.motortrend.com/uploads/sites/10/2019/11/2020-toyota-camry-se-auto-sedan-angular-front.png?w=640&width=640&q=75&format=webp",
                    PublicId = "car123",
                    VehicleStatus = VehicleStatus.Unassigned
                };

                await _vehicleRepository.AddVehicleAsync(firstVehicle);

                var vehicle2 = new Vehicle
                {
                    Make = "Chevrolet",
                    Model = "Impala",
                    PlateNumber = "XYZ789",
                    CarDescription = "A stylish and portable car.",
                    ChasisNumber = "1HGCM82633A654321",
                    SeatNumbers = "5",
                    Mileage = "20000",
                    TrunkSize = "16.7 cubic feet",
                    Color = "Black",
                    VIN = "1HGCM82633A654321",
                    PickupAddress = "789 Oak St, Sampletown, USA",
                    City = "Sampletown",
                    ZIP = "67890",
                    OperatorId = firstOperator.Id,
                    CarDealerName = firstOperator.BusinessName,
                    FinacialStartDate = "2022-05-01",
                    FinacialEndDate = "2027-05-01",
                    CarWarrantyOverview = "5 years / 60,000 miles",
                    Torque = "152 lb-ft",
                    TransmissionType = "Automatic",
                    Horsepower = "158 HP",
                    TurningDiameter = "38.1 ft",
                    CurbWeight = "3131 lbs",
                    DiscBrakes = "Yes",
                    TransmissionSpeed = "6",
                    DriveAxleRatio = "3.42",
                    StabilityControl = "Yes",
                    RangeOfFullCharge = null,
                    CargoSpace = "16.7 cubic feet",
                    TouchScreenDisplay = true,
                    DriverLumbarSupport = true,
                    NumberOfSpeakers = "8",
                    Radio = true,
                    AndroidAuto_AppleCarPlay = true,
                    TouchScreenNavSystem = true,
                    ProjectorBeamLedHeadlight = true,
                    RemoteKeylessEntry = true,
                    StandardLowTirePressureWarning = true,
                    BluetoothSystem = true,
                    WheelDrive = WheelDrive.Front_Wheel_Drive,
                    EngineType = EngineType.Gas,
                    State = State.California,
                    DcFastCharging = DcFastCharging.Available,
                    HomeCharger = HomeCharger.Type2,
                    SeatHeater = SeatHeater.AllSeats,
                    Cartype = CarType.Gas,
                    ReservationFee = 120.00,
                    TotalCostOfCar = 25000.00,
                    ActivationFee = 220.00,
                    MonthlyCost = 350.00,
                    MarketValue = 27000.00,
                    DecrementMarketValue = 1200.00,
                    ImageUrl = "https://www.motortrend.com/uploads/sites/10/2019/11/2020-chevrolet-impala-1lt-sedan-angular-front.png?w=640&width=640&q=75&format=webp",
                    PublicId = "car456",
                    VehicleStatus = VehicleStatus.Unassigned
                };

                await _vehicleRepository.AddVehicleAsync(vehicle2);

                var vehicle3 = new Vehicle 
                {
               
                    Make = "ADRO Ford",
                    Model = "Mustang Coupe",
                    PlateNumber = "XYZ789",
                    CarDescription = "A stylish and portable car.",
                    ChasisNumber = "1HGCM82633A654321",
                    SeatNumbers = "5",
                    Mileage = "20000",
                    TrunkSize = "16.7 cubic feet",
                    Color = "Ash",
                    VIN = "1HGCM82633A654321",
                    PickupAddress = "789 Oak St, Sampletown, USA",
                    City = "Sampletown",
                    ZIP = "67890",
                    OperatorId = firstOperator.Id,
                    CarDealerName = firstOperator.BusinessName,
                    FinacialStartDate = "2022-05-01",
                    FinacialEndDate = "2027-05-01",
                    CarWarrantyOverview = "5 years / 60,000 miles",
                    Torque = "152 lb-ft",
                    TransmissionType = "Automatic",
                    Horsepower = "158 HP",
                    TurningDiameter = "38.1 ft",
                    CurbWeight = "3131 lbs",
                    DiscBrakes = "Yes",
                    TransmissionSpeed = "6",
                    DriveAxleRatio = "3.42",
                    StabilityControl = "Yes",
                    RangeOfFullCharge = null,
                    CargoSpace = "16.7 cubic feet",
                    TouchScreenDisplay = true,
                    DriverLumbarSupport = true,
                    NumberOfSpeakers = "8",
                    Radio = true,
                    AndroidAuto_AppleCarPlay = true,
                    TouchScreenNavSystem = true,
                    ProjectorBeamLedHeadlight = true,
                    RemoteKeylessEntry = true,
                    StandardLowTirePressureWarning = true,
                    BluetoothSystem = true,
                    WheelDrive = WheelDrive.Front_Wheel_Drive,
                    EngineType = EngineType.Gas,
                    State = State.California,
                    DcFastCharging = DcFastCharging.Available,
                    HomeCharger = HomeCharger.Type2,
                    SeatHeater = SeatHeater.AllSeats,
                    Cartype = CarType.Gas,
                    ReservationFee = 120.00,
                    TotalCostOfCar = 25000.00,
                    ActivationFee = 220.00,
                    MonthlyCost = 350.00,
                    MarketValue = 27000.00,
                    DecrementMarketValue = 1200.00,
                    ImageUrl = "https://www.hdwallpapers.in/thumbs/2023/adro_ford_mustang_coupe_car_white_background_4k_hd_cars-t2.jpg",
                    PublicId = "car456",
                    VehicleStatus = VehicleStatus.Unassigned
                };

                await _vehicleRepository.AddVehicleAsync(vehicle3);

                var vehicle4 = new Vehicle
                {
                    Make = "Alfa Romeo",
                    Model = "Super",
                    PlateNumber = "XYZ789",
                    CarDescription = "A stylish and portable car.",
                    ChasisNumber = "1HGCM82633A654321",
                    SeatNumbers = "5",
                    Mileage = "20000",
                    TrunkSize = "16.7 cubic feet",
                    Color = "Red",
                    VIN = "1HGCM82633A654321",
                    PickupAddress = "789 Oak St, Sampletown, USA",
                    City = "Sampletown",
                    ZIP = "67890",
                    OperatorId = secondOperator.Id,
                    CarDealerName = secondOperator.BusinessName,
                    FinacialStartDate = "2022-05-01",
                    FinacialEndDate = "2027-05-01",
                    CarWarrantyOverview = "5 years / 60,000 miles",
                    Torque = "152 lb-ft",
                    TransmissionType = "Automatic",
                    Horsepower = "158 HP",
                    TurningDiameter = "38.1 ft",
                    CurbWeight = "3131 lbs",
                    DiscBrakes = "Yes",
                    TransmissionSpeed = "6",
                    DriveAxleRatio = "3.42",
                    StabilityControl = "Yes",
                    RangeOfFullCharge = null,
                    CargoSpace = "16.7 cubic feet",
                    TouchScreenDisplay = true,
                    DriverLumbarSupport = true,
                    NumberOfSpeakers = "8",
                    Radio = true,
                    AndroidAuto_AppleCarPlay = true,
                    TouchScreenNavSystem = true,
                    ProjectorBeamLedHeadlight = true,
                    RemoteKeylessEntry = true,
                    StandardLowTirePressureWarning = true,
                    BluetoothSystem = true,
                    WheelDrive = WheelDrive.Front_Wheel_Drive,
                    EngineType = EngineType.Gas,
                    State = State.California,
                    DcFastCharging = DcFastCharging.Available,
                    HomeCharger = HomeCharger.Type2,
                    SeatHeater = SeatHeater.AllSeats,
                    Cartype = CarType.Gas,
                    ReservationFee = 120.00,
                    TotalCostOfCar = 25000.00,
                    ActivationFee = 220.00,
                    MonthlyCost = 350.00,
                    MarketValue = 27000.00,
                    DecrementMarketValue = 1200.00,
                    ImageUrl = "https://www.hdwallpapers.in/thumbs/2010/alfa_romeo_super_car-t2.jpg",
                    PublicId = "car456",
                    VehicleStatus = VehicleStatus.Unassigned
                };
                await _vehicleRepository.AddVehicleAsync(vehicle4);

                var vehicle5 = new Vehicle
                {
                    Make = "Aston Martin",
                    Model = "Superleggera Concorde",
                    PlateNumber = "XYZ789",
                    CarDescription = "A stylish and portable car.",
                    ChasisNumber = "1HGCM82633A654321",
                    SeatNumbers = "5",
                    Mileage = "20000",
                    TrunkSize = "16.7 cubic feet",
                    Color = "White",
                    VIN = "1HGCM82633A654321",
                    PickupAddress = "789 Oak St, Sampletown, USA",
                    City = "Sampletown",
                    ZIP = "67890",
                    OperatorId = secondOperator.Id,
                    CarDealerName = secondOperator.BusinessName,
                    FinacialStartDate = "2022-05-01",
                    FinacialEndDate = "2027-05-01",
                    CarWarrantyOverview = "5 years / 60,000 miles",
                    Torque = "152 lb-ft",
                    TransmissionType = "Automatic",
                    Horsepower = "158 HP",
                    TurningDiameter = "38.1 ft",
                    CurbWeight = "3131 lbs",
                    DiscBrakes = "Yes",
                    TransmissionSpeed = "6",
                    DriveAxleRatio = "3.42",
                    StabilityControl = "Yes",
                    RangeOfFullCharge = null,
                    CargoSpace = "16.7 cubic feet",
                    TouchScreenDisplay = true,
                    DriverLumbarSupport = true,
                    NumberOfSpeakers = "8",
                    Radio = true,
                    AndroidAuto_AppleCarPlay = true,
                    TouchScreenNavSystem = true,
                    ProjectorBeamLedHeadlight = true,
                    RemoteKeylessEntry = true,
                    StandardLowTirePressureWarning = true,
                    BluetoothSystem = true,
                    WheelDrive = WheelDrive.Front_Wheel_Drive,
                    EngineType = EngineType.Gas,
                    State = State.California,
                    DcFastCharging = DcFastCharging.Available,
                    HomeCharger = HomeCharger.Type2,
                    SeatHeater = SeatHeater.AllSeats,
                    Cartype = CarType.Gas,
                    ReservationFee = 120.00,
                    TotalCostOfCar = 25000.00,
                    ActivationFee = 220.00,
                    MonthlyCost = 350.00,
                    MarketValue = 27000.00,
                    DecrementMarketValue = 1200.00,
                    ImageUrl = "https://www.hdwallpapers.in/thumbs/2019/aston_martin_dbs_superleggera_concorde_edition_2019_5k-t2.jpg",
                    PublicId = "car456",
                    VehicleStatus = VehicleStatus.Unassigned

                };

                await _vehicleRepository.AddVehicleAsync(vehicle5);

                var vehicle6 = new Vehicle
                {
                    Make = "BMW",
                    Model = "530li",
                    PlateNumber = "XYZ789",
                    CarDescription = "A stylish and portable car.",
                    ChasisNumber = "1HGCM82633A654321",
                    SeatNumbers = "5",
                    Mileage = "20000",
                    TrunkSize = "16.7 cubic feet",
                    Color = "Blue",
                    VIN = "1HGCM82633A654321",
                    PickupAddress = "789 Oak St, Sampletown, USA",
                    City = "Sampletown",
                    ZIP = "67890",
                    OperatorId = secondOperator.Id,
                    CarDealerName = secondOperator.BusinessName,
                    FinacialStartDate = "2022-05-01",
                    FinacialEndDate = "2027-05-01",
                    CarWarrantyOverview = "5 years / 60,000 miles",
                    Torque = "152 lb-ft",
                    TransmissionType = "Automatic",
                    Horsepower = "158 HP",
                    TurningDiameter = "38.1 ft",
                    CurbWeight = "3131 lbs",
                    DiscBrakes = "Yes",
                    TransmissionSpeed = "6",
                    DriveAxleRatio = "3.42",
                    StabilityControl = "Yes",
                    RangeOfFullCharge = null,
                    CargoSpace = "16.7 cubic feet",
                    TouchScreenDisplay = true,
                    DriverLumbarSupport = true,
                    NumberOfSpeakers = "8",
                    Radio = true,
                    AndroidAuto_AppleCarPlay = true,
                    TouchScreenNavSystem = true,
                    ProjectorBeamLedHeadlight = true,
                    RemoteKeylessEntry = true,
                    StandardLowTirePressureWarning = true,
                    BluetoothSystem = true,
                    WheelDrive = WheelDrive.Front_Wheel_Drive,
                    EngineType = EngineType.Gas,
                    State = State.California,
                    DcFastCharging = DcFastCharging.Available,
                    HomeCharger = HomeCharger.Type2,
                    SeatHeater = SeatHeater.AllSeats,
                    Cartype = CarType.Gas,
                    ReservationFee = 120.00,
                    TotalCostOfCar = 25000.00,
                    ActivationFee = 220.00,
                    MonthlyCost = 350.00,
                    MarketValue = 27000.00,
                    DecrementMarketValue = 1200.00,
                    ImageUrl = "https://www.hdwallpapers.in/thumbs/2020/bmw_530li_m_sport_blue_2020_4k_5k_hd_cars-t2.jpg",
                    PublicId = "car456",
                    VehicleStatus = VehicleStatus.Unassigned
                };

                await _vehicleRepository.AddVehicleAsync(vehicle6);

                var vehicle7 = new Vehicle
                {
                    Make = "Range Rover",
                    Model = "Sentinel",
                    PlateNumber = "XYZ789",
                    CarDescription = "A stylish and portable car.",
                    ChasisNumber = "1HGCM82633A654321",
                    SeatNumbers = "5",
                    Mileage = "20000",
                    TrunkSize = "16.7 cubic feet",
                    Color = "Black",
                    VIN = "1HGCM82633A654321",
                    PickupAddress = "789 Oak St, Sampletown, USA",
                    City = "Sampletown",
                    ZIP = "67890",
                    OperatorId = thirdOperator.Id,
                    CarDealerName = thirdOperator.BusinessName,
                    FinacialStartDate = "2022-05-01",
                    FinacialEndDate = "2027-05-01",
                    CarWarrantyOverview = "5 years / 60,000 miles",
                    Torque = "152 lb-ft",
                    TransmissionType = "Automatic",
                    Horsepower = "158 HP",
                    TurningDiameter = "38.1 ft",
                    CurbWeight = "3131 lbs",
                    DiscBrakes = "Yes",
                    TransmissionSpeed = "6",
                    DriveAxleRatio = "3.42",
                    StabilityControl = "Yes",
                    RangeOfFullCharge = null,
                    CargoSpace = "16.7 cubic feet",
                    TouchScreenDisplay = true,
                    DriverLumbarSupport = true,
                    NumberOfSpeakers = "8",
                    Radio = true,
                    AndroidAuto_AppleCarPlay = true,
                    TouchScreenNavSystem = true,
                    ProjectorBeamLedHeadlight = true,
                    RemoteKeylessEntry = true,
                    StandardLowTirePressureWarning = true,
                    BluetoothSystem = true,
                    WheelDrive = WheelDrive.Front_Wheel_Drive,
                    EngineType = EngineType.Gas, 
                    State = State.California,
                    DcFastCharging = DcFastCharging.Available,
                    HomeCharger = HomeCharger.Type2,
                    SeatHeater = SeatHeater.AllSeats,
                    Cartype = CarType.Gas,
                    ReservationFee = 120.00,
                    TotalCostOfCar = 25000.00,
                    ActivationFee = 220.00,
                    MonthlyCost = 350.00,
                    MarketValue = 27000.00,
                    DecrementMarketValue = 1200.00,
                    ImageUrl = "https://www.hdwallpapers.in/thumbs/2019/range_rover_sentinel_2019_4k_8k-t2.jpg",
                    PublicId = "car456",
                    VehicleStatus = VehicleStatus.Unassigned
                };

                await _vehicleRepository.AddVehicleAsync(vehicle7);

                var vehicle8 = new Vehicle
                {
                    Make = "Maybach",
                    Model = "Zeppelin",
                    PlateNumber = "XYZ789",
                    CarDescription = "A stylish and portable car.",
                    ChasisNumber = "1HGCM82633A654321",
                    SeatNumbers = "5",
                    Mileage = "20000",
                    TrunkSize = "16.7 cubic feet",
                    Color = "Beige",
                    VIN = "1HGCM82633A654321",
                    PickupAddress = "789 Oak St, Sampletown, USA",
                    City = "Sampletown",
                    ZIP = "67890",
                    OperatorId = thirdOperator.Id,
                    CarDealerName = thirdOperator.BusinessName,
                    FinacialStartDate = "2022-05-01",
                    FinacialEndDate = "2027-05-01",
                    CarWarrantyOverview = "5 years / 60,000 miles",
                    Torque = "152 lb-ft",
                    TransmissionType = "Automatic",
                    Horsepower = "158 HP",
                    TurningDiameter = "38.1 ft",
                    CurbWeight = "3131 lbs",
                    DiscBrakes = "Yes",
                    TransmissionSpeed = "6",
                    DriveAxleRatio = "3.42",
                    StabilityControl = "Yes",
                    RangeOfFullCharge = null,
                    CargoSpace = "16.7 cubic feet",
                    TouchScreenDisplay = true,
                    DriverLumbarSupport = true,
                    NumberOfSpeakers = "8",
                    Radio = true,
                    AndroidAuto_AppleCarPlay = true,
                    TouchScreenNavSystem = true,
                    ProjectorBeamLedHeadlight = true,
                    RemoteKeylessEntry = true,
                    StandardLowTirePressureWarning = true,
                    BluetoothSystem = true,
                    WheelDrive = WheelDrive.Front_Wheel_Drive,
                    EngineType = EngineType.Gas,
                    State = State.California,
                    DcFastCharging = DcFastCharging.Available,
                    HomeCharger = HomeCharger.Type2,
                    SeatHeater = SeatHeater.AllSeats,
                    Cartype = CarType.Gas,
                    ReservationFee = 120.00,
                    TotalCostOfCar = 25000.00,
                    ActivationFee = 220.00,
                    MonthlyCost = 350.00,
                    MarketValue = 27000.00,
                    DecrementMarketValue = 1200.00,
                    ImageUrl = "https://www.hdwallpapers.in/thumbs/2010/maybach_zeppelin_classic-t2.jpg",
                    PublicId = "car456",
                    VehicleStatus = VehicleStatus.Unassigned

                };

                await _vehicleRepository.AddVehicleAsync(vehicle8);

                var vehicle9 = new Vehicle
                {
                    Make = "Toyota",
                    Model = "Avalon XSE",
                    PlateNumber = "XYZ789",
                    CarDescription = "A stylish and portable car.",
                    ChasisNumber = "1HGCM82633A654321",
                    SeatNumbers = "5",
                    Mileage = "20000",
                    TrunkSize = "16.7 cubic feet",
                    Color = "Black",
                    VIN = "1HGCM82633A654321",
                    PickupAddress = "789 Oak St, Sampletown, USA",
                    City = "Sampletown",
                    ZIP = "67890",
                    OperatorId = thirdOperator.Id,
                    CarDealerName = thirdOperator.BusinessName,
                    FinacialStartDate = "2022-05-01",
                    FinacialEndDate = "2027-05-01",
                    CarWarrantyOverview = "5 years / 60,000 miles",
                    Torque = "152 lb-ft",
                    TransmissionType = "Automatic",
                    Horsepower = "158 HP",
                    TurningDiameter = "38.1 ft",
                    CurbWeight = "3131 lbs",
                    DiscBrakes = "Yes",
                    TransmissionSpeed = "6",
                    DriveAxleRatio = "3.42",
                    StabilityControl = "Yes",
                    RangeOfFullCharge = null,
                    CargoSpace = "16.7 cubic feet",
                    TouchScreenDisplay = true,
                    DriverLumbarSupport = true,
                    NumberOfSpeakers = "8",
                    Radio = true,
                    AndroidAuto_AppleCarPlay = true,
                    TouchScreenNavSystem = true,
                    ProjectorBeamLedHeadlight = true,
                    RemoteKeylessEntry = true,
                    StandardLowTirePressureWarning = true,
                    BluetoothSystem = true,
                    WheelDrive = WheelDrive.Front_Wheel_Drive,
                    EngineType = EngineType.Gas,
                    State = State.California,
                    DcFastCharging = DcFastCharging.Available,
                    HomeCharger = HomeCharger.Type2,
                    SeatHeater = SeatHeater.AllSeats,
                    Cartype = CarType.Gas,
                    ReservationFee = 120.00,
                    TotalCostOfCar = 25000.00,
                    ActivationFee = 220.00,
                    MonthlyCost = 350.00,
                    MarketValue = 27000.00,
                    DecrementMarketValue = 1200.00,
                    ImageUrl = "https://www.hdwallpapers.in/thumbs/2020/2021_toyota_avalon_xse_nightshade_edition_4k_hd-t2.jpg",
                    PublicId = "car456",
                    VehicleStatus = VehicleStatus.Unassigned
                };

                await _vehicleRepository.AddVehicleAsync(vehicle9);
              
            }

            if (!users.Any(u => _userManager.IsInRoleAsync(u, "Main_Subscriber").Result))
            {
                var subscriberUser2 = new AppUser
                {
                    UserName = "subscriberprime4@mailinator.com",
                    Email = "subscriberprime4@mailinator.com",
                    FirstName = "Subscriber",
                    LastName = "Prime4",
                    Type = "Main_Subscriber"
                };
                await _userManager.CreateAsync(subscriberUser2, "199026_Ll");
                await _userManager.AddToRoleAsync(subscriberUser2, "Main_Subscriber");
            }


        }

        
    }
}
